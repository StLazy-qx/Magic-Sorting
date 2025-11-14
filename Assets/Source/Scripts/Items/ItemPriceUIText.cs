using UnityEngine;
using TMPro;

namespace Items
{
    public class ItemPriceUIText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Item _item;

        private void Start()
        {
            _text.text = _item.Price.ToString();
        }
    }
}