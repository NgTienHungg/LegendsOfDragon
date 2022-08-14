using UnityEngine;

public class UIPlayController : MonoBehaviour
{
    public GameObject gamePlayScreen;
    public GameObject gameOverScreen;

    private void Start()
    {
        gamePlayScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void OnGameOver()
    {
        gamePlayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}