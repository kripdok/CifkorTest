using System.Threading.Tasks;
using UnityEngine.Events;

public class EnergyManager : IService
{
    private EnergyUI _ui;
    private Energy _energy;

    public TaskCompletionSource<bool> _taskCompletion { get; private set; }

    public UnityAction<float> CountChanged;
    public UnityAction<float> CountTryChanged;

    public EnergyManager(EnergyUI ui)
    {
        _ui = ui;
        var gameData = ServiceLocator.Instance.Get<SettingsData>();
        _energy = new Energy(this, gameData.MaxEnergyValue, gameData.RecoveryTimeOfOneUnitEnergy);
        _ui.Init(this, gameData.MaxEnergyValue);

        _energy.CheckCompleted += SetTransactionResult;
        _energy.ValuesHasChanged += ChangeUIElement;
    }

    public async Task<bool> TryChangeWallet(float count)
    {
        _taskCompletion = new TaskCompletionSource<bool>();
        CountTryChanged?.Invoke(count);
        return await _taskCompletion.Task;
    }

    private void SetTransactionResult(bool isResult)
    {
        _taskCompletion.TrySetResult(isResult);

    }

    private void ChangeUIElement(float number)
    {
        CountChanged?.Invoke(number);
    }
}
