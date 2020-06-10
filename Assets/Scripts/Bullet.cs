using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthBehaviour health = collision.gameObject.GetComponent<HealthBehaviour>();
          
        if (health != null)
        {
            health.TakeDamage(20);
        }
        Destroy(gameObject, 1f);
    }
}
