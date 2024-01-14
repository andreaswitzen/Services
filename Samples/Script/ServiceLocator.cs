#nullable enable
using UnityEngine;
using UnityEngine.Assertions;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator? Instance { get; set; }

        // Services
        private static ExampleServiceBase? _exampleService;

        [field: SerializeField] private GameObject? ExampleServicePrefab { get; set; }

        [field: SerializeField] private bool LoggingEnabled { get; set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                if (Instance.LoggingEnabled)
                {
                    Debug.Log($"{GetType().Name}: Instance registered.");
                }
            }
            else if (!Instance.Equals(this))
            {
                if (Instance.LoggingEnabled)
                {
                    Debug.Log($"{GetType().Name}: More than one service locator instance. Destroying.");
                }

                Destroy(this);
            }

            Assert.IsNotNull(Instance);
        }

        public static ExampleServiceBase ExampleService
        {
            get
            {
                if (_exampleService != null)
                {
                    return _exampleService;
                }

                _exampleService = CreateService<ExampleServiceBase, ExampleServiceFoo>(Instance!.ExampleServicePrefab);
                return _exampleService;
            }
        }

        /*
         * T = Base class.
         * TU = Default implementation.
         */
        private static T CreateService<T, TU>(GameObject? prefab = null) where T : Service where TU : T
        {
            // If a service prefab was provided, instantiate it.
            if (prefab != null)
            {
                var obj = Object.Instantiate(prefab);
                if (obj.TryGetComponent(out T instantiatedService))
                {
                    if (Instance!.LoggingEnabled)
                        Debug.Log($"{Instance.GetType().Name}: Instantiated service of type: " +
                                  $"{instantiatedService!.GetType().Name}");
                    return instantiatedService;
                }
            }

            // Otherwise create default implementation (one could also opt to create a dummy).
            var createdService = Service.Create<TU>();
            if (Instance!.LoggingEnabled)
                Debug.Log($"{Instance.GetType().Name}: Creating default service of type: " +
                          $"{createdService.GetType().Name}.");

            return createdService;
        }
    }
}