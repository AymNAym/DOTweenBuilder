using System;
using DG.Tweening;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [Serializable]
    public class DOTweenShakeRotation : DOTweenShakeElement<Transform>
    {
        public override Tween Generate()
        {
            return Target.DOShakeRotation(Duration, Value, Vibrato, Randomness, FadeOut, RandomnessMode);
        }
    }
}
