using UnityEngine;
using Zenject;
using YG;

public class Player : MonoBehaviour 
{
    private string _playerID;
    private Wallet _wallet;

    public string PlayerID => _playerID;
    public Wallet Wallet => _wallet;

    private void Awake()
    {
        if(YG2.player.auth)
            _playerID = YG2.player.id;
    }

    [Inject]
    public void Construct(Wallet walletl)
    {
        _wallet = walletl;
    }
}
