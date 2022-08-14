using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GiftCard : MonoBehaviour
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite faceUpSprite;
    [SerializeField] private Sprite postUpSprite;

    [SerializeField] private Image item;
    [SerializeField] private Sprite removeItem;
    [SerializeField] private Sprite replaceItem;
    [SerializeField] private Sprite heartItem;

    private GiftCardManager giftCardManager;
    private Image image;
    private bool isChosing;
    private int idItem; // 0: remove, 1: replace, 2: heart

    private void Start()
    {
        giftCardManager = GetComponentInParent<GiftCardManager>();
        image = GetComponent<Image>();
    }

    public void Renew()
    {
        image.sprite = idleSprite;
        isChosing = false;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        item.gameObject.SetActive(false);
        idItem = Random.Range(0, 3);
        if (idItem == 0)
            item.sprite = removeItem;
        else if (idItem == 1)
            item.sprite = replaceItem;
        else if (idItem == 2)
            item.sprite = heartItem;
    }

    public void OnClick()
    {
        if (!giftCardManager.canSelect)
            return;

        isChosing = true;
        giftCardManager.canSelect = false;

        LeanTween.rotateAround(gameObject, Vector3.up, -180f, 0.6f);
        StartCoroutine(ChangeFaceUpSprite());
    }

    private IEnumerator ChangeFaceUpSprite()
    {
        yield return new WaitForSeconds(0.3f);
        image.sprite = faceUpSprite;
        transform.localScale = new Vector3(-1f, 1f);
        item.gameObject.SetActive(true);

        // complete flip
        yield return new WaitForSeconds(0.3f);
        if (idItem == 0)
            PlayerPrefs.SetInt("RemoveItem", PlayerPrefs.GetInt("RemoveItem") + 1);
        else if (idItem == 1)
            PlayerPrefs.SetInt("ReplaceItem", PlayerPrefs.GetInt("ReplaceItem") + 1);
        else if (idItem == 2)
            PlayerPrefs.SetInt("HeartItem", PlayerPrefs.GetInt("HeartItem") + 1);

        PlayerPrefs.SetInt("HaveGift", 0);
        GameManager.Instance.timeOnline = 0f;

        yield return new WaitForSeconds(0.5f);
        giftCardManager.hasSelected = true;
    }

    public void PostUp()
    {
        if (isChosing)
            return;

        LeanTween.rotateAround(GetComponent<RectTransform>(), Vector3.up, -180f, 0.4f);
        StartCoroutine(ChangePostUpSprite());
    }

    private IEnumerator ChangePostUpSprite()
    {
        yield return new WaitForSeconds(0.2f);
        image.sprite = postUpSprite;
        transform.localScale = new Vector3(-1f, 1f);
    }
}
