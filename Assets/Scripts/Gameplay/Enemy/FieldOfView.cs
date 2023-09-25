using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float range;
    [SerializeField]
    private float fieldofviewrange;
    [SerializeField]
    private float colliderDistance;
    [SerializeField]
    private Collider2D boxCollider;
    [SerializeField]
    private LayerMask playerLayer;
    


    private SpriteRenderer sr;

    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame

    public bool PlayerContact()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y *range, boxCollider.bounds.size.z));
    }
    public bool FieldOfViewEnemy()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * fieldofviewrange, boxCollider.bounds.size.y * fieldofviewrange, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
        //new Vector3(boxCollider.bounds.size.x * fieldofviewrange, boxCollider.bounds.size.y * fieldofviewrange, boxCollider.bounds.size.z));
    //}

}
