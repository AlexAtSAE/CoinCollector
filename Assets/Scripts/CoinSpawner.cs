using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    private float timeElapsed = 0;
    private int coinSpawnLimit = 10;
    private int coinsSpawned = 0;
    

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1.0f && coinsSpawned < coinSpawnLimit)
        {
            timeElapsed = 0;
            gameObject.transform.position = new Vector3(Random.Range(-20f,20f), 8f, Random.Range(-20f,20f));
            Instantiate(coinPrefab, transform.position, transform.rotation);
            coinsSpawned++;
        }
    }
}
