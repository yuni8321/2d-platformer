using UnityEngine;
using UnityEngine.UI;

public class HUDProjectile : MonoBehaviour
{
    PlayerController3 player;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 컴포넌트 미리 저장(캐싱; caching)
        player = FindObjectOfType<PlayerController3>();

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (player.hasProjectile)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
