using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalebCodeLibrary.ObjectPool.Example
{
    /// <summary>
    /// The object pooler script that will pool bullets to spawn.
    /// </summary>
    public class ObjectPool_ProjectilesExampleScript : CalebCodeLibrary.ObjectPool.ObjectPool
    {
        [Tooltip("Duration between shooting each projectile.")]
        [SerializeField]
        float firingCooldown = 1f;

        // Used as a timer
        float debounceTime = 0f;

        // To demonstrate preloading of objects.
        void Start()
        {
            PreloadObjectPoolObjects(); // ObjectPool's function to preload amount of selected gameobjects provided in the Inspector.
        }

        // We will use Update() to demonstrate how to use an ObjectPool inherited class.
        void Update()
        {
            debounceTime += Time.deltaTime;
            if (debounceTime > firingCooldown) // if timer has exceeded given cooldown time
            {
                // Reset the timer
                debounceTime = 0f;

                // Fire A Projectile
                {
                    GameObject projectile = GetAvailableObject();

                    // This is the example's logic of activating a projectile
                    {
                        projectile.transform.position = transform.position; // We reset the projectile's position to this object's position.
                        projectile.SetActive(true); // We activate the projectile
                    }
                }
                
            }
        }

        // For this example, we will check if that GameObject is inactive in hierarchy.
        public override GameObject GetAvailableObject()
        {
            foreach (Transform child in transform) // we are looping through all the children under this transform.
            {
                if (!child.gameObject.activeInHierarchy) // Checks if the rules that define whether that gameobject is "inactive"
                {
                    // we found a gameobject that is "inactive" and thus "available"
                    return child.gameObject;
                }
            }

            // If the code reaches here, it means there are currently no available GameObjects.
            // Therefore, we need to create a new one and return it.
            return CreateNewObject();
        }

        // We are overriding CreateNewObject() so that we can print some text to console!
        // Default CreateNewObject() automatically instantiates the GameObject and sets its parent to this object's transform.
        protected override GameObject CreateNewObject()
        {
            GameObject newObject = base.CreateNewObject(); // This is the default CreateNewObject() and we cache the returned value
            Debug.Log("A new object is created: " + newObject.name); // Print the new object's name
            return newObject;
        }

    }
}