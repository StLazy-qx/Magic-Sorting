using UnityEngine;
using TMPro;

public class VesselFillLevelView : MonoBehaviour
{
    [SerializeField] private VolumeAggregator _vessel;
    [SerializeField] private TMP_Text _count;

    //добавить эффект при заполнении
    private void OnEnable()
    {
        _vessel.SizeChanged += OnFillView;
    }

    private void OnDisable()
    {
        _vessel.SizeChanged -= OnFillView;
    }

    private void OnFillView(int value)
    {
        _count.text = value.ToString();
    }
}
