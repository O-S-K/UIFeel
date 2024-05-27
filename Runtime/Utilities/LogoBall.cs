using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoBall : MonoBehaviour
{
    public float speed = 5f;
    public float offset = 5f;
    public float distance = 5f;
    private bool noNegatives = false;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        float sinVal = Mathf.PerlinNoise(Time.time * speed + offset * Mathf.PI, offset) - distance * 0.5f;
        float xVal = Mathf.PerlinNoise(Time.time * speed + offset * Mathf.PI, offset * 10f) - distance * 0.5f;
        //		sinVal = noNegatives ? Mathf.Abs (sinVal) : sinVal;
        var mod = 1.25f;
        transform.localPosition = originalPosition + Vector3.up * sinVal * distance * mod +
                                  Vector3.right * xVal * distance * mod;
    }
}