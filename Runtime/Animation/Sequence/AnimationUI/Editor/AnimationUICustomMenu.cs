#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace OSK
{
    public class AnimationUICustomMenu
    {
        [MenuItem("OSK-Framework/UI/Create AnimationUI")]
        static void CreateAnimationUI(MenuCommand menuCommand)
        {
            GameObject selected = Selection.activeGameObject;
            GameObject createdGo = new GameObject("AnimationUI");
            createdGo.AddComponent<AnimationUI>();
            GameObjectUtility.SetParentAndAlign(createdGo, selected);
            Undo.RegisterCreatedObjectUndo(createdGo, "Created +" + createdGo.name);
        }
    }
}
#endif