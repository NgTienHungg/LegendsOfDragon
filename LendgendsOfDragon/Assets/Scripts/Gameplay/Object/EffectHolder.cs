using UnityEngine;

public enum EffectType
{
    Smoke,
    Blink
}

public class EffectHolder : Singleton<EffectHolder>
{
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject blink;

    public void Play(EffectType type, Vector3 position, Transform parent)
    {
        if (type == EffectType.Smoke)
            Instantiate(smoke, position, Quaternion.identity, parent);
        else if (type == EffectType.Blink)
            Instantiate(blink, position, Quaternion.identity, parent);
        else
            Debug.LogWarning("Effect is invalid!");
    }
}