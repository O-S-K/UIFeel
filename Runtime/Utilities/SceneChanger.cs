using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OSK
{
    public class SceneChanger : MonoBehaviour
    {
        public Blinders blinders;
        public Transform spinner;

        public string sceneToLoadAtStart;

        public Canvas canvas;

        private string sceneToLoad;
        private AsyncOperation operation;

        private static SceneChanger instance = null;

        public static SceneChanger Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(instance.gameObject);
        }

        private void Start()
        {
            if (!string.IsNullOrEmpty(sceneToLoadAtStart))
                ChangeScene(sceneToLoadAtStart);
        }

        public void AttachCamera()
        {
            canvas.worldCamera = Camera.main;
        }

        private void Update()
        {
            if (operation != null && operation.isDone)
            {
                operation = null;
                Invoke("After", 0.1f);
            }
        }

        void After()
        {
            blinders.Open();
            Tweener.Instance.ScaleTo(spinner, Vector3.zero, 0.2f, 0f, TweenEasings.QuadraticEaseIn);
        }

        public void ChangeScene(string sceneName)
        {
            blinders.Close();
            Tweener.Instance.ScaleTo(spinner, Vector3.one, 0.2f, 0f, TweenEasings.BounceEaseOut);
            sceneToLoad = sceneName;
            Invoke(nameof(DoChangeScene), blinders.GetDuration());
        }

        void DoChangeScene()
        {
            operation = SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}