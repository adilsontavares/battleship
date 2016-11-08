using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuintInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if ((time /= 1f / 2f) < 1f) return 1f / 2f * time * time * time * time * time;
            return 1f / 2f * ((time -= 2f) * time * time * time * time + 2f);
        }
    }
}
