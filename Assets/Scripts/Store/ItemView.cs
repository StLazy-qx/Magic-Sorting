using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour 
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _imageScin;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _priceText;

    public Image BackgroundImage => _backgroundImage;
    public Button Button => _button;

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
}
