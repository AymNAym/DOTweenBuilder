using System;
using DG.Tweening;
using UnityEngine;
namespace CCLBStudio.DTB
{
    [Serializable]
    public class DOTweenBoolVariable : DOTweenVariable<bool, DOTweenBoolValue>
    {
        public DOTweenBoolVariable(bool defaultValue) : base(defaultValue)
        {
        }
    }
}
