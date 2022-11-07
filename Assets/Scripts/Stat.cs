using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat {

    #region Events
    public event Action OnStatChanged;
    #endregion

    public List<PlayerCalculateStatHandler> CalculateStatHandlers = new();

    [SerializeField] private float baseValue = 10f;
    private bool isOutdated = true;
    private float value;

    public float Value {
        get {
            if (isOutdated) {
                value = CalculateStat();
                isOutdated = false;
            }

            return value;
        }
    }

    public void SetOutdated() => isOutdated = true;

    private float CalculateStat() {
        var calculatedHealth = baseValue;

        foreach (var handler in CalculateStatHandlers.OrderBy(handler => handler.CalculationType)) {
            // Apply modification
            calculatedHealth = handler.HandleStatCalculation(calculatedHealth);
        }

        return calculatedHealth;
    }

}

public class PlayerCalculateStatHandler {

    public CalculationType CalculationType { get; set; }
    public Func<float, float> HandleStatCalculation { get; set; }

}

/**
 * CalculationType is used to change the order of calculations when doing the math for stats.
 * For example, all additions and subtractions to the stat should be done first, then all multiplications and divisions. 
 */
public enum CalculationType {

    Addition = 0,
    Multiplication = 1

}
