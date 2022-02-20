using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CalebCodeLibrary.ObjectPool.Example
{
    /// <summary>
    /// A simple demo script to simulate an object moving forward
    /// </summary>
    public class ObjectPool_ProjectilesExampleProjectileScript : MonoBehaviour
    {
        [Tooltip("How fast the projectile will move forward")]
        [SerializeField]
        float speed;

        [Tooltip("Amount of time before projectile Despawns")]
        [SerializeField]
        float activeDuration = 1f;

        float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > activeDuration)
            {
                Debug.Log("Timer" + activeDuration);
                Debug.Log("Despawning Projectile");
                gameObject.SetActive(false);
                return;
            }
            transform.position += transform.forward * (speed * Time.deltaTime); // moving forward(transform.forward is direction vector) by speed. We multiply by Time.deltaTime so that its time dependant instead of being framerate dependant.
        }

        private void OnEnable()
        {
            timer = 0f; // Important as whenever the object is reused and set to active, timer needs to reset as well!
        }
    }
}