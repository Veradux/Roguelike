using UnityEngine;

namespace Abilities {

    public class MultiplyHealthAbility : Ability {

        [SerializeField] private float multiplier = 0.5f; // percentage of health per stack to add to max health

        public override void RegisterEventHandlers() {
            Creature.MaxHealth.CalculateStatHandlers.Add(new PlayerCalculateStatHandler() {
                CalculationType = CalculationType.Multiplication,
                HandleStatCalculation = StatsOnOnPlayerCalculateHealth
            });
        }

        private float StatsOnOnPlayerCalculateHealth(float health) {
            return health + (health * Stacks * multiplier);
        }

        public override void OnAdded() {
            base.OnAdded();

            Creature.MaxHealth.SetOutdated();
        }

    }

}
