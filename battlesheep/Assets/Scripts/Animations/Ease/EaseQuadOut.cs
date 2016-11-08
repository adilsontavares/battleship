using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseQuadOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return -1f * time * (time - 2f);
        }
    }
}
