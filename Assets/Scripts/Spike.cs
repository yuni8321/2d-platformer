using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 가시 충돌");
            Debug.Log(SceneManager.GetActiveScene().buildIndex);

            // 게임오버 이벤트
            GameManager.Instance.GameOver();
        }
    }
}
