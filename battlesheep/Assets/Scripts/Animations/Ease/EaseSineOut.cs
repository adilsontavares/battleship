using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseSineOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return 1f * Mathf.Sin(time / 1f * (Mathf.PI / 2f));
        }
    }
}
