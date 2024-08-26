using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 800f;
    public float timeToBeDestroy = 5f;

    Vector3 direction;

    Rigidbody2D rigidbody2D;

    public void Fire(Vector3 direction)
    {
        this.direction = direction;

        GameObject.Destroy(gameObject, timeToBeDestroy);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("발사체 적 충돌");

            GameObject.Destroy(collision.collider.gameObject);
            GameObject.Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Platform"))
        {
            Debug.Log("발사체 지형 충돌");
            GameObject.Destroy(gameObject);
        }
        else if (collision.collider.gameObject.name == "Objective")
        {
            Debug.Log("발사체 목표 충돌");
            GameObject.Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = direction * moveSpeed;
    }

    private void Update()
    {
        if(GameManager.Instance.State == GameManager.GameState.Paused)
        {
            return;
        }

        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }
}
