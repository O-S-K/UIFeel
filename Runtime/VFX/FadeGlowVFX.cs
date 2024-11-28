using UnityEngine;

public class FadeGlowVFX : MonoBehaviour
{
    public float speed = 0.2f;
    public float from = 0f;
    public float to = 1f;

    public SpriteRenderer sr;

    void Update()
    {
        var srColor = sr.color;
        srColor.a = Remap(Mathf.Abs(Mathf.Sin(Time.time * speed)), -1f, 1f, from, to);
        sr.color = srColor;
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}