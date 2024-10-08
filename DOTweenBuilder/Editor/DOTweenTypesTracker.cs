using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CCLBStudioEditor;
using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTweenTypesTracker
    {
        private readonly string _scriptableValueFolder;

        public DOTweenTypesTracker()
        {
            var settings = EditorExtender.LoadScriptableAsset<DOTweenBuilderEditorSettings>();
            
            string relativePath = AssetDatabase.GetAssetPath(settings);
            string absolutePath = Path.GetFullPath(relativePath);
            string settingsFolder = Path.GetDirectoryName(absolutePath);
            
            if (!Directory.Exists(settingsFolder))
            {
                Debug.LogError($"No folder at path {settingsFolder}, this is not normal.");
                return;
            }
            
            string parentFolder = Directory.GetParent(settingsFolder)?.FullName;
            _scriptableValueFolder = parentFolder + "/Main/ScriptableAsVariable/";
        }

        private static void TrackElementTypes(string parentFolder, DOTweenBuilderEditorSettings settings)
        {
            DOTweenBuilderEditorSettings.TrackedTypes = new List<DOTweenTrackedType>();
            List<string> filteredFolders = Directory.EnumerateDirectories(parentFolder).Where(x => !settings.foldersToIgnore.Contains(Path.GetFileName(x))).ToList();

            foreach (var folderPath in filteredFolders)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath, "*cs", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    string relative = "Assets/" + Path.GetRelativePath(Application.dataPath, file);
                    Type type = AssetDatabase.LoadAssetAtPath<MonoScript>(relative).GetClass();
                    if (!typeof(DOTweenElement).IsAssignableFrom(type))
                    {
                        continue;
                    }

                    var arr = GetGenericArguments(type);
                    DOTweenBuilderEditorSettings.TrackedTypes.Add(new DOTweenTrackedType
                    {
                        type = type,
                        targetType = arr[0],
                        valueType = arr[1]
                    });
                }
            }
        }

        public List<DOTweenTrackedType> TrackScriptableTypes()
        {
            if (string.IsNullOrEmpty(_scriptableValueFolder))
            {
                return null;
            }

            var tracked = new List<DOTweenTrackedType>();

            IEnumerable<string> files = Directory.EnumerateFiles(_scriptableValueFolder, "*cs", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                string relativePath = "Assets/" + Path.GetRelativePath(Application.dataPath, file);
                Type type = AssetDatabase.LoadAssetAtPath<MonoScript>(relativePath).GetClass();
                if (null == type)
                {
                    continue;
                }
                
                if (!IsSubclassOfRawGeneric(typeof(DOTweenScriptableValue<>), type, out Type genericParam))
                {
                    continue;
                }
                
                tracked.Add(new DOTweenTrackedType
                {
                    type = type,
                    valueType = genericParam
                });
            }
            
            tracked.TrimExcess();
            return tracked;
        }

        private static Type[] GetGenericArguments(Type from)
        {
            Type type = from;

            while (type != null && type.GenericTypeArguments.Length < 2)
            {
                type = type.BaseType;
            }

            if (type == null)
            {
                throw new Exception("Unexpected result.");
            }

            return type.GetGenericArguments();
        }
        
        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck, out Type genericParam)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    genericParam = toCheck.GetGenericArguments()[0];
                    return true;
                }
                toCheck = toCheck.BaseType;
            }

            genericParam = null;
            return false;
        }
    }
}
