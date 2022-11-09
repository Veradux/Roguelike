using System;
using UnityEngine;

namespace Abilities {

    public abstract class Ability : ScriptableObject {

        protected Creature Creature { get; private set; }
        [NonSerialized] public int Stacks = 0;

        public void RegisterDependencies(Creature creature) {
            Creature = creature;
        }

        public virtual void RegisterEventHandlers() { }

        public virtual void OnAdded() { }

        protected void AddModifier<TModifier>(Stat stat, float modifierValue) where TModifier : Modifier, new() {
            new TModifier().RegisterDependencies(stat)
                           .SetModifierValue(modifierValue)
                           .RegisterEventHandlers();

            stat.SetOutdated();
        }

    }

}
