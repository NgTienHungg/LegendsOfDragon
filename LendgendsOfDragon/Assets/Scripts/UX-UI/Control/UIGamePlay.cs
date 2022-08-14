using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGamePlay : MonoBehaviour
{
    public Notification notification;
    public Slider timeRemainingSlider;

    public TextMeshProUGUI removeOptionText;
    public TextMeshProUGUI replaceOptionText;

    [Header("Dialogs")]
    public GameObject backHomePanel;
    public GameObject restartPanel;
    public GameObject pausePanel;
    public GameObject helpPanel;

    [Header("Scene transition")]
    public SceneTransition sceneTransition;

    private void Update()
    {
        if (Controller.Instance.isPlaying)
            DisplayTimeRemaining();

        UpdateOptions();
    }

    public void NotifyCantMoveAnymore()
    {
        notification.GetComponent<Animator>().SetTrigger("Enable");
    }

    private void DisplayTimeRemaining()
    {
        timeRemainingSlider.value = Controller.Instance.timeRemaining / Controller.Instance.timeOneTurn;
    }

    private void UpdateOptions()
    {
        removeOptionText.text = PlayerPrefs.GetInt("RemoveItem").ToString();
        replaceOptionText.text = PlayerPrefs.GetInt("ReplaceItem").ToString();
    }

    /*----------------------------------------------------------------------------------------------------*/

    public void OnHomeButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        backHomePanel.SetActive(true);
        Controller.Instance.isPlaying = false;
    }

    public void OnYesHomeDialog()
    {
        AudioManager.Instance.PlaySound("Tap");
        sceneTransition.LoadScene("Menu");
    }

    public void OnNoHomeDialog()
    {
        AudioManager.Instance.PlaySound("Tap");
        backHomePanel.SetActive(false);
        Controller.Instance.isPlaying = true;
    }

    public void OnRestartButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        restartPanel.SetActive(true);
        Controller.Instance.isPlaying = false;
    }

    public void OnYesRestartDialog()
    {
        AudioManager.Instance.PlaySound("Tap");
        sceneTransition.LoadScene("Play");
    }

    public void OnNoRestartDialog()
    {
        AudioManager.Instance.PlaySound("Tap");
        restartPanel.SetActive(false);
        Controller.Instance.isPlaying = true;
    }

    public void OnPauseButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        pausePanel.SetActive(true);
        Controller.Instance.isPlaying = false;
    }

    public void OnClosePause()
    {
        AudioManager.Instance.PlaySound("Tap");
        pausePanel.SetActive(false);
        Controller.Instance.isPlaying = true;
    }

    public void OnHelpButton()
    {
        AudioManager.Instance.PlaySound("Pop");
        helpPanel.SetActive(true);
        Controller.Instance.isPlaying = false;
    }

    public void OnCloseHelp()
    {
        AudioManager.Instance.PlaySound("Tap");
        helpPanel.SetActive(false);
        Controller.Instance.isPlaying = true;
    }

    public void OnShareButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }

    /*----------------------------------------------------------------------------------------------------*/

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

    /*----------------------------------------------------------------------------------------------------*/

    public void OnRemoveButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }

    public void OnReplaceButton()
    {
        AudioManager.Instance.PlaySound("Pop");
    }
}