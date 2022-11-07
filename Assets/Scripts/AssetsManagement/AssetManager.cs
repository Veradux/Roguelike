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
            abilityAssets = abilityAssets.Select(x => Instantiate(x))
                                         .ToList();

            playerActionsAssets = playerActionsAssets.Select(x => Instantiate(x))
                                                     .ToList();
        }

    }

}
