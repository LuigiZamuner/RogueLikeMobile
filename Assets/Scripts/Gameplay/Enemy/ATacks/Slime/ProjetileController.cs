using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjetileController : MonoBehaviour
{
    [SerializeField]
    GameObject slimePrefab;
    private float projectileSpeed = 3.5f;
    private float projectileduration = 2f;
    private int damage = 3;
    private int maxSpawns = 0;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        if (GameManager.instance.playerPos != null)
        {
            Vector2 direction = (GameManager.instance.playerPos.position - transform.position).normalized;
            rb2D.velocity = direction * projectileSpeed;
            StartCoroutine(DestruirAposSegundos(projectileduration));
        }
        else
        {
            Debug.Log("player nao existe");
        }
    }
    IEnumerator DestruirAposSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        int randomNumber = Random.Range(0, 26);
        if (randomNumber == 2 && maxSpawns <= 2)
        {
            Instantiate(slimePrefab, gameObject.transform.position, Quaternion.identity);
            maxSpawns++;
        }
        
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage, health.deathPrefab);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}





