using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() != null)
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage, health.deathPrefab);
        }
    }
}
