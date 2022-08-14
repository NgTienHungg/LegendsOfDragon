using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private GameObject dragon;
    [SerializeField] private Sprite unlockSpr, notUnlockSpr;
    private Image tile;

    private void Awake()
    {
        tile = GetComponent<Image>();

        if (!PlayerPrefs.HasKey("HighestLevel"))
            PlayerPrefs.SetInt("HighestLevel", 1);
    }

    private void OnEnable()
    {
        if (level <= PlayerPrefs.GetInt("HighestLevel"))
        {
            tile.sprite = unlockSpr;
            dragon.SetActive(true);
        }
        else
        {
            tile.sprite = notUnlockSpr;
            dragon.SetActive(false);
        }
    }
}