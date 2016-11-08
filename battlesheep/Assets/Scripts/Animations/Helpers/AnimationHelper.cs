using Tween.Animation.Ease;
using Tween.Animation.Interfaces;

namespace Tween.Animation.Helpers
{
    public static class AnimationHelper
    {
        public static IAnimationEase EaseTypeToFunction(AnimationEaseType easeType)
        {
            switch (easeType)
            {
                case AnimationEaseType.EaseBackIn:
                    return new EaseBackIn();

                case AnimationEaseType.EaseBackOut:
                    return new EaseBackOut();

                case AnimationEaseType.EaseBackInOut:
                    return new EaseBackInOut();

                case AnimationEaseType.EaseBounceIn:
                    return new EaseBounceIn();

                case AnimationEaseType.EaseBounceOut:
                    return new EaseBounceOut();

                case AnimationEaseType.EaseBounceInOut:
                    return new EaseBounceInOut();

                case AnimationEaseType.EaseCircIn:
                    return new EaseCircIn();

                case AnimationEaseType.EaseCircOut:
                    return new EaseCircOut();

                case AnimationEaseType.EaseCircInOut:
                    return new EaseCircInOut();

                case AnimationEaseType.EaseCubicIn:
                    return new EaseCubicIn();

                case AnimationEaseType.EaseCubicOut:
                    return new EaseCubicOut();

                case AnimationEaseType.EaseCubicInOut:
                    return new EaseCubicInOut();

                case AnimationEaseType.EaseElasticIn:
                    return new EaseElasticIn();

                case AnimationEaseType.EaseElasticOut:
                    return new EaseElasticOut();

                case AnimationEaseType.EaseElasticInOut:
                    return new EaseElasticInOut();

                case AnimationEaseType.EaseExpoIn:
                    return new EaseExpoIn();

                case AnimationEaseType.EaseExpoOut:
                    return new EaseExpoOut();

                case AnimationEaseType.EaseExpoInOut:
                    return new EaseExpoInOut();

                case AnimationEaseType.EaseLinear:
                    return new EaseLinear();

                case AnimationEaseType.EaseQuadIn:
                    return new EaseQuadIn();

                case AnimationEaseType.EaseQuadOut:
                    return new EaseQuadOut();

                case AnimationEaseType.EaseQuadInOut:
                    return new EaseQuadInOut();

                case AnimationEaseType.EaseQuartIn:
                    return new EaseQuartIn();

                case AnimationEaseType.EaseQuartOut:
                    return new EaseQuartOut();

                case AnimationEaseType.EaseQuartInOut:
                    return new EaseQuartInOut();

                case AnimationEaseType.EaseQuintIn:
                    return new EaseQuintIn();

                case AnimationEaseType.EaseQuintOut:
                    return new EaseQuintOut();

                case AnimationEaseType.EaseQuintInOut:
                    return new EaseQuintInOut();

                case AnimationEaseType.EaseSineIn:
                    return new EaseSineIn();

                case AnimationEaseType.EaseSineOut:
                    return new EaseSineOut();

                case AnimationEaseType.EaseSineInOut:
                    return new EaseSineInOut();
            }

            return null;
        }
    }
}
