using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreText.text = Controller.Instance.score.ToString();
    }

    private void Update()
    {
        scoreText.text = Controller.Instance.score.ToString();
    }
}