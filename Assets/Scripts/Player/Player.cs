using UnityEngine;
using Zenject;
using YG;

public class Player : MonoBehaviour 
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Texture _currentTexture;

    private Material _materialInstance;
    private Wallet _wallet;
    private string _playerID;

    public Wallet PlayerWallet => _wallet;
    public string PlayerID => _playerID;
    public int TableScore { get; private set; }

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

    public void SetTexture(Texture texture)
    {
        if (_materialInstance == null || texture == null)
            return;

        _currentTexture = texture;
        _materialInstance.SetTexture("_MainTex", _currentTexture);
    }
}
