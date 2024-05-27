using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OSK
{
    public class Blinders : MonoBehaviour
    {
        public Transform left, right;
        public bool startsOpen, openAtStart = true;

        public UnityEvent openCompleted;
        public UnityEvent closeCompleted;

        private float duration = 0.3f;
        private bool isOpen;

        private void Start()
        {
            isOpen = startsOpen;

            if (startsOpen) return;

            left.transform.localScale = new Vector3(1f, 1f, 1f);
            right.transform.localScale = new Vector3(1f, 1f, 1f);

            if (openAtStart)
                Invoke(nameof(Open), 0.5f);
        }

        public void Close()
        {
            if (!isOpen) return;

            Tweener.Instance.ScaleTo(left, Vector3.one, duration, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ScaleTo(right, Vector3.one, duration, 0f, TweenEasings.BounceEaseOut);
            Invoke(nameof(CloseCloseCompleted), duration * 0.6f);
            isOpen = false;
        }

        public void Open()
        {
            Tweener.Instance.ScaleTo(left, new Vector3(0f, 1f, 1f), duration, 0f, TweenEasings.BounceEaseOut);
            Tweener.Instance.ScaleTo(right, new Vector3(0f, 1f, 1f), duration, 0f, TweenEasings.BounceEaseOut);
            Invoke(nameof(OpenCloseCompleted), duration * 0.6f);
            isOpen = true;
        }

        public float GetDuration()
        {
            return duration;
        }

        private void OpenCloseCompleted()
        {
            openCompleted?.Invoke();
        }

        private void CloseCloseCompleted()
        {
            closeCompleted?.Invoke();
        }
    }
}