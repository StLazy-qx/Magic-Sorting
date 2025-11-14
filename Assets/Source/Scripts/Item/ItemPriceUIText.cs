using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPriceUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Item _item;

    private void Start()
    {
        _text.text = _item.Price.ToString();
    }
}
