using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectilePickup : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ����üȹ�� �浹");

            PlayerController3 player = collision.collider.gameObject.GetComponent<PlayerController3>();
            player.hasProjectile = true;

            GameObject.Destroy(gameObject);
        }
    }
}
