using UnityEngine;
using Zenject;
using YG;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Texture _currentTexture;

    private Material _materialInstance;
    private Wallet _wallet;
    private string _playerID;
    private List<Item> _items;

    public Wallet PlayerWallet => _wallet;
    public string PlayerID => _playerID;

    private void Awake()
    {
        if(YG2.player.auth)
            _playerID = YG2.player.id;

        _materialInstance = _meshRenderer.material;
    }

    [Inject]
    public void Construct(Wallet walletl)
    {
        _wallet = walletl;
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        //if (_items.Contains(item))
        //    return;

        _items.Add(item);

        if (IsCloth(item))
            SetTexture(item);
    }

    private bool IsCloth(Item item)
    {
        return item is Cloth;
    }

    private void SetTexture(Item item)
    {
        if (_materialInstance == null || item.Texture == null)
            return;

        Texture texture = item.Texture;
        _currentTexture = texture;
        _materialInstance.SetTexture("_MainTex", _currentTexture);
    }
}
