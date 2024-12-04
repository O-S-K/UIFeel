using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace OSK
{
    public class BaseButton : MonoBehaviour
    {
        [Serializable]
        public class TweenSetting
        {
            [Header("Animation")] [EnumToggleButtons]
            public AnimationType animationType = AnimationType.Scale;

            public float Duration;
            public bool UseCurve;

            [ShowIf(nameof(UseCurve), true)] public AnimationCurve Curve;

            [HideIf(nameof(UseCurve), true)] public Ease EaseType;

            [ShowIf("@animationType.HasFlag(AnimationType.Scale)")]
            public Vector3 SizeTarget;

            [ShowIf("@animationType.HasFlag(AnimationType.Rotator)")]
            public Vector3 RotationTarget;

            [ShowIf("@animationType.HasFlag(AnimationType.Shaker)")]
            public float StrengthTarget;

            [ShowIf("@animationType.HasFlag(AnimationType.Shaker)")]
            public int Vibrato;

            public TweenSetting()
            {
            }

            public TweenSetting(float duration, bool useCurve, AnimationCurve curve, Ease easeType, Vector3 sizeTarget,
                Vector3 rotationTarget, float strengthTarget, int vibrato)
            {
                this.Duration = duration;
                this.UseCurve = useCurve;
                this.Curve = curve;
                this.EaseType = easeType;
                this.SizeTarget = sizeTarget;
                this.RotationTarget = rotationTarget;
                this.StrengthTarget = strengthTarget;
                this.Vibrato = vibrato;
            }
        }

        [System.Flags]
        public enum AnimationType
        {
            Scale = 1 << 0, // 1
            Rotator = 1 << 1, // 2
            Shaker = 1 << 2 // 4
        }

        protected Vector3 defaultSize;
        protected Vector3 defaultRotation;
        protected Vector3 defaultPosition;

        [Header("Sound")] public bool playSoundOnClick;
        [ShowIf(nameof(playSoundOnClick))] public string soundClick = "ui_click";

        protected Tween mTween;

        protected void KillTween()
        {
            mTween?.Kill();
            mTween = null;
        }

        public void ApplyTweenDown(TweenSetting setting)
        {
            if (setting.animationType.HasFlag(AnimationType.Scale))
            {
                mTween = transform.DOScale(setting.SizeTarget, setting.Duration).SetUpdate(true);
            }

            if (setting.animationType.HasFlag(AnimationType.Rotator))
            {
                mTween = transform.DORotate(defaultRotation + setting.RotationTarget, setting.Duration);
            }

            if (setting.animationType.HasFlag(AnimationType.Shaker))
            {
                mTween = transform.DOShakePosition(setting.Duration, setting.StrengthTarget, setting.Vibrato, 90,
                    false);
            }

            // Apply easing
            if (setting.UseCurve)
                mTween?.SetEase(setting.Curve); // Ensure mTween is not null
            else
                mTween?.SetEase(setting.EaseType);
        }


        public void ApplyTweenReset(TweenSetting setting)
        {
            defaultSize = transform.localScale;
            defaultRotation = Vector3.zero;
            defaultPosition = transform.localPosition;
        }
    }
}