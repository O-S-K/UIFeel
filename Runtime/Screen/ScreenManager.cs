using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OSK
{
    public class ScreenManager : SingletonMono<ScreenManager>
    {
        [ContextMenu("GetOrAdd_AllScreens")]
        public void GetAllScreenForChild()
        {
            Screens = new List<UIScreen>();
            for (int i = 0; i < transform.childCount; i++)
            {
                UIScreen screen = transform.GetChild(i).GetComponent<UIScreen>();
                if (screen != null && !Screens.Contains(screen))
                {
                    screen.gameObject.name = screen.GetType().Name;
                    Screens.Add(screen);
                }
            }
        }

        public List<UIScreen> Screens = null;

        // Screen id back stack
        private List<UIScreen> backStack;

        // The screen that is currently being shown
        public UIScreen currentScreen;

        private bool isAnimating;

        public UIScreen CurrentScreen
        {
            get { return currentScreen; }
        }

        /// <summary>
        /// Invoked when the ScreenController is transitioning from one screen to another. The first argument is the current showing screen id, the
        /// second argument is the screen id of the screen that is about to show (null if its the first screen). The third argument id true if the screen
        /// that is being show is an overlay
        /// </summary>
        public System.Action<UIScreen, UIScreen> OnSwitchingScreens;

        /// <summary>
        /// Invoked when ShowScreen is called
        /// </summary>
        public System.Action<UIScreen> OnShowingScreen;

        public void Setup()
        {
            backStack = new List<UIScreen>();

            // Initialize and hide all the screens
            for (int i = 0; i < Screens.Count; i++)
            {
                UIScreen screen = Screens[i];
                screen.Initialize();
                screen.gameObject.SetActive(true);
                screen.Hide();
            }
        }

        public void Back()
        {
            if (backStack.Count <= 0)
            {
                Debug.LogWarning($"[ScreenController] There is no screen on the back stack to go back to.");
                return;
            }

            // Get the screen id for the screen at the end of the stack (The last shown screen)
            UIScreen screenBack = backStack[backStack.Count - 1];
            // Remove the screen from the back stack
            backStack.RemoveAt(backStack.Count - 1);
            // Show the screen
            screenBack.Show();
        }

        public T Show<T>(bool back = false, bool immediate = true) where T : UIScreen
        {
            // Get the screen we want to show
            UIScreen screen = GetScreen<T>();
            if (screen == null)
            {
                Debug.LogError($"[ScreenController] Could not find screen in list screens ");
                return null;
            }

            Debug.Log("[ScreenController] Showing screen " + screen.name);

            // Check if there is a current screen showing
            if (currentScreen != null)
            {
                // Hide the current screen
                currentScreen.Hide();

                if (!back)
                {
                    // Add the screens id to the back stack
                    backStack.Add(currentScreen);
                }

                OnSwitchingScreens?.Invoke(currentScreen, screen);
            }

            // Show the new screen
            screen.Show();

            // Set the new screen as the current screen
            currentScreen = screen;
            OnShowingScreen?.Invoke(screen);
            return currentScreen as T;
        }

        public void RefreshUI()
        {
            if (Screens != null)
                foreach (var item in Screens)
                    item.RefreshUI();
        }

        private void ClearBackStack()
        {
            backStack.Clear();
        }

        public T GetScreen<T>() where T : UIScreen
        {
            foreach (var item in Screens)
                if (item is T)
                    return item as T;
            Debug.LogError($"[ScreenTransitionController] No Screen exists in List Screens");
            return null;
        }
    }
}