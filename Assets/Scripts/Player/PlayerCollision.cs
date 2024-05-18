using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        GameManager.instance.onPlay.AddListener(RestartPlayer);
        anim = GetComponent<Animator>();
    }

    private void RestartPlayer()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(-5.89f, 0f);
        gameObject.SetActive(true);
    }

    private void PlayerDeath()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("isHit", true);
            StartCoroutine(Delay(1f));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerDeath();
    }
}
