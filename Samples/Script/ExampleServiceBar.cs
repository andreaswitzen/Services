using UnityEngine;

namespace Packages.com.andreaswitzen.services.Samples.Script
{
    public class ExampleServiceBar : ExampleServiceBase
    {
        public override void DoThing()
        {
            Debug.Log($"{GetType().Name}: Doing other variant of thing that was requested.");
        }
    }
}
