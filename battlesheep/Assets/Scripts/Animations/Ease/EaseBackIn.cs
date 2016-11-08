using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBackIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            var s = 1.70158f;
            return 1f * (time /= 1f) * time * ((s + 1f) * time - s);
        }
    }
}
