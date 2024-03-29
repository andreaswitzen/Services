using UnityEngine;

namespace Packages.com.andreaswitzen.services
{
    public abstract class Service : MonoBehaviour
    {
        public virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            gameObject.name = GetType().Name;
        }

        public static T Create<T>() where T : Service
        {
            var gameObject = new GameObject();
            var service = gameObject.AddComponent<T>();
            service.enabled = true;
            return service;
        }
    }
}