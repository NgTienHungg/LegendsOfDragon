using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [Header("UI animation")]
    public RectTransform wheel;
    public RectTransform title;
    public RectTransform buttons;

    public RectTransform dragon;
    public RectTransform yourScore;
    public RectTransform bestScore;

    public RectTransform leftStar;
    public RectTransform midStar;
    public RectTransform rightStar;

    [Header("Handler Event Click")]
    public RectTransform summaryWindow;
    public RectTransform achievementWindow;
    public RectTransform bonusWindow;
    public RectTransform giftWindow;

    [Header("Scene transition")]
    public SceneTransition sceneTransition;

    private void Awake()
    {
        // summary window
        title.anchoredPosition = new Vector3(0f, 300f);
        buttons.anchoredPosition = new Vector3(0f, -250f);

        wheel.transform.localScale = Vector3.zero;
        summaryWindow.transform.localScale = Vector3.zero;
        dragon.transform.localScale = Vector3.zero;

        yourScore.transform.localScale = Vector3.zero;
        bestScore.transform.localScale = Vector3.zero;

        leftStar.transform.localScale = Vector3.zero;
        leftStar.anchoredPosition = new Vector3(-500f, 1000f);

        midStar.transform.localScale = Vector3.zero;
        midStar.anchoredPosition = new Vector3(-500f, 1000f);

        rightStar.transform.localScale = Vector3.zero;
        rightStar.anchoredPosition = new Vector3(-500f, 1000f);

        // windows control
        summaryWindow.anchoredPosition = new Vector3(0f, -80f);
        achievementWindow.anchoredPosition = new Vector3(-1300f, -80f);
        bonusWindow.anchoredPosition = new Vector3(1300f, -80f);
        giftWindow.anchoredPosition = new Vector3(1300f, -80f);
    }

    private void OnEnable()
    {
        LeanTween.scale(summaryWindow, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(dragon, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBack).setDelay(0.3f);

        // score
        LeanTween.scale(yourScore, Vector3.one, 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.6f);
        LeanTween.scale(bestScore, Vector3.one, 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.9f);

        // active stars
        LeanTween.scale(leftStar, Vector3.one, 0.1f).setDelay(1f);
        LeanTween.scale(midStar, Vector3.one, 0.1f).setDelay(1f);
        LeanTween.scale(rightStar, Vector3.one, 0.1f).setDelay(1f);

        // handle star
        if (Controller.Instance.score == 0)
            LeanTween.scale(leftStar, Vector3.one, 0.1f).setDelay(1.5f).setOnComplete(OnCompleteStars);
        else if (Controller.Instance.score < PlayerPrefs.GetInt("BestScore") / 2)
        {
            LeanTween.move(leftStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(1.3f).setOnComplete(OnCompleteStars);
            LeanTween.rotateAround(leftStar, Vector3.forward, -720, 0.8f).setDelay(0.9f);
        }
        else if (Controller.Instance.score < PlayerPrefs.GetInt("BestScore"))
        {
            LeanTween.move(leftStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(1.3f);
            LeanTween.rotateAround(leftStar, Vector3.forward, -720, 0.8f).setDelay(0.9f);

            LeanTween.move(rightStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(2f).setOnComplete(OnCompleteStars);
            LeanTween.rotateAround(rightStar, Vector3.forward, -720, 0.8f).setDelay(1.6f);
        }
        else
        {
            LeanTween.move(leftStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(1.3f);
            LeanTween.rotateAround(leftStar, Vector3.forward, -720, 0.8f).setDelay(0.9f);

            LeanTween.move(rightStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(2f);
            LeanTween.rotateAround(rightStar, Vector3.forward, -720, 0.8f).setDelay(1.6f);

            LeanTween.move(midStar, new Vector3(0, 0), 1f).setEase(LeanTweenType.easeOutBounce).setDelay(2.7f).setOnComplete(OnCompleteStars);
            LeanTween.rotateAround(midStar, Vector3.forward, -720, 0.8f).setDelay(2.3f);
        }
    }

    private void OnCompleteStars()
    {
        // title and buttons
        LeanTween.moveY(title, -380f, 1f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.moveY(buttons, 250f, 1f).setEase(LeanTweenType.easeOutCubic);

        // wheel
        LeanTween.scale(wheel, Vector3.one, 2f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.rotateAround(wheel, Vector3.forward, -360f, 28f).setLoopClamp();
    }

    /*----------------------------------------------------------------------------------------------------*/

    public void OnHomeButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        sceneTransition.LoadScene("Menu");
    }

    public void OnRestartButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        sceneTransition.LoadScene("Play");
    }

    public void OnAchievementButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveX(summaryWindow, 1300f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveX(achievementWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveY(buttons, -200f, 0.5f).setEase(LeanTweenType.easeOutQuart);
    }

    public void OnCloseAchievement()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveX(achievementWindow, -1300f, 0.4f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveX(summaryWindow, 0f, 0.4f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveY(buttons, 250f, 0.3f).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnBonusButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        LeanTween.moveX(summaryWindow, -1300f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveX(bonusWindow, 0f, 0.5f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveY(buttons, -200f, 0.5f).setEase(LeanTweenType.easeOutQuart);
    }

    public void OnCloseBonus()
    {
        AudioManager.Instance.PlaySound("Tap");
        LeanTween.moveX(summaryWindow, 0f, 0.4f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveX(bonusWindow, 1300f, 0.4f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveY(buttons, 250f, 0.3f).setEase(LeanTweenType.easeOutQuad);
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
}