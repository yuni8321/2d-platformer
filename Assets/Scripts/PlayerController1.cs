using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;

    public float jumpForce = 15f;
    public float moveSpeed = 5f;
    public float x_direction;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start »Æ¿Œ");

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 1.1f, LayerMask.GetMask("Platforms"));
        animator.SetBool("Grounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        x_direction = Input.GetAxis("Horizontal");
        if (Mathf.Abs(x_direction) > 0)
        {
            if (x_direction < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(x_direction * moveSpeed, rigidbody2d.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.1f);
    }
}
