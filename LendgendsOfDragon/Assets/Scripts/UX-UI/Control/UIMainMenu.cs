using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public RectTransform gameTitle;
    public RectTransform buttonGroup;

    [Header("Windows")]
    public RectTransform achievementWindow;
    public RectTransform settingWindow;
    public RectTransform bonusWindow;
    public RectTransform helpWindow;
    public RectTransform giftWindow;

    public SceneTransition sceneTransition;

    private void Start()
    {
        gameTitle.anchoredPosition = new Vector3(0f, -600f);
        buttonGroup.anchoredPosition = new Vector3(0f, 260f);

        achievementWindow.anchoredPosition = new Vector3(-1300f, -250f);
        settingWindow.anchoredPosition = new Vector3(0f, -2000f);
        bonusWindow.anchoredPosition = new Vector3(1300, -250f);
        helpWindow.anchoredPosition = new Vector3(1300f, -250);
        giftWindow.anchoredPosition = new Vector3(1300f, -250f);
    }

    public void OnPlayButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        sceneTransition.LoadScene("Play");
    }

    public void OnAchievementButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveY(gameTitle, -480f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveX(achievementWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart).setDelay(0.1f);
        LeanTween.moveY(buttonGroup, -260f, 0.5f).setEase(LeanTweenType.easeOutQuart);
    }

    public void OnCloseAchievement()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveY(gameTitle, -600f, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.2f);
        LeanTween.moveX(achievementWindow, -1300f, 0.3f).setEase(LeanTweenType.easeInBack);
        LeanTween.moveY(buttonGroup, 260f, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.15f);
    }

    public void OnSettingButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveY(gameTitle, -480f, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.1f);
        LeanTween.moveY(settingWindow, -250f, 0.3f).setEase(LeanTweenType.easeOutCirc).setDelay(0.1f);
        LeanTween.scale(buttonGroup, Vector3.zero, 0.3f);
    }

    public void OnCloseSetting()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveY(gameTitle, -600f, 0.5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveY(settingWindow, -2000f, 0.6f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(buttonGroup, Vector3.one, 0.2f).setDelay(0.05f);
    }

    public void OnHelpButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveY(gameTitle, -480f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveX(helpWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart).setDelay(0.1f);
        LeanTween.moveY(buttonGroup, -260f, 0.5f).setEase(LeanTweenType.easeOutQuart);
    }

    public void OnCloseHelp()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveY(gameTitle, -600f, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.2f);
        LeanTween.moveX(helpWindow, 1300f, 0.3f).setEase(LeanTweenType.easeInBack);
        LeanTween.moveY(buttonGroup, 260f, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.15f);
    }

    public void OnBonusButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveY(gameTitle, -480f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveX(bonusWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart).setDelay(0.1f);
        LeanTween.moveY(buttonGroup, -260f, 0.5f).setEase(LeanTweenType.easeOutQuart);
    }

    public void OnCloseBonus()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveY(gameTitle, -600f, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.2f);
        LeanTween.moveX(bonusWindow, 1300f, 0.3f).setEase(LeanTweenType.easeInBack);
        LeanTween.moveY(buttonGroup, 260f, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.15f);
    }

    public void OnGiftButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        if (PlayerPrefs.GetInt("HaveGift") == 1)
        {
            FindObjectOfType<GiftCardManager>().Renew();
            LeanTween.moveX(bonusWindow, -1300f, 0.5f).setEase(LeanTweenType.easeOutQuart);
            LeanTween.moveX(giftWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        }
    }

    public void OnCloseGift()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveX(bonusWindow, 0f, 0.4f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveX(giftWindow, 1300f, 0.4f).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnArcadeButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }

    public void OnFlashButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }

    public void OnShareButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }

    public void OnFacebookButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        Application.OpenURL("https://facebook.com/NgTienHungg");
    }

    public void OnStarButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        Application.OpenURL("https://github.com/NgTienHungg");
    }

    public void OnInfoButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        Application.OpenURL("https://www.facebook.com/supergamestudio");
    }

    public void OnNoAdsButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }
}