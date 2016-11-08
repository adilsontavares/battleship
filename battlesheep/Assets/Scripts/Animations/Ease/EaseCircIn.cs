using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseCircIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if (time >= 1) return time;
            return -1f * (Mathf.Sqrt(1f - (time /= 1f) * time) - 1f);
        }
    }
}
