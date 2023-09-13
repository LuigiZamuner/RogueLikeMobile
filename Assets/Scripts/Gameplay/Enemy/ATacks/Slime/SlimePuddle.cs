using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePuddle : MonoBehaviour
{
    private Timer timerToDestroy;
    private int timerDuration = 30;
    // Start is called before the first frame update
    void Start()
    {
        timerToDestroy = gameObject.AddComponent<Timer>();
        timerToDestroy.Duration = 15;
        timerToDestroy.AddTimerFinishedEventListener(FinishTimer);
        timerToDestroy.Run();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pM = other.gameObject.GetComponent<PlayerMovement>();
            SpriteRenderer sr= other.gameObject.GetComponent<SpriteRenderer>();
            pM.moveSpeed = 2;
            sr.color = Color.green;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pM = other.gameObject.GetComponent<PlayerMovement>();
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            pM.moveSpeed = 5;
            sr.color = Color.white;
        }
    }
    private void FinishTimer()
    {
        Destroy(gameObject);
    }
}
