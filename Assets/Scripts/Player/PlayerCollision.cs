using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.onPlay.AddListener(RestartPlayer);
    }

    private void RestartPlayer()
    {
        gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }
}
