using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenAnchorPos : DOTweenGenericElement<RectTransform, Vector2>
    {
        public override Tween Generate()
        {
            return Target.DOAnchorPos(Value, Duration, SnapToInteger);
        }
    }
}
