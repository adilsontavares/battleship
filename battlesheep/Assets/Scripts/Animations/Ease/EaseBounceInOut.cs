using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBounceInOut : IAnimationEase
    {
        private readonly EaseBounceIn _bounceIn = new EaseBounceIn();
        private readonly EaseBounceOut _bounceOut = new EaseBounceOut();

        public float ConvertTime(float time)
        {
            if (time < 1f / 2f) return _bounceIn.ConvertTime(time * 2f) * 0.5f;
            return _bounceOut.ConvertTime(time * 2f - 1f) * 0.5f + 1f * 0.5f;
        }
    }
}
