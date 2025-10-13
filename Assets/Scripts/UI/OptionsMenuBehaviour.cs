using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public UserSettings userSettings;
    public TextMeshProUGUI slideButtonText;
    public TextMeshProUGUI FOVText; public Slider FOVslider;
    public TextMeshProUGUI XText; public Slider Xslider;
    public TextMeshProUGUI YText; public Slider Yslider;
    public Canvas MainMenu;

    private float timeout = 0;
    private bool changingSlide;

    private Canvas thisCanvas;
    public void Start()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.enabled = false;
    }
    public void SetSlideButton()
    {
        changingSlide = true;
    }
    public void SetFOV()
    {
        float Res = Mathf.Floor(Mathf.Lerp(60, 90, FOVslider.value));
        userSettings.FOV = Res;
        FOVText.SetText($"{Res}");
    }
    public void onPressBack()
    {
        MainMenu.enabled = true;
        thisCanvas.enabled = false;
    }

    public void changeXSensitivity()
    {
        float val = Xslider.value;
        float min = 10, max = 1000;
        float res = Mathf.Lerp(min,max,val);
        XText.SetText($"{Mathf.Floor(res)}");
        userSettings.mouseSensitivity.x = res;
    }
    public void changeYSensitivity()
    {
        float val = Yslider.value;
        float min = 10, max = 1000;
        float res = Mathf.Lerp(min, max, val);
        YText.SetText($"{Mathf.Floor(res)}");
        userSettings.mouseSensitivity.y = res;
    }


    public void OnGUI()
    {
        if (changingSlide && Event.current.type == EventType.KeyDown)
        {
            KeyCode k = Event.current.keyCode;
            userSettings.SlideKey = k;
            slideButtonText.text = k.ToString();
            changingSlide = false;
            return;
        }
    }

}
