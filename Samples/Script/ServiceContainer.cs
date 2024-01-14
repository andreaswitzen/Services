#nullable enable
using UnityEditor;
using UnityEngine;

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    [CreateAssetMenu(menuName = "ServiceContainer (Example)")]
    public class ServiceContainer : ScriptableObject
    {
        public static ServiceContainer? Instance { get; set; }

        public bool LoggingEnabled { get; set; }
        public GameObject? ExampleSystemPrefab;

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
                               $"The application might not behave predictably.");
            }
        }
    }
}