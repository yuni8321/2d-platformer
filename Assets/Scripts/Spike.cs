using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ���� �浹");
            Debug.Log(SceneManager.GetActiveScene().buildIndex);

            // ���ӿ��� �̺�Ʈ
            GameManager.Instance.GameOver();
        }
    }
}
