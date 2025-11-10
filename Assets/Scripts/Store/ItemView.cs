using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour 
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _imageScin;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _boughtText;

    private void Awake()
    {
        _boughtText.gameObject.SetActive(false);
    }

    public void Initialize(ItemSO itemData)
    {
        if (itemData == null)
            return;

        _imageScin.sprite = itemData.Icon;
        _priceText.text = itemData.Price.ToString();
    }

    public void ChangeBackgroundColor(Color color)
    {
        _backgroundImage.color = color;
    }

    public void ActivateBoughtText()
    {
        _boughtText.gameObject.SetActive(true);
    }
}
