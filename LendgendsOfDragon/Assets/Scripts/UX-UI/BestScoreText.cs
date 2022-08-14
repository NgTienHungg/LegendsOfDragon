using UnityEngine;
using TMPro;

public class BestScoreText : MonoBehaviour
{
    [SerializeField] private string prefix;
    private TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        bestScoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        bestScoreText.text = prefix + PlayerPrefs.GetInt("BestScore").ToString();
    }

    private void Update()
    {
        bestScoreText.text = prefix + PlayerPrefs.GetInt("BestScore").ToString();
    }
}