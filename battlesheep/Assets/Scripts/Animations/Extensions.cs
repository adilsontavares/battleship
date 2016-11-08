using Tween.Animation.Callbacks;
using Tween.Animation.Ease;
using Tween.Animation.Helpers;
using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation
{
    public static class AnimationExtensions
    {
        public static AnimationBehaviour CreateAnimation<TEase>(this MonoBehaviour monoBehaviour, float duration,
            float delay = 0f, string name = null, StartCallback startCallback = null, UpdateCallback updateCallback = null,
            CancelCallback cancelCallback = null, DoneCallback doneCallback = null, FinishCallback finishCallback = null,
            bool single = false, bool considerTimeScale = true, object data = null, bool inverse = false) where TEase : IAnimationEase, new()
        {
            var ease = new TEase();
            return CreateAnimation(monoBehaviour, ease, duration, delay, name, startCallback, updateCallback, cancelCallback, doneCallback, finishCallback, single, considerTimeScale, data, inverse);
        }

        public static AnimationBehaviour CreateAnimation(this MonoBehaviour monoBehaviour, AnimationEaseType easeType,
            float duration,
            float delay = 0f, string name = null, StartCallback startCallback = null, UpdateCallback updateCallback = null,
            CancelCallback cancelCallback = null,
            DoneCallback doneCallback = null, FinishCallback finishCallback = null, bool single = false,
            bool considerTimeScale = false, object data = null, bool inverse = false)
        {
            var ease = AnimationHelper.EaseTypeToFunction(easeType);
            return CreateAnimation(monoBehaviour, ease, duration, delay, name, startCallback, updateCallback, cancelCallback, doneCallback, finishCallback, single, considerTimeScale, data, inverse);
        }

        public static AnimationBehaviour CreateAnimation(this MonoBehaviour monoBehaviour, IAnimationEase ease, float duration,
            float delay = 0f, string name = null, StartCallback startCallback = null, UpdateCallback updateCallback = null, CancelCallback cancelCallback = null,
            DoneCallback doneCallback = null, FinishCallback finishCallback = null, bool single = false, bool considerTimeScale = false, object data = null, bool inverse = false)
        {
            if (single)
            {
                if (string.IsNullOrEmpty(name))
                    Debug.Log("Animation can just be single if its name is not empty neither null.");
                else
                    CancelAnimations(monoBehaviour, name);
            }

            var gameObject = monoBehaviour.gameObject;
            var anim = gameObject.AddComponent<AnimationBehaviour>();
            anim.Delay = delay;
            anim.Name = name;
            anim.Ease = ease;
            anim.StartCallback = startCallback;
            anim.UpdateCallback = updateCallback;
            anim.FinishCallback = finishCallback;
            anim.Duration = duration;
            anim.DoneCallback = doneCallback;
            anim.CancelCallback = cancelCallback;
            anim.ConsiderTimeScale = considerTimeScale;
            anim.Data = data ?? monoBehaviour;
            anim.Inverse = inverse;
            anim.Start();

            return anim;
        }
        public static bool CancelAnimations(this MonoBehaviour monoBehaviour, string name)
        {
            if (monoBehaviour == null || string.IsNullOrEmpty(name)) return false;

            var canceled = false;

            var animations = monoBehaviour.GetComponents<AnimationBehaviour>();
            foreach (var anim in animations)
            {
                if (anim.Name == name)
                {
                    anim.Cancel();
                    canceled = true;
                }
            }

            return canceled;
        }

        public static bool CancelAnimations(this MonoBehaviour monoBehaviour)
        {
            var animations = monoBehaviour.GetComponents<AnimationBehaviour>();
            var canceled = animations.Length != 0;

            foreach (var anim in animations)
                anim.Cancel();

            return canceled;
        }
    }
}
