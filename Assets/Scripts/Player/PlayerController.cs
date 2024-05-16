using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Jump variables
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float dropSpeed = 5f;

    private bool isJumping = false;
    private bool forceDescent = false;
    private Vector3 initialPosition;

    // Stamina
    [SerializeField] private float maxStamina = 30f;
    [SerializeField] private float staminaConsumptionRate = 5f;
    [SerializeField] private float staminaRecoveryRate = 5f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float outOfStaminaLimit = 3f;

    void Start()
    {
        initialPosition = transform.position;
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (!isJumping
            && (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            && currentStamina >= outOfStaminaLimit)
        {
            isJumping = true;
            Jump();
        }

        if (isJumping)
        {
            if (currentStamina <= outOfStaminaLimit)
            {
                if (!forceDescent)
                {
                    forceDescent = true;
                    StartCoroutine(LowerPlayerCoroutine());
                }
            }
            else
            {
                ConsumeStamina(staminaConsumptionRate * Time.deltaTime);
            }
        }

        if (!isJumping && currentStamina < maxStamina)
        {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }
    }

    void Jump()
    {
        float targetY = initialPosition.y + jumpHeight;
        StartCoroutine(JumpCoroutine(targetY));
    }

    IEnumerator JumpCoroutine(float targetY)
    {
        while (transform.position.y < targetY && !forceDescent)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
            yield return null;

            // Check stamina continuously while jumping
            if (currentStamina <= outOfStaminaLimit)
            {
                forceDescent = true;
                StartCoroutine(LowerPlayerCoroutine());
                yield break; // Exit the jump coroutine
            }
        }

        if (!forceDescent)
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);

            // Keep the player above the original position until Space/W/UpArrow key is released
            while (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                yield return null;
            }

            // Lower the player back to the original position
            StartCoroutine(LowerPlayerCoroutine());
        }
    }

    void ConsumeStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
    }

    void RecoverStamina(float amount)
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + amount);
    }

    IEnumerator LowerPlayerCoroutine()
    {
        while (transform.position.y > initialPosition.y)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = initialPosition;
        isJumping = false;
        forceDescent = false;
    }
}
