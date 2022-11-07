using System.Collections;
using UnityEngine;

namespace Actions {

    public class DashAction : CreatureAction {

        [SerializeField] private float dashDuration = .2f;
        [SerializeField] private float dashSpeedMultiplier = 1.5f;

        protected override void Act() {
            Creature.StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine() {
            var timer = 0f;
            var waitForFixedUpdate = new WaitForFixedUpdate();
            var direction = Creature.movementDirection;

            while (timer < dashDuration) {
                var dashMove = direction * (Time.fixedDeltaTime * Creature.movementSpeed * dashSpeedMultiplier);
                Creature.transform.position += (Vector3) dashMove;

                timer += Time.fixedDeltaTime;

                yield return waitForFixedUpdate;
            }
        }

    }

}
