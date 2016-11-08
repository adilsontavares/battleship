using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuintIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return 1f * (time /= 1f) * time * time * time * time;
        }
    }
}
