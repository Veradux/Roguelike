using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;

namespace Abilities {

    /**
     * ActionAbility is an effect triggered by a creature's actions.
     */
    public abstract class ActionAbility : Ability {

        [SerializeField] private CreatureActionType actionType;

        protected IEnumerable<CreatureAction> AffectedActions {
            get { return Creature.actions.Where(action => (action.type & actionType) == action.type); }
        }

    }

}
