using UnityEngine;
using TMPro;

public class HighestLevelText : MonoBehaviour
{
    private TextMeshProUGUI highestLevelText;

    private void Awake()
    {
        highestLevelText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        highestLevelText.text = Controller.Instance.highestLevel.ToString();
    }

    private void Update()
    {
        highestLevelText.text = Controller.Instance.highestLevel.ToString();
    }
}