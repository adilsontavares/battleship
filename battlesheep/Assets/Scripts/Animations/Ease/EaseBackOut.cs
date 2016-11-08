using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBackOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            var s = 1.70158f;
            return 1 * ((time = time / 1 - 1) * time * ((s + 1) * time + s) + 1);
        }
    }
}
