using UnityEngine;

public class DisposableAnimation : MonoBehaviour
{
    // use for AddKey in the last frame of animation
    public void OnCompleteAnimation()
    {
        Destroy(gameObject);
    }
}