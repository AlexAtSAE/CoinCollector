using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    private float timeElapsed = 0;
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1.0f)
        {
            timeElapsed = 0;
            gameObject.transform.position = new Vector3(Random.Range(-20f,20f), 8f, Random.Range(-20f,20f));
            Instantiate(coinPrefab, transform.position, transform.rotation);
            
        }
    }
}
