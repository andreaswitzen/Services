#nullable enable
using UnityEngine;
using UnityEngine.Assertions;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    public static class ServiceLocator
    {
        private static ServiceContainer Services => ServiceContainer.Instance!;
        private static ExampleServiceBase? _exampleService;

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
                Assert.IsNotNull(ServiceContainer.Instance);
                _exampleService = CreateService<ExampleServiceBase, ExampleServiceFoo>(Services.ExampleSystemPrefab);
                return _exampleService;
            }
        }


        private static T CreateService<T, TU>(GameObject? prefab = null) where T : Service where TU : T
        {
            // If a service prefab was provided, instantiate it.
            if (prefab != null)
            {
                var obj = Object.Instantiate(prefab);
                if (obj.TryGetComponent(out T instantiatedService))
                {
                    if (ServiceContainer.Instance!.LoggingEnabled)
                        Debug.Log($"ServiceLocator: Instantiated ExampleService of type: " +
                                  $"{instantiatedService!.GetType().Name}");
                    return instantiatedService;
                }
            }

            // Otherwise create a default service (one could also opt to create a dummy).
            var createdService = Service.Create<TU>();
            if (ServiceContainer.Instance!.LoggingEnabled)
                Debug.Log($"ServiceLocator: Creating default ExampleService of type: " +
                          $"{createdService.GetType().Name}.");

            return createdService;
        }
    }
}