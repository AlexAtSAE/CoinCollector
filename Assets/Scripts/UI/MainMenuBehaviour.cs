using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public Canvas Options;
    public Canvas Play;
    private Canvas thisCanvas;
    public void Start()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.enabled = true;
    }
    public void PressedPlay()
    {
        Play.enabled = true;
        thisCanvas.enabled = false;
        //SceneManager.LoadScene("Game");
    }
    public void PressedOptions()
    {
        Options.enabled = true;
        thisCanvas.enabled = false;
    }
    public void PressedQuit()
    {
        Application.Quit();
    }
}
