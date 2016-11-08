using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBounceIn : IAnimationEase
    {
        private readonly EaseBounceOut _bounceOut = new EaseBounceOut();

        public float ConvertTime(float time)
        {
            return 1f - _bounceOut.ConvertTime(1f - time);
        }
    }
}
