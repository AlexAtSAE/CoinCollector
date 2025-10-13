using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public UserSettings userSettings;
    public TextMeshProUGUI slideButtonText;
    public TextMeshProUGUI FOVText; public Slider FOVslider;

    public void Update()
    {

    }
    private float timeout = 0;
    private bool changingSlide;
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
