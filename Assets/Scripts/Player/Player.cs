using UnityEngine;
using Zenject;
using YG;

public class Player : MonoBehaviour 
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Inventory _inventory;

    private Material _materialInstance;
    private Wallet _wallet;
    private string _playerID;
    private Item _currentItem;

    public string PlayerID => _playerID;
    public Wallet Wallet => _wallet;
    public Inventory Inventory => _inventory;
    public Item CurrentItem => _currentItem;

    private void Awake()
    {
        if(YG2.player.auth)
            _playerID = YG2.player.id;

        _materialInstance = _meshRenderer.material;
        _inventory.ItemEquipped += OnEquipItem;
    }

    private void Start()
    {
        if (_inventory.EquippedItem != null)
            OnEquipItem(_inventory.EquippedItem);
    }

    private void OnDestroy()
    {
        if (_inventory != null)
            _inventory.ItemEquipped -= OnEquipItem;
    }

    [Inject]
    public void Construct(Wallet walletl)
    {
        _wallet = walletl;
    }

    public void OnEquipItem(Item item)
    {
        //if (_materialInstance == null || item.Texture == null)
        //    return;

        //Texture texture = item.Texture;
        //_currentTexture = texture;

        //_materialInstance.SetTexture("_MainTex", _currentTexture);
        if (item == null)
            return;

        if (_inventory.HasItem(item) == false)
            return;

        _currentItem = item;

        ApplyItemTexture(item);
    }

    private void ApplyItemTexture(Item item)
    {
        if (_materialInstance == null || item.Texture == null)
            return;

        _materialInstance.SetTexture("_MainTex", item.Texture);
    }
}
