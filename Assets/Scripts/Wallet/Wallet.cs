using System.Threading.Tasks;
using UnityEngine.Events;

public class Wallet
{
    private float _count;
    private WalletManager _manager;
    private TaskCompletionSource<bool> _task;

    public UnityAction<float> ValuesHasChanged;
    public UnityAction<bool> CheckCompleted;

    public Wallet(float startCount)
    {
        _manager = ServiceLocator.Instance.Get<WalletManager>();
        _count = startCount;

        _manager.CountTryChanged += TryToReduceValue;
        _manager.CountChanged += AddAddValue;
    }

    ~Wallet()
    {
        _manager.CountTryChanged -= TryToReduceValue;
        _manager.CountChanged -= AddAddValue;
    }

    public void AddAddValue(float amount)
    {
        _count += amount;
        ValuesHasChanged?.Invoke(_count);
    }

    public void TryToReduceValue(float amount)
    {
        bool isSuccessful = false;

        if (_count - amount < 0)
            isSuccessful = false;
        else
        {
            _count -= amount;
            ValuesHasChanged?.Invoke(_count);
            isSuccessful = true;
        }

        CheckCompleted?.Invoke(isSuccessful);
    }

}
