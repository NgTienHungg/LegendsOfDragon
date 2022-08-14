using UnityEngine;

public class GiftCardManager : MonoBehaviour
{
    [SerializeField] private GiftCard[] cards;
    [HideInInspector] public bool canSelect = true;
    [HideInInspector] public bool hasSelected;

    private void Update()
    {
        if (hasSelected)
        {
            //Debug.Break();
            hasSelected = false;
            foreach (var card in cards)
                card.PostUp();
        }
    }

    public void Renew()
    {
        canSelect = true;
        foreach (var card in cards)
            card.Renew();
    }
}
