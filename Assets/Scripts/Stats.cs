using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject {

    #region Events
    public event Action OnStatsChanged;
    #endregion

    public Stat maxHealth;
    public Stat damage;

}

[Serializable]
public class Stat {

    #region Events
    public event Action OnStatChanged;
    #endregion

    [SerializeField] private float baseValue = 10f;

    public List<PlayerCalculateStatHandler> CalculateStatHandlers = new();

    private bool outdated = true;

    private float value;

    public float Value {
        get {
            if (outdated) {
                value = CalculateStat();
                outdated = false;
            }

            return value;
        }
    }

    public void SetOutdated() => outdated = true;

    private float CalculateStat() {
        var calculatedHealth = baseValue;

        foreach (var handler in CalculateStatHandlers.OrderBy(handler => handler.Order)) {
            // Apply modification
            calculatedHealth = handler.HandleStatCalculation(calculatedHealth);
        }

        return calculatedHealth;
    }

}


public class PlayerCalculateStatHandler {

    public int Order { get; set; }
    public Func<float, float> HandleStatCalculation { get; set; }

}
