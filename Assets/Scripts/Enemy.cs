using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    GameObject player;

    public float moveSpeed = 5f;
    public float x_direction;
    public float changeMindTime = 3f;
    public bool isGrounded;
    public float chaseRange = 3f;

    enum EnemyState
    {
        Patrol, // 순찰
        Chase   // 플레이어 추적
    }

    enum EnemyDirection
    {
        Left = -1,
        Idle = 0,
        Right = 1
    }

    [SerializeField] EnemyState state;
    [SerializeField] EnemyDirection direction;

    [SerializeField] float elapsedTime;  //마지막으로 방향을 바꾸고 경과한 시간
    [SerializeField] Vector3 edgeOffset;
    [SerializeField] Vector3 directionToPlayer;
    [SerializeField] float distanceToPlayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        state = EnemyState.Patrol;
        direction = EnemyDirection.Idle;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // 적 캐릭터의 앞이 절벽이 아닌가?
        isGrounded = Physics2D.CircleCast(transform.position + edgeOffset, 0.3f, Vector2.down, 1.1f, LayerMask.GetMask("Platforms"));
        if (!isGrounded)
        {
            direction = direction == EnemyDirection.Left ? EnemyDirection.Right : EnemyDirection.Left;
            x_direction = (float)direction;
            elapsedTime = 0;
        }

        // 플레이어로 향하는 방향벡터
        directionToPlayer = player.transform.position - transform.position;
        distanceToPlayer = directionToPlayer.magnitude; //벡터의 길이

        if (Mathf.Abs(x_direction) > 0)
        {
            if (x_direction < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

        if (state == EnemyState.Patrol)
        {
            if (elapsedTime >= changeMindTime)
            {
                direction = (EnemyDirection)Random.Range(-1, 2);  //(-1, 0 ,1)
                x_direction = (float)direction;

                elapsedTime = 0;
            }

            elapsedTime += Time.deltaTime;

            if (distanceToPlayer <= chaseRange)
            {
                state = EnemyState.Chase;
            }
        }
        else if (state == EnemyState.Chase)
        {
            direction = directionToPlayer.x < 0 ? EnemyDirection.Left : EnemyDirection.Right;
            x_direction = (float)direction;

            if (distanceToPlayer > chaseRange)
            {
                state = EnemyState.Patrol;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(moveSpeed * x_direction, rigidbody2d.velocity.y);
    }

    private void LateUpdate()
    {
        edgeOffset = new Vector3(transform.localScale.x * 0.5f, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + edgeOffset, transform.position + edgeOffset + Vector3.down * 1.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 적 충돌");

            collision.collider.SendMessage("Damage", 1);
        }
    }
}
