using System.Collections.Generic;
using UnityEngine;

namespace OSK
{
    public class DialogManager : SingletonMono<DialogManager>
    {
        public List<Dialog> Dialogs = null;
        public Transform canvas;

        [ContextMenu("GetOrAdd_AllDialogs")]
        public void GetAllDialogsForChild()
        {
            Dialogs = new List<Dialog>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Dialog dialog = transform.GetChild(i).GetComponent<Dialog>();
                if (dialog != null && !Dialogs.Contains(dialog))
                {
                    dialog.gameObject.name = dialog.GetType().Name;
                    Dialogs.Add(dialog);
                }
            }
        }
        
        public void Setup()
        {
            for (int i = 0; i < Dialogs.Count; i++)
            {
                Dialogs[i].Initialize();
            }
        }

        public T Show<T>(object[] inData = null, bool isHideAllDialog = true, Dialog.DialogClosed dialogClosed = null) where T : Dialog
        {
            Dialog dialog = Get<T>();

            if (isHideAllDialog)
            {
            }

            if (dialog != null)
            {
                dialog.Show(inData, dialogClosed);
            }
            else
            {
                Debug.LogErrorFormat($"[PopupController] Popup does not exist");
            }
            return (T)dialog;
        }

        public T LoadResourceShow<T>(string pathDialog = "", object[] inData = null, bool isHideAllDialog = true, Dialog.DialogClosed dialogClosed = null) where T : Dialog
        {
            if (isHideAllDialog)
            {
            }

            T popup = (T)Instantiate(Resources.Load<T>(pathDialog), canvas);
            popup.name = pathDialog;
            popup.Show(inData, dialogClosed);
            Dialogs.Add(popup);
            return popup;
        }

        public void OnHideDialog(Dialog dialog)
        {
            for (int i = Dialogs.Count - 1; i >= 0; i--)
            {
                if (dialog == Dialogs[i])
                {
                    Dialogs.RemoveAt(i);
                    break;
                }
            }
        }

        public void RefreshUI()
        {
            if(Dialogs != null)
            {
                foreach (var item in Dialogs)
                {
                    item.RefreshUI();
                }
            }
        }

        public T Get<T>() where T : Dialog
        {
            foreach (var item in Dialogs)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
    }
}