using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToDeath : MonoBehaviour
{
    [SerializeField]
    GameObject slimePuddle;
    [SerializeField]
    GameObject lifeDropPrefab;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("death");
        StartCoroutine(WaitForAnimationEnd());
        int random = Random.Range(0, 11);
        if (random == 9)
        {
            Instantiate(lifeDropPrefab,gameObject.transform.position,Quaternion.identity);
        }
    }

    IEnumerator WaitForAnimationEnd()
    {

        // Espere até que a animação termine
        while (true)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Death") && stateInfo.normalizedTime >= 1.0f)
            {
                Destroy(gameObject);
                Instantiate(slimePuddle,new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - 0.4f), Quaternion.identity);
                break; 
            }
            yield return null;
        }
    }
}
