using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseElasticIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            float s = 1.70158f, p = 0, a = 1;
            if (time == 0) return 0; if ((time /= 1f) == 1f) return 1f; if (p == 0) p = 1f * 0.3f;
            if (a < Mathf.Abs(1f)) { a = 1f; s = p / 4f; }
            else s = p / (2f * Mathf.PI) * Mathf.Asin(1f / a);
            return -(a * Mathf.Pow(2f, 10f * (time -= 1f)) * Mathf.Sin((time * 1f - s) * (2f * Mathf.PI) / p));
        }
    }
}
