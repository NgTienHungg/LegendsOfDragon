using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    private TextMeshProUGUI message;
    private Animator animator;

    private void Awake()
    {
        message = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    public void Notify(string mes)
    {
        message.text = mes;
        animator.SetTrigger("Enable");
    }
}
