using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public int AmountCollected;
    public RawImage winscreen;
    public TextMeshProUGUI TextBlock;
    public void Start()
    {
        winscreen.enabled = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("Objective"))
        {
            AmountCollected++;
            Debug.Log($"Hit a coin! {AmountCollected} Collected");
            Destroy(otherObject); 
            TextBlock?.SetText($"Coins collected: {AmountCollected}");

            if (AmountCollected >= 5)
            {
                winscreen.enabled = true;
            }
        }
    }
}

