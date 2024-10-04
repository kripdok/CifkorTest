using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObject/SettingsData", order = 1)]
public class GameData : ScriptableObject, IService
{
    [field: SerializeField] public float AmountOfMoneyPerTap { get; private set; }

    [field: SerializeField] public float RequiredEnergyPerPress { get; private set; }

    [SerializeField] private List<AutoclickerInfo> _autoclickers;

    public IReadOnlyList<AutoclickerInfo> Autoclickers => _autoclickers;

}

[Serializable]
public struct AutoclickerInfo
{
    public string Name;
    public float AmountOfMoney;
    public float RollbackTimeInSeconds;
    public float CostPerUnit;
    [Range(1,10)] public float CostMultiplier;
}
