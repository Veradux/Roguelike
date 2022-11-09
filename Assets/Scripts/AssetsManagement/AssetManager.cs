using System.Collections.Generic;
using System.Linq;
using Abilities;
using Actions;
using UnityEngine;

namespace AssetsManagement {

    public interface IAssetManager<T> {

        public IList<T> Assets { get; }

    }

    public class AssetManager : MonoBehaviour, IAssetManager<Ability>, IAssetManager<CreatureAction> {

        public List<Ability> abilityAssets;
        public List<CreatureAction> playerActionsAssets;

        IList<Ability> IAssetManager<Ability>.Assets => abilityAssets;
        IList<CreatureAction> IAssetManager<CreatureAction>.Assets => playerActionsAssets;

        private void Awake() {
            // We copy assets so that each run has a brand new fresh copy.
            // Using the original will create problems with referencing destroyed objects,
            // by restarting the game in the editor.
            abilityAssets = abilityAssets.Select(Instantiate)
                                         .ToList();
            playerActionsAssets = playerActionsAssets.Select(Instantiate)
                                                     .ToList();
        }

    }

}
