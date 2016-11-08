using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseExpoIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return (time == 0) ? 1f : 1f * Mathf.Pow(2f, 10f * (time / 1f - 1f));
        }
    }
}
