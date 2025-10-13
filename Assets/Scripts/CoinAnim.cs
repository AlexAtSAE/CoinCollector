using UnityEngine;

[ExecuteAlways]
public class CoinSpinner : MonoBehaviour
{
    [SerializeField] private float offsetHeight = 0;
    [SerializeField] private float bounceMult = 0;
    [SerializeField] private float bounceSpeed = 0;
    [SerializeField] private float rotateSpeed = 0;
    [SerializeField] private Transform parentTransform;


    
    private float elapsedTime = 0f;
  
    void Update()
    {
        
        elapsedTime += Time.deltaTime;
        float ypos = bounceMult*((1+Mathf.Sin(elapsedTime* bounceSpeed))/2f)+offsetHeight;
        transform.position = parentTransform.position+new Vector3(0, ypos, 0);
        float pitch = elapsedTime*10f*rotateSpeed;
        transform.rotation = Quaternion.Euler(90, pitch, 0);

    }
}
