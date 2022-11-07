using Abilities;
using Actions;
using UnityEditor;

namespace AssetsManagement {

    /// <summary>
    /// Class containing all the cute buttons for automated asset management
    /// </summary>
    public static class AssetsFactoryMenuItems {

        #region Abilities
        private const string AbilityAssetDirectory = "Assets/Abilities";

        [MenuItem("Automation/Abilities/Create missing Ability Scriptable Objects")]
        public static void CreateAbilityScriptableObjects() {
            AssetsFactory.CreateAssetsForAbstractClass(typeof(Ability), AbilityAssetDirectory);
        }

        [MenuItem("Automation/Abilities/Add missing Ability assets to AssetManager")]
        public static void RegisterAbilityAssets() {
            var assetManager = UnityEngine.Object.FindObjectOfType<AssetManager>();
            AssetsFactory.RegisterReferencesToAssets<Ability>(assetManager, AbilityAssetDirectory);
        }
        #endregion

        #region Actions
        private const string PlayerActionsAssetDirectory = "Assets/PlayerActions";

        [MenuItem("Automation/Actions/Create missing Player Actions Scriptable Objects")]
        public static void CreateActionsScriptableObjects() {
            AssetsFactory.CreateAssetsForAbstractClass(typeof(CreatureAction), PlayerActionsAssetDirectory);
        }

        [MenuItem("Automation/Actions/Add missing Player Actions assets to AssetManager")]
        public static void RegisterActionsAssets() {
            var assetManager = UnityEngine.Object.FindObjectOfType<AssetManager>();
            AssetsFactory.RegisterReferencesToAssets<CreatureAction>(assetManager, PlayerActionsAssetDirectory);
        }
        #endregion

    }

}
