using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EnergyManager : MonoBehaviour, IService
{
    [SerializeField] private EnergyUI _ui;
    private Energy _energy;

    public TaskCompletionSource<bool> PurchaseTaskCompletion { get; private set; }

    public UnityAction<float> CountChanged;
    public UnityAction<float> CountTryChanged;

    public void Init(float maxValue)
    {
        _energy = new Energy(maxValue);
        _ui.Init(maxValue);

        _energy.TransactionVerified += SetTransactionResult;
        _energy.CountChangedWallet += ChangeUIElement;
    }

    public async Task<bool> TryChangeWallet(float count)
    {
        PurchaseTaskCompletion = new TaskCompletionSource<bool>();
        CountTryChanged?.Invoke(count);
        return await PurchaseTaskCompletion.Task;
    }

    private void SetTransactionResult(bool isResult)
    {
        PurchaseTaskCompletion.TrySetResult(isResult);
        
    }

    private void ChangeUIElement(float number)
    {
        CountChanged?.Invoke(number);
    }
}
