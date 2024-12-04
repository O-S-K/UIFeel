using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace OSK
{
    public class ButtonUI : BaseButton, IPointerDownHandler, IPointerUpHandler
    {
        [Space]
        [Header("Button UI")]
        [SerializeField] private Graphic m_Image;
        [SerializeField] private Color colorShow = Color.white;
        [SerializeField] private Color colorPress = Color.gray;


        public TweenSetting DownSetting = new TweenSetting(0.1f, false, AnimationCurve.Linear(0, 0, 1, 1), Ease.OutQuad,
            new Vector3(0.95f, 0.95f, 1), new Vector3(0,0, 7), 7, 10);

        public TweenSetting UpSetting = new TweenSetting(0.1f, false, AnimationCurve.Linear(0, 0, 1, 1), Ease.OutQuad,
            new Vector3(1.02f, 1.02f, 1), Vector3.zero, 3 , 1);

        [Space] [Header("Second Up Setting")]
        [Button("Second Up Setting")]
        public bool NeedSecondUpSetting = true;

        [ShowIf(nameof(NeedSecondUpSetting), true)]
        public TweenSetting SecondUpSetting = new TweenSetting(0.08f, false, AnimationCurve.Linear(0, 0, 1, 1),
            Ease.OutQuad, new Vector3(1, 1, 1), new Vector3(0,0, -5), 5 , 7);
        private bool mIsPress;

        [Space] [Header("Events")] 
        public UnityEvent OnPointerDownEvent;
        public UnityEvent OnPointerUpEvent;


        private void Awake()
        {
            if (m_Image == null)
                m_Image = gameObject.GetComponent<Image>();
        }

        private void OnEnable()
        {
            m_Image.color = colorShow;
            mIsPress = false;
            Recover();
        }

        public void Recover()
        {
            KillTween();
            ApplyTweenReset(DownSetting);
            ApplyTweenReset(UpSetting);
            ApplyTweenReset(SecondUpSetting);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (mIsPress) return;
            mIsPress = true;
            m_Image.color = colorPress;
            OnPointerDownEvent?.Invoke();
            DownAnim();
            
            
            if (playSoundOnClick)
            {
                //Main.Sound.Play("ui_click", false);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!mIsPress) return;
            mIsPress = false;

            m_Image.color = colorShow;
            OnPointerUpEvent?.Invoke();
            UpAnim();
        }

        public void DownAnim()
        {
            AnimInternal(DownSetting);
        }

        public void UpAnim()
        {
            AnimInternal(UpSetting);
            if (NeedSecondUpSetting)
                mTween.OnComplete(() => { AnimInternal(SecondUpSetting); });
        }

        private void AnimInternal(TweenSetting setting)
        {
            KillTween();
            ApplyTweenDown(setting);
        }
    }
}