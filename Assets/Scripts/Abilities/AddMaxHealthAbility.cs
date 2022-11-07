using UnityEngine;

namespace Abilities {

    public class AddMaxHealthAbility : Ability {

        [SerializeField] private float additionalMaxHealth = 10f;

        public override void RegisterEventHandlers() {
            Creature.MaxHealth.CalculateStatHandlers.Add(new PlayerCalculateStatHandler() {
                Order = 1,
                HandleStatCalculation = StatsOnOnPlayerCalculateHealth
            });
        }

        private float StatsOnOnPlayerCalculateHealth(float health) {
            return health + additionalMaxHealth * Stacks;
        }

        public override void OnAdded() {
            base.OnAdded();

            Creature.MaxHealth.SetOutdated();
        }

    }

}
