using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace OSK
{
    public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Text text;
        public Appearer appearer;
        public Image bg;
        public Color normalColor, hoverColor;

        // public string activatesScene;
        public UnityEvent action;

        public bool isHidesOnClick = false;

        public bool isAnimEnter = true;
        public bool isChangeColor = true;

        public bool isRotating = true;
        public bool isScaler = true;
        public float scaleHover = 1.2f;

        private bool done;
        private bool hovered;


        private void OnValidate()
        {
            if (Application.isEditor && isChangeColor)
            {
                bg.color = normalColor;
                text.color = normalColor;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (done) return;

            if (isHidesOnClick)
            {
                appearer.Hide();
                done = true;
            }

            // if (!string.IsNullOrEmpty(activatesScene))
            //     SceneChanger.Instance.ChangeScene(activatesScene);

            if (action != null) action.Invoke();

            if (isScaler)
                Tweener.Instance.ScaleTo(transform, Vector3.one * scaleHover, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
            //Invoke("RemoveHover", 0.25f);

            AudioManager.Instance.Lowpass(false);
            AudioManager.Instance.Highpass(false);
        }

        private void RemoveHover()
        {
            hovered = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (done) return;
            hovered = true;

            if (isAnimEnter)
            {
                Tweener.Instance.ScaleTo(transform, Vector3.one * 1.1f, 0.2f, 0f, TweenEasings.BounceEaseOut);
                Tweener.Instance.ScaleTo(text.transform, Vector3.one * 0.9f, 0.3f, 0f, TweenEasings.BounceEaseOut);
            }

            if (isChangeColor)
            {
                bg.color = hoverColor;
                text.color = hoverColor;
            }

            if (isRotating)
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (done) return;

            hovered = false;
            Tweener.Instance.ScaleTo(transform, Vector3.one, 0.2f, 0f, TweenEasings.QuadraticEaseOut);
            Tweener.Instance.ScaleTo(text.transform, Vector3.one, 0.1f, 0f, TweenEasings.QuadraticEaseOut);

            if (isChangeColor)
            {
                bg.color = normalColor;
                text.color = normalColor;
            }

            if (isRotating)
                transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void OnDisable()
        {
            hovered = false;
            if (isChangeColor)
            {
                bg.color = normalColor;
                text.color = normalColor;
            }

            if (isRotating)
                transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public bool IsHovered()
        {
            return hovered;
        }
    }
}