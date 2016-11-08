using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseCubicOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return 1f * ((time = time / 1f - 1f) * time * time + 1f);
        }
    }
}
