using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class WalletManager : MonoBehaviour, IService
{
    [SerializeField] private WalletUI _ui;

    private Wallet _wallet;
    public TaskCompletionSource<bool> PurchaseTaskCompletion { get; private set; }

    public UnityAction<float> CountChanged;
    public UnityAction<float> CountTryChanged;
    public UnityAction<float> Test;

    public void Init()
    {
        _wallet = new Wallet(0);
        _ui.Init(0);
        _wallet.CountChangedWallet += InvokeCountChanged;
        _wallet.TransactionVerified += SetTransactionResult;
    }

    private void OnDestroy()
    {
        _wallet.CountChangedWallet -= InvokeCountChanged;
        _wallet.TransactionVerified -= SetTransactionResult;
    }

    public async Task<bool> TryChangeWallet(float count)
    {
        PurchaseTaskCompletion = new TaskCompletionSource<bool>();
        CountTryChanged?.Invoke(count);
        return await PurchaseTaskCompletion.Task;
    }

    public void AddMoneyBecomClickOrAutoclicker(float count) // переименовать
    {
        CountChanged?.Invoke(count);
    }

    private void InvokeCountChanged(float count)
    {
        Test?.Invoke(count); //
    }

    private void SetTransactionResult(bool isResult)
    {
        PurchaseTaskCompletion.TrySetResult(isResult);
    }
}
