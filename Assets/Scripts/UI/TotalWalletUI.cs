using UnityEngine;
using TMPro;
using Zenject;

public class TotalWalletUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private Wallet _wallet;

    public int Score => _wallet.TotalScore;

    private void OnEnable()
    {
        _wallet.TotalScoreChanged += OnCoinView;

        OnCoinView(_wallet.TotalScore );
    }

    private void OnDisable()
    {
        _wallet.TotalScoreChanged -= OnCoinView;
    }

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnCoinView(int value)
    {
        //проверка

        _scoreText.text = value.ToString();
    }
}
