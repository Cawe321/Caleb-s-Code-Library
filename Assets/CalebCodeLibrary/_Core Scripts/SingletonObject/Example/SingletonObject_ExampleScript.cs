using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.SingletonObject.Example
{
    public class SingletonObject_ExampleScript : SingletonObject<SingletonObject_ExampleScript>
    {

        public override void Awake()
        {
            //base.Awake();  
            SingletonAwake(); // base.Awake() works as well since this class directly inherits from SingletonObject<T>.
        }

        /// <summary>
        /// A dummy function that will be called from <see cref="SingletonObject_ExampleCallerScript"/>. 
        /// </summary>
        /// <returns></returns>
        public int DummyFunctionThatReturns5()
        {
            return 5;
        }
    }
}
