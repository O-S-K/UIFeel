using UnityEngine;
using static BreathingVFX;
using Random = UnityEngine.Random;

public class BreathingVFX : MonoBehaviour
{
    public enum Asix
    {
        X, Y, Z
    }

    public Asix asix;
    public float time = 5f;
    public float mul = 0.1f;

    float clock;

    protected  void Start()
    {
        clock = Random.Range(-5f, 5f);
    }

    protected void Update()
    {
        clock += Time.deltaTime * time;

        switch (asix)
        {
            case Asix.X:
                transform.localScale = new Vector3(1f + Mathf.Sin(clock) * mul, 1f, 1f);
                break;
            case Asix.Y:
                transform.localScale = new Vector3(1, 1f + Mathf.Sin(clock) * mul, 1f);
                break;
                case Asix.Z:
                transform.localScale = new Vector3(1, 1f, 1f + Mathf.Sin(clock) * mul);
                break;
        }
    }
}