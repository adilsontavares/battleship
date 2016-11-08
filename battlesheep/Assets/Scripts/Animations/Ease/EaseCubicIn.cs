using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseCubicIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return time * time * time;
        }
    }
}
