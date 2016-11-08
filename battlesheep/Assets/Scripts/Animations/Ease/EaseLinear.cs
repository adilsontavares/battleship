using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseLinear : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return time;
        }
    }
}
