using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;

namespace Abilities {

    public abstract class ActionAbility : Ability {

        [SerializeField] private CreatureActionType actionType;

        protected IEnumerable<CreatureAction> AffectedActions {
            get {
                return Creature.creatureActions.Where(creatureAction =>
                    (creatureAction.ActionType & actionType) == creatureAction.ActionType);
            }
        }

    }

}
