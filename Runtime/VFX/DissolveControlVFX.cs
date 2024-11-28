using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DissolveControlVFX : MonoBehaviour
{
    private Material dissolveMaterialInstance;

    [Range(0f, 1f)]
    public float dissolveValue = 0.0f;
    public Texture texture; // Public texture to be set via Editor or scripts

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Image image = GetComponent<Image>();
        RawImage rawImage = GetComponent<RawImage>();

        if (renderer != null)
        {
            dissolveMaterialInstance = new Material(renderer.sharedMaterial);
            renderer.material = dissolveMaterialInstance;
        }
        else if (spriteRenderer != null)
        {
            dissolveMaterialInstance = new Material(spriteRenderer.sharedMaterial);
            spriteRenderer.material = dissolveMaterialInstance;
        }
        else if (image != null)
        {
            dissolveMaterialInstance = new Material(image.material);
            image.material = dissolveMaterialInstance;
        }
        else if (rawImage != null)
        {
            dissolveMaterialInstance = new Material(rawImage.material);
            rawImage.material = dissolveMaterialInstance;
        }
        else
        {
            Debug.LogWarning("DissolveControl: No compatible renderer found on the GameObject.", this);
        }
    }

    void Update()
    {
        if (dissolveMaterialInstance != null)
        {
            dissolveMaterialInstance.SetFloat("_DissolveThreshold", dissolveValue);

            // Check if the texture is not null and set it
            if (texture != null)
            {
                dissolveMaterialInstance.SetTexture("_NoiseTex", texture);
            }
        }
    }
}