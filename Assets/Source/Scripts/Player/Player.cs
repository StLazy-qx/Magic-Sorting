using UnityEngine;
using Zenject;
using YG;

public class Player : MonoBehaviour, IObjectInitilizable
{
    private string _playerID;
    private Wallet _wallet;

    public string PlayerID => _playerID;
    public Wallet Wallet => _wallet;

    public bool IsInitialized { get; private set; }

    [Inject]
    public void Construct(Wallet walletl)
    {
        _wallet = walletl;
    }

    //добавить авторизацию игрока
    public void Initilize()
    {
        if (_wallet == null)
            return;

        if (YG2.player.auth)
            _playerID = YG2.player.id;

        IsInitialized = true;
    }
}
