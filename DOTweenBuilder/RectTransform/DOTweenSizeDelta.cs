using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenSizeDelta : DOTweenGenericElement<RectTransform, Vector2>
    {
        public override Tween Generate()
        {
            return Target.DOSizeDelta(Value, Duration, SnapToInteger);
        }
    }
}
