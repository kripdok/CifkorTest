using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    private float _maxValue;
    private float _concreteValue;
    private EnergyManager _manager;

    public void Init(EnergyManager manager, float maxValue)
    {
        _maxValue = maxValue;
        ChangeValue(maxValue);
        _manager = manager;
        _manager.CountChanged += ChangeValue;
    }

    private void OnDestroy()
    {
        _manager.CountChanged -= ChangeValue;
    }

    private void ChangeValue(float concreteValue)
    {
        _text.text = concreteValue.ToString() + "/" + _maxValue.ToString();
        _slider.value = concreteValue / _maxValue;
    }
}
