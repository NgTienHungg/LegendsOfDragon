using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private Sprite lockGift;
    [SerializeField] private Sprite unLockGift;
    [SerializeField] TextMeshProUGUI timeText;

    private Image image;
    private int timeRemaining;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        timeRemaining = (int)(GameManager.Instance.timeReceiveGift - GameManager.Instance.timeOnline);

        if (PlayerPrefs.GetInt("HaveGift") == 1 || timeRemaining <= 0)
        {
            PlayerPrefs.SetInt("HaveGift", 1);
            image.sprite = unLockGift;
            timeText.text = "";
        }
        else
        {
            timeText.text = "00:0" + (timeRemaining / 60).ToString() + ":";
            timeText.text += (timeRemaining % 60 >= 10) ? (timeRemaining % 60).ToString() : ("0" + (timeRemaining % 60).ToString());
            image.sprite = lockGift;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("HaveGift") == 0)
        {
            timeRemaining = (int)(GameManager.Instance.timeReceiveGift - GameManager.Instance.timeOnline);
            timeText.text = "00:0" + (timeRemaining / 60).ToString() + ":";
            timeText.text += (timeRemaining % 60 >= 10) ? (timeRemaining % 60).ToString() : ("0" + (timeRemaining % 60).ToString());
            image.sprite = lockGift;

            if (timeRemaining <= 0)
                PlayerPrefs.SetInt("HaveGift", 1);
        }

        if (PlayerPrefs.GetInt("HaveGift") == 1)
        {
            image.sprite = unLockGift;
            timeText.text = "";
        }
    }
}