using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Store/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private ItemView _prefabView;
    [SerializeField] private Item _item;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Texture _texture;

    public ItemView ItemView => _prefabView;
    public Item Item => _item;
    public Sprite Icon => _icon;
    public int Price => _price;
    public Texture Scin => _texture;
}
