using UnityEngine;
using UnityEngine.UI;

public class HighestLevelImage : MonoBehaviour
{
    [SerializeField] private Sprite[] dragonSprites;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        image.sprite = dragonSprites[Controller.Instance.highestLevel - 1];
    }
}