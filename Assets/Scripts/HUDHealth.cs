using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealth : MonoBehaviour
{
    PlayerController3 player;

    public Sprite hasHealth;
    public Sprite hasNoHealth;

    public List<Image> hearts = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < player.health)
            {
                hearts[i].sprite = hasHealth;
            }
            else
            {
                hearts[i].sprite = hasNoHealth;
            }
        }
    }
}
