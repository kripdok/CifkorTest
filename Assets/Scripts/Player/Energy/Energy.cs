using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Energy
{
    public UnityAction<float> ChangeParameters;

    private float _maxValue;
    private float _concreteValue;
    private EnergyManager _manager;
    private Coroutines _coroutines;
    private Coroutine _coroutine;


    public UnityAction<float> CountChangedWallet;
    public UnityAction<bool> TransactionVerified;

    public Energy(float maxValue)
    {
        _maxValue = maxValue;
        _concreteValue = _maxValue;
        _manager = ServiceLocator.Instance.Get<EnergyManager>();
        _coroutines = ServiceLocator.Instance.Get<Coroutines>();
        _manager.CountTryChanged += TryToReduceTheMoney;
    }

    ~Energy()
    {
        _manager.CountTryChanged -= TryToReduceTheMoney;
    }

    public void TryToReduceTheMoney(float number)
    {
        bool isSuccessful = false;

        if (_concreteValue - number < 0)
            isSuccessful = false;
        else
        {
            _concreteValue -= number;
            CountChangedWallet?.Invoke(_concreteValue);
            isSuccessful = true;

            if(_coroutine != null)
                _coroutines.StopCoroutine(_coroutine);

            _coroutine = _coroutines.StartCoroutine(Replenish());
        }

        TransactionVerified?.Invoke(isSuccessful);
    }

    private IEnumerator Replenish()
    {
        while (_concreteValue != _maxValue)
        {
            yield return new WaitForSeconds(0.5f);
            _concreteValue += 1;
            CountChangedWallet?.Invoke(_concreteValue);
        }
    }
}
