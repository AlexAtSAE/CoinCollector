using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int AmountCollected;
    private void OnCollisionStay(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("Objective"))
        {
            AmountCollected++;
            Debug.Log($"Hit a coin! {AmountCollected} Collected");
            Destroy(otherObject);
        }
    }
}

