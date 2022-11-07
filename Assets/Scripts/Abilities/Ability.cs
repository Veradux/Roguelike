using System;
using UnityEngine;

namespace Abilities {

    [CreateAssetMenu(fileName = "SomeAbility", menuName = "ScriptableObjects/Ability", order = 1)]
    public abstract class Ability : ScriptableObject {

        protected Creature Creature { get; private set; }

        [NonSerialized] public int Stacks = 0;

        public void RegisterDependencies(Creature creature) {
            Creature = creature;
        }

        public abstract void RegisterEventHandlers();

        public virtual void OnAdded() { }

    }

}
