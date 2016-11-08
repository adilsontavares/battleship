using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseExpoInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if (time == 0) return 0;
            if (time == 1) return 1;
            if ((time /= 1f / 2f) < 1f) return 1f / 2f * Mathf.Pow(2f, 10f * (time - 1f));
            return 1f / 2f * (-Mathf.Pow(2f, -10f * --time) + 2f);
        }
    }
}
