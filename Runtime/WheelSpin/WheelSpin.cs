using UnityEngine;
// using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;


[System.Serializable]
public class DataWheelSpin
{
    public List<string> wheelItems; // List of items on the wheel
    public GameObject piecePrefab; // Prefab of the text object
}

public class WheelSpin : MonoBehaviour
{
    public float spinDuration = 2f; // Duration of the spin animation
    public int minSpinCount = 3; // Minimum number of spins
    public int maxSpinCount = 5; // Maximum number of spins

    public DataWheelSpin dataWheelSpin;

    public Text resultText; // Text to display the result

    private bool isSpinning = false; // Flag to prevent multiple spins
    public Transform spinner;
    // public Ease easeType = Ease.OutQuad; // Type of easing for the spin animation

    public float radius = 100f; // Radius of the circle
    public ParticleSystem vfxCompleted;

    public UnityEvent onCompleted;
    
    [ContextMenu("SpawnPieces")]
    public void SpawnPieces()
    {           
        // draw gizmos in angle of each wheel section
        if (dataWheelSpin.wheelItems != null && dataWheelSpin.wheelItems.Count > 0)
        { 
            // Calculate the angle between each text
            float angleStep = 360f / dataWheelSpin.wheelItems.Count;
            // Spawn texts arranged in a circle
            for (int i = 0; i < dataWheelSpin.wheelItems.Count; i++)
            {
                // Calculate the direction for this text
                Vector3 direction = Quaternion.Euler(0, 0, i * angleStep) * Vector3.up;

                // Calculate the direction for the middle piece
                Vector3 nextDirection = Quaternion.Euler(0, 0, (i + 1) * angleStep) * Vector3.up;
                // Calculate the midpoint between the two directions
                Vector3 midpoint = (direction + nextDirection) * 0.5f;

                // Instantiate the middle piece object
                GameObject middlePiece = Instantiate(dataWheelSpin.piecePrefab, spinner);
                // Set the position of the middle piece between the two texts
                middlePiece.transform.localPosition = midpoint * radius;
                middlePiece.GetComponentInChildren<Text>().text = dataWheelSpin.wheelItems[i];
            }
        }
    }


    public void StartSpin()
    {
        if (isSpinning)
            return;
        
        isSpinning = true;
        int spinCount = Random.Range(minSpinCount, maxSpinCount + 1); // Random number of spins
        float totalSpinDuration = spinDuration * spinCount; // Total duration of all spins

        // Calculate the final angle after spinning
        float finalAngle = spinCount * 360f;

        // spinner.DOScale(1.2f, totalSpinDuration / 1.15f).SetDelay(0.1f).SetEase(Ease.InOutSine).OnComplete(() =>
        // {
        //     vfxCompleted.Play();
        //     spinner.DOScale(1f, .25f).SetEase(Ease.OutExpo);
        // });
        // // Perform the spin animation using DOTween
        // spinner.DORotate(new Vector3(0f, 0f, finalAngle), totalSpinDuration, RotateMode.FastBeyond360)
        //     .SetEase(easeType)
        //     .OnComplete(() => FinishSpin(finalAngle)); // Call FinishSpin when the spin is complete
    }

    private void FinishSpin(float finalAngle)
    {
        // Calculate the index of the selected item based on the final angle
        int itemCount = dataWheelSpin.wheelItems.Count;
        float normalizedAngle = finalAngle % 360f;
        float itemAngle = 360f / itemCount;
        int selectedItemIndex = Mathf.FloorToInt(normalizedAngle / itemAngle);

        // Display the result
        string selectedItem = dataWheelSpin.wheelItems[selectedItemIndex];
        resultText.text = "Result: " + selectedItem;

        // Reset the flag to allow another spin
        isSpinning = false;
        // spinner.DOKill();
        onCompleted?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if(!Application.isEditor)
            return;
        // draw gizmos in angle of each wheel section
        if (dataWheelSpin.wheelItems != null && dataWheelSpin.wheelItems.Count > 0)
        {
            for (int i = 0; i < dataWheelSpin.wheelItems.Count; i++)
            {
                Gizmos.color = Color.red;
                Vector3 direction = Quaternion.Euler(0, 0, i * 360f / dataWheelSpin.wheelItems.Count) * Vector3.up;
                Gizmos.DrawRay(spinner.position, direction * 1.5f);
            }
        }
    }
}