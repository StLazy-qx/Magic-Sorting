using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class PlayerInfoView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    private string _playerID;

    private void Start()
    {
        //if (YG2.player.auth)
        //    _playerID = YG2.player.id;

        //_name.text = YG2.player.name;
        //_score.text = "100";
    }
}
