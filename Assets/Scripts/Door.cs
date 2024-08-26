using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    public Sprite openedDoorSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 문 충돌");

            PlayerController3 player = collision.collider.GetComponent<PlayerController3>();
            if (player == null)
            {
                return;
            }

            if (player.hasKey)
            {
                // 열쇠 회수
                player.hasKey = false;

                //문 열기 처리
                spriteRenderer.sprite = openedDoorSprite;
                boxCollider2D.enabled = false;
            }
        }
    }
}
