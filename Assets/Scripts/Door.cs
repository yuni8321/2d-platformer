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
            Debug.Log("�÷��̾� �� �浹");

            PlayerController3 player = collision.collider.GetComponent<PlayerController3>();
            if (player == null)
            {
                return;
            }

            if (player.hasKey)
            {
                // ���� ȸ��
                player.hasKey = false;

                //�� ���� ó��
                spriteRenderer.sprite = openedDoorSprite;
                boxCollider2D.enabled = false;
            }
        }
    }
}
