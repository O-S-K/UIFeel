using UnityEngine;

public class RestartIntervalVFX : MonoBehaviour
{
    public float interval = 1f;
    public bool isActivated = false;
    public GameObject target;
    private float clock = 0f;

    private void Update()
    {
        clock += Time.deltaTime;

        if (clock > interval)
        {
            clock = 0;
            isActivated = !isActivated;
            target.SetActive(isActivated);
        }
    }
}