using UnityEngine;

#region PlayerPref
/*
 * BestScore
 * HighestLevel
 * OnMusic
 * OnSound
 * HaveGift
 * RemoveItem
 * ReplaceItem
 * HeartItem
 */
#endregion

public class GameManager : Singleton<GameManager>
{
    public float timeReceiveGift;
    [HideInInspector] public float timeOnline;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        PlayerPrefs.SetInt("HaveGift", 0);

        timeOnline = 0f;
    }

    private void Update()
    {
        timeOnline += Time.deltaTime;
    }
}