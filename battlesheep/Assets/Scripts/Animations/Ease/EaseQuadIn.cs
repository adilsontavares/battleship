using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuadIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return time * time;
        }
    }
}
