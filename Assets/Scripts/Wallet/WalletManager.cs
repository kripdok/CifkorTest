using System.Threading.Tasks;
using UnityEngine.Events;

public class WalletManager : IService
{
    private WalletUI _ui;
    private Wallet _wallet;
    private GameManager _playerManager;

    public TaskCompletionSource<bool> PurchaseTaskCompletion { get; private set; }

    public UnityAction<float> CountChanged;
    public UnityAction<float> CountTryChanged;
    public UnityAction<float> NumberChangesConfirmed;

    public WalletManager(WalletUI ui)
    {
        ServiceLocator.Instance.Register<WalletManager>(this);
        _playerManager = ServiceLocator.Instance.Get<GameManager>();
        _wallet = new Wallet(0);
        _ui = ui;
        _ui.Init(0);
        _playerManager.Action += ChangeValues;
        _wallet.ValuesHasChanged += InvokeCountChanged;
        _wallet.CheckCompleted += SetTransactionResult;
    }

    private void OnDestroy()
    {
        _wallet.ValuesHasChanged -= InvokeCountChanged;
        _wallet.CheckCompleted -= SetTransactionResult;
        _playerManager.Action -= ChangeValues;
    }

    public async Task<bool> TryChangeWallet(float amount)
    {
        PurchaseTaskCompletion = new TaskCompletionSource<bool>();
        CountTryChanged?.Invoke(amount);
        return await PurchaseTaskCompletion.Task;
    }

    private void ChangeValues(float amount)
    {
        CountChanged?.Invoke(amount);
    }

    private void InvokeCountChanged(float count)
    {
        NumberChangesConfirmed?.Invoke(count);
    }

    private void SetTransactionResult(bool isResult)
    {
        PurchaseTaskCompletion.TrySetResult(isResult);
    }
}
