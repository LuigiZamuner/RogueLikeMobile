using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : MonoBehaviour
{
    private Follow follow;
    private int healPower = 3;
    // Start is called before the first frame update
    void Start()
    {
        follow = GetComponent<Follow>();
        follow.SetImpulseForce(2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health playerHealth= other.GetComponent<Health>();
            playerHealth.Heal(healPower);
            Destroy(gameObject);
        }
    }
}
