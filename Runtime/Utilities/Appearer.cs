using UnityEngine;

namespace OSK
{
    public class Appearer : MonoBehaviour
    {
        public TweenEasings.Easings animStartButton = TweenEasings.Easings.BounceEaseOut;
        public TweenEasings.Easings animEndButton = TweenEasings.Easings.ExponentialEaseIn;
        public float duration = 0.3f;

        public float appearAfter;
        public float hideDelay;

        private bool shown;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            if (appearAfter >= 0)
                Invoke(nameof(Show), appearAfter);
        }

        public void Show()
        {
            Tweener.Instance.ScaleTo(transform, Vector3.one, duration, 0f, TweenEasings.GetEasing(animStartButton));

            if (!shown)
            {
                shown = true;
            }
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Show));
            CancelInvoke(nameof(Hide));
        }

        public void Hide()
        {
            Tweener.Instance.ScaleTo(transform, Vector3.zero, duration, 0f, TweenEasings.GetEasing(animEndButton));

            if (shown)
            {
                shown = false;
            }
        }

        public void HideWithDelay()
        {
            Invoke(nameof(Hide), hideDelay);
        }
    }
}