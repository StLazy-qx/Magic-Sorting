using UnityEngine;
using TMPro;

public class RoundCounterUI : MonoBehaviour
{
    [SerializeField] private FinalGameSession _finalGameSession;
    [SerializeField] private TMP_Text _textRoundScore;

    private void OnEnable()
    {
        _finalGameSession.RoundChanged += UpdateRoundUI;
        UpdateRoundUI(_finalGameSession.CurrentRound);
    }

    private void OnDisable()
    {
        _finalGameSession.RoundChanged -= UpdateRoundUI;
    }

    private void UpdateRoundUI(int newRound)
    {
        _textRoundScore.text = newRound.ToString();
    }
}
