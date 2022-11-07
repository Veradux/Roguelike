using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetsManagement {

    public class AssetsFactory {

        public static void RegisterReferencesToAssets<T>(IAssetManager<T> assetManager, string directory)
            where T : UnityEngine.Object {
            assetManager.Assets.Clear();

            string[] files = Directory.GetFiles(directory, "*.asset", SearchOption.TopDirectoryOnly);

            foreach (var file in files) {
                var asset = AssetDatabase.LoadAssetAtPath<T>(file);
                assetManager.Assets.Add(asset);
            }
        }

        public static void CreateAssetsForAbstractClass(Type type, string directory) {
            Assembly.GetAssembly(typeof(AssetsFactory))
                    .GetTypes()
                    .Where(assemblyType => assemblyType.IsSubclassOf(type))
                    .ToList()
                    .ForEach(assemblyType => InstantiateScriptableObject(assemblyType, directory));
        }

        public static void CreateAndRegisterAssets<T>(IAssetManager<T> assetManager, string directory)
            where T : Object {
            CreateAssetsForAbstractClass(typeof(T), directory);
            RegisterReferencesToAssets(assetManager, directory);
        }

        private static void InstantiateScriptableObject(Type type, string directory) {
            var assetName = $"{type.Name}";

            // Check if asset already exists
            var assetPaths = AssetDatabase.FindAssets(assetName, new[] {directory});

            if (assetPaths is {Length: > 0}) {
                return;
            }

            var scriptableObject = ScriptableObject.CreateInstance(type);

            var path = $"{directory}/{assetName}.asset";
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = scriptableObject;
        }

    }

}
