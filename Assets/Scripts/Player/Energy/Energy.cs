using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Energy
{
    private EnergyManager _manager;
    private Coroutines _coroutines;
    private Coroutine _coroutine;
    private float _maxValue;
    private float _concreteValue;
    private float _durationRecovery;

    public UnityAction<float> ChangeParameters;
    public UnityAction<float> ValuesHasChanged;
    public UnityAction<bool> CheckCompleted;

    public Energy(EnergyManager manager,float maxValue, float durationRecovery)
    {
        _maxValue = maxValue;
        _durationRecovery = durationRecovery;
        _concreteValue = _maxValue;
        _manager = manager;
        _coroutines = ServiceLocator.Instance.Get<Coroutines>();
        _manager.CountTryChanged += TryToReduceValue;
    }

    ~Energy()
    {
        _manager.CountTryChanged -= TryToReduceValue;

        if (_coroutine != null)
            _coroutines.StopCoroutine(_coroutine);
    }

    public void TryToReduceValue(float amount)
    {
        bool isSuccessful = false;

        if (_concreteValue - amount < 0)
            isSuccessful = false;
        else
        {
            _concreteValue -= amount;
            ValuesHasChanged?.Invoke(_concreteValue);
            isSuccessful = true;

            if(_coroutine != null)
                _coroutines.StopCoroutine(_coroutine);

            _coroutine = _coroutines.StartCoroutine(Replenish());
        }

        CheckCompleted?.Invoke(isSuccessful);
    }

    private IEnumerator Replenish()
    {
        var waitSecond = new WaitForSeconds(_durationRecovery);
        while (_concreteValue != _maxValue)
        {
            yield return waitSecond;
            _concreteValue += 1;
            ValuesHasChanged?.Invoke(_concreteValue);
        }
    }
}
