using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite onModeIcon;
    [SerializeField] private Sprite offModeIcon;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (PlayerPrefs.GetInt("OnSound") == 1)
            image.sprite = onModeIcon;
        else
            image.sprite = offModeIcon;
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("OnSound") == 1)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        PlayerPrefs.SetInt("OnSound", 1);
        AudioManager.Instance.PlaySound("Pop");
        image.sprite = onModeIcon;
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt("OnSound", 0);
        image.sprite = offModeIcon;
    }
}