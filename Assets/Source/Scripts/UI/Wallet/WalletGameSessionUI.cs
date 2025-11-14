using UnityEngine;
using TMPro;
using Zenject;

public class WalletGameSessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.CurrentScoreChanged += OnCoinView;

        OnCoinView(_wallet.TotalScore);
    }

    private void OnDisable()
    {
        _wallet.CurrentScoreChanged -= OnCoinView;
    }

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnCoinView(int value)
    {
        //проверка

        _moneyText.text = value.ToString();
    }
}
