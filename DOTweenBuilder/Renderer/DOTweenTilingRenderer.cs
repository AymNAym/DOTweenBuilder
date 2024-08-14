using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenTilingRenderer : DOTweenRendererElement<Vector2>
    {
        public override Tween Generate()
        {
            AssignPropertyId();
            
            var sq = DOTween.Sequence();
            
            if (string.IsNullOrEmpty(Property))
            {
                foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
                {
                    sq.Join(m.DOTiling(Value, Duration));
                }

                return sq;
            }

            foreach (var m in UseSharedMaterials ? Target.sharedMaterials : Target.materials)
            {
                sq.Join(m.DOTiling(Value, propertyId, Duration));
            }

            return sq;
        }
    }
}
