using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuadInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if ((time /= 1f / 2f) < 1f) return 1f / 2f * time * time;
            return -1f / 2f * ((--time) * (time - 2f) - 1f);
        }
    }
}
