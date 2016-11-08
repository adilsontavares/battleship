using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseSineIn : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return -1f * Mathf.Cos(time / 1f * (Mathf.PI / 2f)) + 1f;
        }
    }
}
