using UnityEngine;
using TMPro;

public class GiftButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notification;

    private void Update()
    {
        if (PlayerPrefs.GetInt("HaveGift") == 1)
        {
            notification.text = "Click!!!";
        }
        else
            notification.text = "Wait!!!";
    }
}