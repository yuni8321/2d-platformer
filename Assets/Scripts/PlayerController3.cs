using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController3 : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;

    public Projectile projectilePrefab;

    public bool hasKey;
    public bool hasProjectile;

    public int health = 5;
    public float jumpForce = 15f;
    public float moveSpeed = 5f;
    public float x_direction;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start 호출");

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.CircleCast(transform.position, 0.3f, Vector2.down, 1.1f, LayerMask.GetMask("Platforms"));
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

        if (GameManager.Instance.State == GameManager.GameState.Running 
            && Input.GetKeyDown(KeyCode.Space) 
            && isGrounded) 
            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        x_direction = Input.GetAxis("Horizontal");
        if (Mathf.Abs(x_direction) > 0)
        {
            if (x_direction < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }

        if (hasProjectile && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("발사");

            Vector3 playerDirection = new Vector3(transform.localScale.x, 0, 0);

            Projectile projectile = GameObject.Instantiate<Projectile>(projectilePrefab, 
                transform.position + playerDirection, Quaternion.identity);

            projectile.Fire(playerDirection);

            hasProjectile = false;
        }
    }
    public void Damage(int damage)
    {
        //Debug.Log(damage + "를 받았다");
        Debug.Log($"{damage}를 받았다");

        //health = health - damage;
        health -= damage;

        if (health <= 0)
        {
            // 게임오버 이벤트
            GameManager.Instance.GameOver();
        }

        animator.SetTrigger("Hurt");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.1f);
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(x_direction * moveSpeed, rigidbody2d.velocity.y);
    }
}
