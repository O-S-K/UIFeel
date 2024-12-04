using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace OSK
{
    public class ButtonEffect : BaseButton, IPointerDownHandler, IPointerUpHandler
    {
        [Space] [Header("Settings")] public TweenSetting setting = new TweenSetting(0.1f, false,
            AnimationCurve.Linear(0, 0, 1, 1), Ease.OutQuad,
            new Vector3(0.95f, 0.95f, 1), new Vector3(0, 0, 10), 7f, 5);

        public bool IsPressed { get; private set; }

        private void OnEnable()
        {
            KillTween();
            ApplyTweenReset(setting);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetComponent<Button>() != null && GetComponent<Button>().interactable == false)
                return;

            if (playSoundOnClick)
            {
                //Main.Sound.Play("ui_click", false);
            }

            KillTween();
            ApplyTweenDown(setting);
            IsPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = defaultSize;
            transform.localPosition = defaultPosition;
            transform.localEulerAngles = defaultRotation;
            
            KillTween();
            ApplyTweenReset(setting);
            IsPressed = false;
        }
    }
}