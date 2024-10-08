using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenPixelRect : DOTweenGenericElement<Camera, Rect>
    {
        public override Tween Generate()
        {
            return Target.DOPixelRect(Value, Duration);
        }
    }
}
