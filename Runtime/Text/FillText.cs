using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OSK
{
    public class FillText : MonoBehaviour
    {
        public Text textFill;
        public float timeFill = 1;
        public float delayFill = 0;

        [Space]
        public bool isScaleToFill = true;
        public float scaleTextFill = 1.25f;
        // public TweenEasings.Easings Easings;
        
        public UnityEvent onStartFill;

        private float target;

        private void OnEnable()
        {
            textFill = gameObject.GetOrAdd<Text>();
        }

        public void FillNumberTextDemo()
        {
            FillFloatText(0, 1000, delayFill, 1, () => { Debug.Log("Fill Text Completed!"); });
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            textFill.text = target.ToString();
        }

        public void FillFloatText(float current, float target, float delay = 0, float timeFill = 1,
            Action onCompleted = null)
        {
            this.timeFill = timeFill;
            this.delayFill = delay;
            StartCoroutine(FillTextTo(current, target, delay, onCompleted));
        }

        private IEnumerator FillTextTo(float current, float target, float delay = 0, Action onCompleted = null)
        {
            this.target = target;
            yield return new WaitForSeconds(delay);

            onStartFill?.Invoke();

            if (isScaleToFill)
                Tweener.Instance.ScaleTo(textFill.transform, Vector3.one * scaleTextFill, timeFill, delay,
                    TweenEasings.GetEasing(TweenEasings.Easings.BounceEaseOut));

            float elapsedTime = 0f;
            while (elapsedTime < timeFill)
            {
                int value = Mathf.RoundToInt(Mathf.Lerp(current, target, elapsedTime / timeFill));
                textFill.text = value.ToString();

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (isScaleToFill)
                Tweener.Instance.ScaleTo(textFill.transform, Vector3.one, .1f, 0f,
                    TweenEasings.GetEasing(TweenEasings.Easings.BounceEaseIn));
            
            textFill.text = target.ToString();
            onCompleted?.Invoke();
        }
    }
}