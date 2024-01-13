using UnityEngine;

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    public class ExampleServiceFoo : ExampleServiceBase
    {
        public override void DoThing()
        {
            Debug.Log($"{GetType().Name}: Doing thing that was requested.");
        }
    }
}
