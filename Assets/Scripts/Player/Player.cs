using UnityEngine;
using Zenject;
using YG;

public class Player : MonoBehaviour 
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    //[SerializeField] private Texture _currentTexture;
    [SerializeField] private Inventory _inventory;

    private Material _materialInstance;
    private Wallet _wallet;
    private string _playerID;
    private Item _currentItem;

    public string PlayerID => _playerID;
    public Wallet Wallet => _wallet;
    public Inventory Inventory { get; private set; }
    public Item CurrentItem => _currentItem;

    private void Awake()
    {
        if(YG2.player.auth)
            _playerID = YG2.player.id;

        _materialInstance = _meshRenderer.material;
        Inventory = GetComponent<Inventory>();
    }

    [Inject]
    public void Construct(Wallet walletl)
    {
        _wallet = walletl;
    }

    public void EquipItem(Item item)
    {
        //if (_materialInstance == null || item.Texture == null)
        //    return;

        //Texture texture = item.Texture;
        //_currentTexture = texture;

        //_materialInstance.SetTexture("_MainTex", _currentTexture);
        if (item == null)
        {
            Debug.LogWarning("Нельзя надеть пустой предмет!");
            return;
        }

        if (_inventory == null)
        {
            Debug.LogError("Инвентарь не присвоен игроку!");
            return;
        }

        if (!_inventory.HasItem(item))
        {
            Debug.Log($"Игрок не имеет предмет {item.name} в инвентаре.");
            return;
        }

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
