using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Timer timerToDestroy;
    private Rigidbody2D rb;
    private Vector2 cimaDireita = new Vector2(1.0f, 1.0f);
    private Vector2 cimaEsquerda = new Vector2(-1.0f, 1.0f);
    private Vector2 baixoDireita = new Vector2(1.0f, -1.0f);
    private Vector2 baixoEsquerda = new Vector2(-1.0f, -1.0f);
    private List<Vector2> diagonais = new List<Vector2>();

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        timerToDestroy = gameObject.AddComponent<Timer>();
        diagonais.Add(cimaDireita);
        diagonais.Add(cimaEsquerda);
        diagonais.Add(baixoDireita);
        diagonais.Add(baixoEsquerda);

        rb = GetComponent<Rigidbody2D>();
        index = UnityEngine.Random.Range(0, 3);
        rb.AddForce(diagonais[index]* 1000, ForceMode2D.Impulse);
        timerToDestroy.Duration = 5;
        timerToDestroy.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerToDestroy.Finished)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(3);
        }
    }
}
