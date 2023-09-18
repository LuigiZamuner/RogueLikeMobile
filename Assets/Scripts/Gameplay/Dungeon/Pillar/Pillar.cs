using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;
    private SpriteRenderer sr ;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
     sr= GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tatu"))
        {
            count++;
            switch (count)
            {
                case 1:
                    sr.sprite = sprite1;
                    break;
                case 2:
                    sr.sprite = sprite2;
                    break;
                case 3:
                    sr.sprite = sprite3;
                    break;
                case 4:
                    Destroy(gameObject);
                    break;
            }
        }

    }
}
