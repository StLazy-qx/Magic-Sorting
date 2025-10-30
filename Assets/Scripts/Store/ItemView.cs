using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour 
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _imageScin;
    [SerializeField] private TMP_Text _priceText;

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

        Debug.Log("Вызвался метод изменения цвета фона у предмета [ItemView]");
    }
}
