using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBehaviour : MonoBehaviour
{
    private Canvas thisCanvas;

    [SerializeField] private Canvas mainMenuCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisCanvas = gameObject.GetComponent<Canvas>();
        thisCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelect(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void PressedBack()
    {
        thisCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
}
