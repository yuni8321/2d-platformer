using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 열쇠 충돌");

            PlayerController3 player = collision.collider.gameObject.GetComponent<PlayerController3>();
            player.hasKey = true;

            GameObject.Destroy(gameObject);
        }
    }
}
