using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseCircOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return 1f * Mathf.Sqrt(1f - (time = time / 1f - 1f) * time);
        }
    }
}
