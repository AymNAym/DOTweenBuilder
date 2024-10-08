using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenScale : DOTweenGenericElement<Transform, Vector3>
    {
        public override Tween Generate()
        {
            return Target.DOScale(Value, Duration);
        }
    }
}
