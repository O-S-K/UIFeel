using UnityEngine;

public class NoiseVFX : MonoBehaviour
{
    public float strength = 0.1f;
    public float duration;
    private float currentDuration;


    private void Start()
    {
        currentDuration = duration;
    }

    void Update()
    {
        if (currentDuration <= 0)
        {
            currentDuration = duration;
            transform.localPosition = Random.insideUnitCircle * strength;
        }
        currentDuration -= Time.deltaTime;
    }
}