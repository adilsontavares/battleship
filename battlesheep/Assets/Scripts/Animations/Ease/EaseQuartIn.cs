using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuartIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return time * time * time * time;
        }
    }
}
