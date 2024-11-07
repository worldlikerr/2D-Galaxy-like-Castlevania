using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties

    [SerializeField] private float maxHealthPoint;
    private float currentHealthPoint;
    [SerializeField] private float maxEnergyValue;
    [SerializeField]private float currentEnergyValue;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    //[SerializeField] private float dashCD;
    //private float dashCDCount;
    //运动方向
    private float inputX;
    //触地判定
    private bool isGround = true;
    [SerializeField] private float checkRadius = .2f;
    [SerializeField] private LayerMask layer;

    new private Rigidbody2D rigidbody2D;
    private Animator animator;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //运动方向
        inputX = Input.GetAxisRaw("Horizontal");
        //触地判定
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, layer);

        //冲刺
        dashTime -= Time.deltaTime;
        //dashCDCount -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashDuration;
            //dashCDCount = dashCD;
        }

        Jump();
        Move();
        Flip();
    }

    /// <summary>
    /// 反转方向
    /// </summary>
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

    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        if (dashTime > 0 && isGround)
        {
            rigidbody2D.velocity = new Vector2(inputX * dashSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(inputX * moveSpeed, rigidbody2D.velocity.y);
        }

        animator.SetFloat("isDash", dashTime);
        animator.SetFloat("isMoving", rigidbody2D.velocity.x);
        animator.SetBool("isGround", isGround);
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rigidbody2D.velocity = new Vector2(0, jumpSpeed);

            animator.SetTrigger("jump");
        }
    }


}
