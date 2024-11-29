using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OSK
{
    public class FillText : MonoBehaviour
    {
        public float timeFill = 1;
        public UnityEvent onStartFill;

        public void FillFloatText(Text textFill, float current, float target, float delay = 0, float timeFill = 1, Action onCompleted = null)
        {
            this.timeFill = timeFill;
            StartCoroutine(FillTextTo(elapsedTime =>
            {
                textFill.text = Mathf.RoundToInt(Mathf.Lerp(current, target, elapsedTime / timeFill)).ToString();
            }, delay, onCompleted));
        }

        public void FillFloatText(TextMeshProUGUI textFill, float current, float target, float delay = 0, float timeFill = 1, Action onCompleted = null)
        {
            this.timeFill = timeFill;
            StartCoroutine(FillTextTo(elapsedTime =>
            {
                textFill.text = Mathf.RoundToInt(Mathf.Lerp(current, target, elapsedTime / timeFill)).ToString();
            }, delay, onCompleted));
        }

        private IEnumerator FillTextTo(Action<float> updateText, float delay, Action onCompleted)
        {
            yield return new WaitForSeconds(delay);

            onStartFill?.Invoke();
            float elapsedTime = 0f;
            while (elapsedTime < timeFill)
            {
                updateText(elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            updateText(elapsedTime);
            onCompleted?.Invoke();
        }
    }
}