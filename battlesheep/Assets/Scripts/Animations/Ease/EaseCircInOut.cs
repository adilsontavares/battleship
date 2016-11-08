using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseCircInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if ((time /= 1f / 2f) < 1f) return -1f / 2f * (Mathf.Sqrt(1f - time * time) - 1f);
            return 1f / 2f * (Mathf.Sqrt(1f - (time -= 2f) * time) + 1f);
        }
    }
}
