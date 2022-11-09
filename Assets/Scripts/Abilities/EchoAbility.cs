using System.Collections;
using UnityEngine;

namespace Abilities {

    [CreateAssetMenu(menuName = "ScriptableObjects/EchoAbility", order = 1)]
    public class EchoAbility : ActionAbility {

        public float delay = 0.5f;

        public override void RegisterEventHandlers() {
            foreach (var creatureAction in AffectedActions) {
                creatureAction.OnActionInvoked += OnUtilityActionHandler;
            }
        }

        private void OnUtilityActionHandler() {
            Creature.StartCoroutine(EchoCoroutine());
        }

        private IEnumerator EchoCoroutine() {
            for (int i = 0; i < Stacks; i++) {
                yield return new WaitForSeconds(delay);

                foreach (var creatureAction in AffectedActions) {
                    creatureAction.ForceInvoke();
                }
            }
        }

    }

}
