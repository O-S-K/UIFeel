using System.Collections;
using UnityEngine;

namespace Animations
{
    public class FlashVFX : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float duration;

        public float Duration => duration;

        private Material originalMaterial;
        private Coroutine flashRoutine;

        [ContextMenu("Flash")]
        public void Flash()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private void Start()
        {
            originalMaterial = spriteRenderer.material;
        }

        private IEnumerator FlashRoutine()
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(duration);
            spriteRenderer.material = originalMaterial;
            flashRoutine = null;
        }
    }
}