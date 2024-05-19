using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Animator anim;
    [SerializeField] private PlayerStaminaBar staminaBar;
    [SerializeField] private PlayerController playerController;
    public bool playerWasHit = false;

    private void Start()
    {
        GameManager.instance.onPlay.AddListener(RestartPlayer);
        anim = GetComponent<Animator>();
    }

    private void RestartPlayer()
    {
        //Set player position to default position
        gameObject.GetComponent<Transform>().position = new Vector3(-5.89f, 0f);

        //Set player stamina to max value
        playerController.currentStamina = 50f;

        staminaBar.UpdateStaminaBar(
            playerController.currentStamina, playerController.maxStamina);

        playerController.isJumping = false;
        playerController.forceDescent = false;

        playerWasHit = false;

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
            playerWasHit = true;
            StartCoroutine(Delay(1f));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerDeath();
    }
}
