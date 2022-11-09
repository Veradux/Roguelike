using System;
using System.Collections;
using Abilities;
using UnityEngine;

namespace Actions {

    [Flags]
    public enum CreatureActionType {

        // Refer to this article to learn about enum flags
        // http://www.alanzucconi.com/2015/07/26/enum-flags-and-bitwise-operators/
        Primary = 1, // 000001
        Utility = 2 // 000010

    }

    public abstract class CreatureAction : ScriptableObject {

        #region Events
        public event Action OnActionInvoked;
        #endregion

        [SerializeField] private float cooldown = 1f;
        private bool isOnCooldown;

        public CreatureActionType type;
        protected Creature Creature;

        public void RegisterDependencies(Creature creature) {
            Creature = creature;
        }

        protected abstract void Act();

        public void Invoke() {
            if (isOnCooldown)
                return;

            OnActionInvoked?.Invoke();

            // Invoke all abilities event handlers EXCEPT for EchoAbility bcs it will just loop and it will be CRINGE
            var list = OnActionInvoked?.GetInvocationList() ?? Array.Empty<Delegate>();

            foreach (var @delegate in list) {
                var target = (Ability) @delegate.Target;

                if (target is not EchoAbility) {
                    (@delegate as EventHandler)?.Invoke(target, null);
                }
            }

            Creature.StartCoroutine(CooldownCoroutine());
            ForceInvoke();
        }

        public void ForceInvoke() {
            Debug.Log("XD");
            Act();
        }

        private IEnumerator CooldownCoroutine() {
            isOnCooldown = true;

            yield return new WaitForSeconds(cooldown);
            isOnCooldown = false;
        }

    }

}
