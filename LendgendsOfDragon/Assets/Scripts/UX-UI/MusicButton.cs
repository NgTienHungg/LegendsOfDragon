using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Sprite onModeIcon;
    [SerializeField] private Sprite offModeIcon;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (PlayerPrefs.GetInt("OnMusic") == 1)
            image.sprite = onModeIcon;
        else
            image.sprite = offModeIcon;
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("OnMusic") == 1)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        PlayerPrefs.SetInt("OnMusic", 1);
        AudioManager.Instance.PlaySound("Pop");
        AudioManager.Instance.ContinuePlayMusic();
        image.sprite = onModeIcon;
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt("OnMusic", 0);
        AudioManager.Instance.StopAllMusic();
        image.sprite = offModeIcon;
    }
}