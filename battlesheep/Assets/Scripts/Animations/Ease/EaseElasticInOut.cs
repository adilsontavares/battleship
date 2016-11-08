using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseElasticInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            float s = 1.70158f, p = 0, a = 1;
            if (time == 0) return 0; if ((time /= 1f / 2f) == 2f) return 1f; if (p == 0) p = 1f * (0.3f * 1.5f);
            if (a < Mathf.Abs(1f)) { a = 1f; s = p / 4f; }
            else s = p / (2f * Mathf.PI) * Mathf.Asin(1f / a);
            if (time < 1f) return -0.5f * (a * Mathf.Pow(2f, 10f * (time -= 1f)) * Mathf.Sin((time * 1f - s) * (2f * Mathf.PI) / p));
            return a * Mathf.Pow(2f, -10f * (time -= 1f)) * Mathf.Sin((time * 1f - s) * (2f * Mathf.PI) / p) * 0.5f + 1f;
        }
    }
}
