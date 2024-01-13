#nullable enable
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    [CreateAssetMenu(menuName = "ServiceLocator (Example)")]
    public class ServiceLocator : ScriptableObject
    {
        private static ServiceLocator? Instance { get; set; }
        private static ExampleServiceBase? _exampleService;

        // Set in editor.
        [field: SerializeField] private bool LoggingEnabled { get; set; }

        // Service prefabs. Assigned in editor.
        [field: SerializeField] private GameObject? ExampleSystemPrefab { get; set; }

        public void OnEnable()
        {
            TryRegister();
        }

        public void Awake()
        {
            TryRegister();
        }

        private void TryRegister()
        {
            if (Instance == null)
            {
                Instance = this;
                if (Instance.LoggingEnabled)
                {
                    Debug.Log("ServiceLocator: Instance registered.");
                }
            }
            else if (!Instance.Equals(this))
            {
                Debug.LogError($"More than one service locator asset exists. " +
                               $"The application might not behave predictably.\n" +
                               $"Active instance path: {AssetDatabase.GetAssetPath(Instance.GetInstanceID())}\n" +
                               $"Duplicate instance path: {AssetDatabase.GetAssetPath(GetInstanceID())}");
            }
        }

        public static ExampleServiceBase ExampleService
        {
            get
            {
                if (_exampleService != null)
                {
                    return _exampleService;
                }

                // A ServiceLocator asset must exist. One can be created with the asset context menu
                // (by right-clicking assets and selecting Create -> ServiceLocator).
                Assert.IsNotNull(Instance);
                _exampleService = CreateService<ExampleServiceBase, ExampleServiceFoo>(Instance!.ExampleSystemPrefab);
                return _exampleService;
            }
        }


        private static T CreateService<T, TU>(GameObject? prefab = null) where T : Service where TU : T
        {
            // If a service prefab was provided, instantiate it.
            if (prefab != null)
            {
                var obj = Instantiate(prefab);
                if (obj.TryGetComponent(out T instantiatedService))
                {
                    if (Instance!.LoggingEnabled)
                        Debug.Log($"ServiceLocator: Instantiated ExampleService of type: " +
                                  $"{instantiatedService!.GetType().Name}");
                    return instantiatedService;
                }
            }

            // Otherwise create a default service (one could also opt to create a dummy).
            var createdService = Service.Create<TU>();
            if (Instance!.LoggingEnabled)
                Debug.Log($"ServiceLocator: Creating default ExampleService of type: " +
                          $"{createdService.GetType().Name}.");

            return createdService;
        }
    }
}