using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Actions;
using AssetsManagement;
using UnityEngine;

public class Creature : MonoBehaviour {

    #region Events
    // TODO: make this fucking event work!
    public event Action OnCalculatedStatsChanged;
    public event Action OnHealthChanged;
    public event Action OnCreatureActionInvoked;
    #endregion

    [SerializeField] private AssetManager assetManager;

    public List<Ability> abilities = new();
    public List<CreatureAction> actions = new();

    [Header("Stats")] public Vector2 movementDirection = Vector2.zero;
    [SerializeField] public float movementSpeed = 4f;
    [SerializeField] private float health;
    [SerializeField] private Stat maxHealth;

    public Stat MaxHealth => maxHealth;

    public float Health {
        get => health;
        set {
            health = value;
            OnHealthChanged?.Invoke();
        }
    }

    private void Awake() {
        // We copy actions so that each run has a brand new fresh copy.
        // Using the original will create problems with referencing destroyed objects
        // by restarting the game in the editor
        actions.Add(assetManager.playerActionsAssets[0]);

        actions.ForEach(x => x.RegisterDependencies(this));

        AddAbility<EchoAbility>();
        AddAbility<EchoAbility>();
        AddAbility<EchoAbility>();

        maxHealth.OnStatChanged += () => OnCalculatedStatsChanged?.Invoke();
    }

    private void AddAbility<T>() where T : Ability {
        var newAbility = abilities.FirstOrDefault(ability => ability is T);

        if (newAbility is null) {
            newAbility = assetManager.abilityAssets.First(ability => ability is T);
            newAbility.Stacks = 0;
            newAbility.RegisterDependencies(this);
            newAbility.RegisterEventHandlers();
            abilities.Add(newAbility);
        }

        newAbility.Stacks++;

        newAbility.OnAdded();
    }

    public void UseCreatureAction(int actionIndex) {
        OnCreatureActionInvoked?.Invoke();

        actions[actionIndex]
            ?.Invoke();
    }

}
