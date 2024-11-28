using UnityEngine;

public class MeanderVFX : MonoBehaviour
{
    public float pos = 0f;
    public float angle = 5f;
    public float speed = 2f;

    private Quaternion startRotation; 
    private Vector3 startPosition;

    float time;
    
    void Start()
    {
        time = Random.Range(0f, 10f);
        startRotation = transform.rotation;
        startPosition = transform.localPosition;
    }

    void Update()
    {
        time += Time.deltaTime;
        
        float angleChange = Mathf.Sin(time * speed) * angle; 
        Quaternion currentRotation = Quaternion.Euler(0f, 0f, angleChange); 
        transform.rotation = startRotation * currentRotation;

        Vector3 newpos = new Vector3(Mathf.Sin(time * speed) * pos, 0, 0);
        transform.localPosition = startPosition + newpos;
    }
}