﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Actions;
using AssetsManagement;
using UnityEngine;

// TODO: FOR SOME REASON ECHOS IS FASTER THAN FIRST DASH, I GUESS MAYBE ECHO CALLS IT MULTIPLE TIMES OR SOMETHING XD
public class Creature : MonoBehaviour {

    #region Events
    // TODO: make this fucking event work!
    public event Action OnCalculatedStatsChanged;
    public event Action OnLiquidStatsChanged;

    public event Action OnCreatureActionInvoked;
    #endregion

    public List<Ability> abilities = new();
    public List<CreatureAction> creatureActions = new();

    [SerializeField] private AssetManager assetManager;

    [Header("Parameters")] public Vector2 movementDirection = Vector2.zero;


    [SerializeField] public float movementSpeed = 4f;
    [SerializeField] private float health;
    [SerializeField] private Stat maxHealth;

    public Stat MaxHealth => maxHealth;

    public float Health {
        get => health;
        set {
            health = value;
            OnLiquidStatsChanged?.Invoke();
        }
    }

    private void Start() {
        // We copy actions so that each run has a brand new fresh copy.
        // Using the original will create problems with referencing destroyed objects
        // by restarting the game in the editor
        creatureActions.Add(assetManager.playerActionsAssets[0]);
        creatureActions.Add(assetManager.playerActionsAssets[0]);

        creatureActions.ForEach(x => x.RegisterDependencies(this));

        AddAbility<EchoAbility>();
        AddAbility<EchoAbility>();
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

        creatureActions[actionIndex]
            ?.Invoke();
    }

}
