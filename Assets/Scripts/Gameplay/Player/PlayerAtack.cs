using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    public GameObject atackArea = default;
    public GameObject swordPrefab = default;
    private CustomInput atackInput;
    private PlayerMovement pM;

    private bool isAtacking = false;

    private float attackDuration = 0.5f;
    private float attackCountdown = 0.5f;
    private float timer = 0f;
    private Transform areaPosition;
    private Animator anim;


    private void Awake()
    {
        atackInput = new CustomInput();
        atackInput.Player.Atack.performed += ctx => Atack();
    }
    private void OnEnable()
    {
        atackInput.Enable();
    }
    private void OnDisable()
    {
        atackInput.Disable();
    }
    void Start()
    {
        atackArea = transform.GetChild(0).gameObject;
        swordPrefab = transform.GetChild(1).gameObject;
        areaPosition = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {


        if(isAtacking)
        {
            timer += Time.deltaTime;
            if(timer >= attackDuration)
            {
                timer = 0f;
                isAtacking= false;
                atackArea.SetActive(isAtacking);
                swordPrefab.SetActive(isAtacking);
            }

        }
        else
        {
            if (timer < attackCountdown)
            {
                timer += Time.deltaTime;
            }
        }

    }
    private void Atack()
    {
        if (!isAtacking && timer >= attackCountdown)
        {
            isAtacking = true;
            atackArea.SetActive(isAtacking);
            swordPrefab.SetActive(isAtacking);

            Vector2 horizontalInput = atackInput.Player.Movement.ReadValue<Vector2>();
            if (horizontalInput == new Vector2(0, 1) || pM.lastMoveDirection == Vector2.up) //cima
            {
                anim.SetTrigger("upAttack");
                atackArea.transform.rotation = Quaternion.Euler(0, 0, 90);
                atackArea.transform.position = new Vector2(areaPosition.position.x - 0.25f, areaPosition.position.y - 0.3f);
            }
            else if (horizontalInput == new Vector2(0, -1) || pM.lastMoveDirection == Vector2.down) // baixo
            {
                anim.SetTrigger("downAttack");
                atackArea.transform.rotation = Quaternion.Euler(0, 0, -90);
                atackArea.transform.position = new Vector2(areaPosition.position.x + 0.28f, areaPosition.position.y - 0.26f);
            }
            else if (horizontalInput == new Vector2(1, 0) || pM.lastMoveDirection == Vector2.right) // direita
            {
                anim.SetTrigger("rightAttack");
                atackArea.transform.rotation = Quaternion.Euler(0, 0, 0);
                atackArea.transform.position = new Vector2(areaPosition.position.x, areaPosition.position.y);
            }
            else if (horizontalInput == new Vector2(-1, 0) || pM.lastMoveDirection == Vector2.left)  //esquerda
            {
                anim.SetTrigger("leftAttack");
                atackArea.transform.rotation = Quaternion.Euler(0, 0, -180);
                atackArea.transform.position = new Vector2(areaPosition.position.x + 0.05f, areaPosition.position.y - 0.53f);
            }

            timer = 0f;
        }


    }
}
