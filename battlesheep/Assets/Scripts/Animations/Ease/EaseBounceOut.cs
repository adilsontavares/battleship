using Tween.Animation.Interfaces;

namespace Tween.Animation.Ease
{
    public class EaseBounceOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            if ((time /= 1f) < (1f / 2.75f))
            {
                return 1f * (7.5625f * time * time);
            }
            else if (time < (2f / 2.75f))
            {
                return 1f * (7.5625f * (time -= (1.5f / 2.75f)) * time + 0.75f);
            }
            else if (time < (2.5f / 2.75f))
            {
                return 1f * (7.5625f * (time -= (2.25f / 2.75f)) * time + 0.9375f);
            }
            else
            {
                return 1f * (7.5625f * (time -= (2.625f / 2.75f)) * time + .984375f);
            }
        }
    }
}
