using System.Threading.Tasks;
using UnityEngine.Events;

public class Wallet
{
    private float _count;
    private WalletManager _manager;

    private TaskCompletionSource<bool> _task;
    public UnityAction<float> CountChangedWallet;
    public UnityAction<bool> TransactionVerified;

    public Wallet(float startCount)
    {
        _count = startCount;

        _manager = ServiceLocator.Instance.Get<WalletManager>();
        
        _manager.CountTryChanged += TryToReduceTheMoney;
        _manager.CountChanged += AddMoney;
    }

    ~Wallet()
    {
        _manager.CountTryChanged -= TryToReduceTheMoney;
        _manager.CountChanged -= AddMoney;
    }

    public void AddMoney(float number)
    {
        _count += number;
        CountChangedWallet?.Invoke(_count);
    }

    public void TryToReduceTheMoney(float number)
    {
        bool isSuccessful = false;

        if (_count - number < 0)
            isSuccessful = false;
        else
        {
            _count -= number;
            CountChangedWallet?.Invoke(_count);
            isSuccessful = true;
        }

        TransactionVerified?.Invoke(isSuccessful);
    }

}
