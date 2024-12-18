using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace OSK
{
    [System.Serializable]
    public class TweenSettings
    {
        public enum TypeAnim
        {
            Ease,
            Curve
        }

        public Transform root;
        public bool playOnEnable = true;
        public bool setAutoKill = true;
        public bool isPlayBackwards = false;

        [Min(0)] public float delay = 0f;
        [Min(0)] public float duration = 1f;
        [Min(-1)] public int loopcount = 0;

        [ShowIf(nameof(loopcount), -1)] public LoopType loopType = LoopType.Restart;

        public TypeAnim typeAnim = TypeAnim.Ease;

        [ShowIf(nameof(typeAnim), TypeAnim.Ease)]
        public Ease ease = Ease.Linear;

        [ShowIf(nameof(typeAnim), TypeAnim.Curve)]
        public AnimationCurve curve;

        public UpdateType updateType = UpdateType.Normal;
        public bool useUnscaledTime = false;
        public UnityEvent eventCompleted;

        public TweenSettings Clone()
        {
            return new TweenSettings
            {
                root = root,
                playOnEnable = playOnEnable,
                setAutoKill = setAutoKill,
                isPlayBackwards = isPlayBackwards,
                delay = delay,
                duration = duration,
                loopcount = loopcount,
                loopType = loopType,
                typeAnim = typeAnim,
                ease = ease,
                curve = curve,
                updateType = updateType,
                useUnscaledTime = useUnscaledTime,
                eventCompleted = eventCompleted
            };
        }
    }

    public abstract class DoTweenBaseProvider : MonoBehaviour, IDoTweenProvider
    {
        public TweenSettings settings;

        [HideInInspector] public Object target;
        public Transform RootTransform => settings.root ? settings.root : transform;
        public RectTransform RootRectTransform => settings.root as RectTransform;

        public Tweener tweener;
        public Tweener Tweener => tweener;
        private bool isPlayBackwards = false;

        public bool IsPlaying => null != tweener && tweener.IsPlaying();

        public virtual void OnEnable() => Play();
        public virtual void OnDisable() => Stop();
        public virtual void OnDestroy() => Stop();

        public abstract object GetStartValue();
        public abstract object GetEndValue();

        public virtual void InitFromMG(bool playOnEnable, bool setAutoKill, UpdateType updateType, bool useUnscaledTime)
        {
            settings.playOnEnable = playOnEnable;
            settings.setAutoKill = setAutoKill;
            settings.updateType = updateType;
            settings.useUnscaledTime = useUnscaledTime;
        }

        public virtual void ProgressTween(bool isPlayBackwards)
        {
            tweener.SetDelay(settings.delay)
                .SetAutoKill(settings.setAutoKill)
                .SetLoops(settings.loopcount, settings.loopType)
                .SetUpdate(settings.updateType, settings.useUnscaledTime)
                .SetTarget(target)
                .OnComplete(() => settings.eventCompleted?.Invoke());

            if (isPlayBackwards)
            {
                tweener.From();
                tweener.Rewind();
                tweener.Play();
            }

            if (settings.typeAnim == TweenSettings.TypeAnim.Ease)
                tweener.SetEase(settings.ease);
            else
                tweener.SetEase(settings.curve);
        }

        [ContextMenu("Play")]
        public virtual void Play()
        {
            if (!settings.playOnEnable)
                return;

            tweener?.Kill();
            tweener = null;
            if (!target)
                if (tweener != null)
                    target = (UnityEngine.Object)tweener.target;

            isPlayBackwards = settings.isPlayBackwards;
            ProgressTween(isPlayBackwards);
        }


        public float GetDuration() => settings.duration;

        public void Preview(float time)
        {
            if (tweener == null) return;
            tweener.Goto(time);
        }

        [ContextMenu("PlayBackwards")]
        public void PlayBackwards()
        {
            if (!settings.playOnEnable)
                return;

            tweener?.Kill();
            tweener = null;
            if (!target)
                if (tweener != null)
                    target = (UnityEngine.Object)tweener.target;

            isPlayBackwards = true;
            ProgressTween(isPlayBackwards);
        }

        public virtual void Rewind() => tweener?.Rewind();

        public virtual void Stop()
        {
            tweener?.Kill();
            tweener = null;
            target = null;
        }

        public virtual void Resume() => tweener?.Play();
        public virtual void Pause() => tweener?.Pause();
        public virtual void Kill() => tweener?.Kill();
        public virtual void Restart() => tweener?.Restart();
    }
}