using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHealthPoint;
    private float currentHealthPoint;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float moveSpeed;

    //运动方向
    private float inputX;
    //触地判定
    private bool isGround = true;
    [SerializeField]
    private float checkRadius = .2f;
    [SerializeField]
    private LayerMask layer;

    new private Rigidbody2D rigidbody2D;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, layer);

        Jump();
        Move();
        Flip();
    }

    private void Flip()
    {
        if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector2(inputX * moveSpeed, rigidbody2D.velocity.y);

        animator.SetFloat("isMoving", rigidbody2D.velocity.x);
        animator.SetBool("isGround", isGround);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rigidbody2D.velocity = new Vector2(0, jumpSpeed);

            animator.SetTrigger("jump");
        }
    }
}
