using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void PressedPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void PressedOptions()
    {
        Debug.Log("TODO");
    }
    public void PressedQuit()
    {
        Application.Quit();
    }
}
