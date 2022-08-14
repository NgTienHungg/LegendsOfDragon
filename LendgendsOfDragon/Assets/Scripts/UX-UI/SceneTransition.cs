using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    private string nextScene;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        animator.SetTrigger("Change");
        nextScene = sceneName;
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}