using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBackInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            var s = 1.70158f;
            if ((time /= 1f / 2f) < 1) return 1f / 2f * (time * time * (((s *= (1.525f)) + 1f) * time - s));
            return 1f / 2f * ((time -= 2f) * time * (((s *= (1.525f)) + 1f) * time + s) + 2f);
        }
    }
}
