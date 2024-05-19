using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Jump variables
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float dropSpeed = 5f;

    public bool isJumping = false;
    public bool forceDescent = false;
    private Vector3 initialPosition;
    Animator anim;

    // Stamina
    [SerializeField] public float maxStamina = 50f;
    [SerializeField] private float staminaConsumptionRate = 10f;
    [SerializeField] private float staminaRecoveryRate = 15f;
    [SerializeField] public float currentStamina;
    [SerializeField] private float outOfStaminaLimit = 0f;
    [SerializeField] private PlayerStaminaBar staminaBar;


    private void Awake()
    {
        staminaBar = GetComponentInChildren<PlayerStaminaBar>();
    }
    void Start()
    {
        initialPosition = new Vector3(-5.89f, 0f);
        currentStamina = maxStamina;
        anim = GetComponent<Animator>();
        staminaBar.UpdateStaminaBar(currentStamina, maxStamina);
    }

    void Update()
    {
        anim.SetFloat("height", transform.position.y);
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
            anim.SetBool("isFall", false);
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
            anim.SetBool("isFall", false);
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
        staminaBar.UpdateStaminaBar(currentStamina, maxStamina);
    }

    void RecoverStamina(float amount)
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + amount);
        staminaBar.UpdateStaminaBar(currentStamina, maxStamina);
    }

    IEnumerator LowerPlayerCoroutine()
    {
        while (transform.position.y > initialPosition.y)
        {
            anim.SetBool("isFall", true);
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = initialPosition;
        isJumping = false;
        forceDescent = false;
    }
}
