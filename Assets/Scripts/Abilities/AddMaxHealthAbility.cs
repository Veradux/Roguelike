using UnityEngine;

namespace Abilities {

    public class AddMaxHealthAbility : Ability {

        [SerializeField] private float additionalMaxHealth = 10f;

        public override void OnAdded() {
            base.OnAdded();

            AddModifier<AdditionModifier>(Creature.MaxHealth, additionalMaxHealth);
        }

    }

}
