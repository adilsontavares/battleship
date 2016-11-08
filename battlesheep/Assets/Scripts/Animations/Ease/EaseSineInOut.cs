using Tween.Animation.Interfaces;
using UnityEngine;

namespace Tween.Animation.Ease
{
    public class EaseSineInOut : IAnimationEase
    {
        public float ConvertTime(float time)
        {
            return -1f / 2f * (Mathf.Cos(Mathf.PI * time / 1f) - 1f);
        }
    }
}
