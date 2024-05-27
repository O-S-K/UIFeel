using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SpawnCoinImage : MonoBehaviour
{
    public Canvas canvas;
    public GameObject coinPrefab;
    public Transform startPoint;
    public Transform endPoint;

    [Space] public Vector2 randomX;
    public Vector2 randomY;


    [Space] public float timeDrop = 1;
    public float speedDrop = .4f;

    [Space] public float timeFly = 1f;
    public float speedFly = 8f;

    [Space] public int numberOfCoins = 10;
    public float delayMove = 0.3f;

    [Space] public float timeDestroyed = 2;

    public UnityEvent onCompleted;

    private GameObject coinParent;


    private void Start()
    {
        coinPrefab.SetActive(false);
    }

    public void SpawnCoins()
    {
        if (coinParent == null)
            StartCoroutine(DelaySpawnCoins());
    }


    private IEnumerator DelaySpawnCoins()
    {
        coinParent = new GameObject("CoinParent");
        coinParent.transform.parent = canvas.transform;
        coinParent.transform.localPosition = Vector3.zero;
        coinParent.transform.localScale = Vector3.one;
        Destroy(coinParent, timeDestroyed);

        float timeDlaySpawn = 0;

        for (int i = 0; i < numberOfCoins; i++)
        {
            var coinClone = Instantiate(coinPrefab, startPoint.position, Quaternion.identity, coinParent.transform);
            coinClone.gameObject.SetActive(true);
            StartCoroutine(DropCoins(coinClone));
            
            float randomTime = Random.Range(0.01f, 0.05f);
            timeDlaySpawn += randomTime;
            yield return new WaitForSeconds(randomTime);
            StartCoroutine(MoveCoinToTarget(coinClone.transform));
        }

        yield return new WaitForSeconds(timeDlaySpawn);
        Oncompleted();
    }


    private IEnumerator DropCoins(GameObject coin)
    {
        float timer = 0f;
        Vector3 startPos = coin.transform.position;
        Vector3 randomOffset = new Vector2(Random.Range(randomX.x, randomX.y), Random.Range(randomY.x, randomY.y));
        Vector3 spawnPos = startPoint.position + randomOffset;
        while (timer < timeDrop)
        {
            float t = timer / speedDrop;
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, spawnPos, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveCoinToTarget(Transform coinTransform)
    {
        float timer = 0f;
        yield return new WaitForSeconds(delayMove + Random.Range(0.01f, 0.05f));
        while (timer < timeFly)
        {
            float t = timer / speedFly;
            if (coinTransform != null && coinTransform.gameObject.activeInHierarchy)
                coinTransform.position = Vector3.MoveTowards(coinTransform.position, endPoint.position, t);
            timer += Time.deltaTime;
            yield return null;
        }

        if (coinTransform != null && coinTransform.gameObject.activeInHierarchy)
            coinTransform.position = endPoint.position;
    }

    private void Oncompleted()
    {
        Debug.Log("Gold Move Completed!");
        onCompleted?.Invoke();
    }
}