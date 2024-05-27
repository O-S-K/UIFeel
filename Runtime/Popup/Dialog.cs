using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OSK
{
    public class Dialog : MonoBehaviour
    {
        private bool isShowing;

        public bool IsShowing
        {
            get { return isShowing; }
        }

        private DialogClosed callback;

        public delegate void DialogClosed(bool cancelled, object[] outData);


        public virtual void Initialize()
        {
        }

        public virtual void Show(object[] inData, DialogClosed callback)
        {
            gameObject.SetActive(true);
            this.callback = callback;

            if (isShowing)
            {
                return;
            }

            isShowing = true;
            // GameManager.SwitchGameState(EGameState.Pause);
        }

        public virtual void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Hide();
            }
        }

        public virtual void Hide()
        {
            // GameManager.SetGameStateAfterPause();
            gameObject.SetActive(false);
            isShowing = false;
        }

        public virtual void Destroyed(float timeDelay = 0)
        {
            isShowing = false;
            HidePopup();
            Destroy(gameObject, timeDelay);
        }

        public virtual void HidePopup()
        {
            // GameManager.SetGameStateAfterPause();
            DialogManager.Instance.OnHideDialog(this);
        }

        public virtual void RefreshUI()
        {
        }
    }
}