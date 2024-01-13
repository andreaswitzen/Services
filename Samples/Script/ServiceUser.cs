using UnityEngine;

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    public class ServiceUser : MonoBehaviour
    {
        public void Start()
        {
            ServiceLocator.ExampleService.DoThing();
        }
    }
}
