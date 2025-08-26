using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float sprintMultiplier = 2f;

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballCooldown = 2f;

    // Mana & Health für UI
    public float maxMana = 100f;
    public float mana = 100f;
    public float manaRegenRate = 10f; // Mana pro Sekunde
    public float fireballManaCost = 20f;

    public float maxHealth = 100f;
    public float health = 100f;

    // Dash-Parameter
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float doubleTapThreshold = 0.3f; // Zeitfenster für Doppeltipp

    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private float dashTimeElapsed = 0f;
    private Vector3 dashDirection;

    private float lastLeftTapTime = -1f;
    private float lastRightTapTime = -1f;

    private float cooldownTimer = 0f;
    private bool canShoot = true;

    private int lastDirection = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Mana regenerieren
        if (mana < maxMana)
        {
            mana += manaRegenRate * Time.deltaTime;
            if (mana > maxMana)
                mana = maxMana;
        }

        // Dash Cooldown aktualisieren
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        // Dash-Logik
        if (isDashing)
        {
            float dashSpeed = dashDistance / dashDuration;
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            dashTimeElapsed += Time.deltaTime;
            if (dashTimeElapsed >= dashDuration)
            {
                isDashing = false;
            }
            return; // Während des Dashs keine normale Bewegung
        }

        // Double-Tap Dash nach links
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastLeftTapTime < doubleTapThreshold && dashCooldownTimer <= 0f)
            {
                isDashing = true;
                dashTimeElapsed = 0f;
                dashCooldownTimer = dashCooldown;
                dashDirection = Vector3.left;
                lastDirection = -1;
                return;
            }
            lastLeftTapTime = Time.time;
        }

        // Double-Tap Dash nach rechts
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastRightTapTime < doubleTapThreshold && dashCooldownTimer <= 0f)
            {
                isDashing = true;
                dashTimeElapsed = 0f;
                dashCooldownTimer = dashCooldown;
                dashDirection = Vector3.right;
                lastDirection = 1;
                return;
            }
            lastRightTapTime = Time.time;
        }

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1, 0, 0);
            lastDirection = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
            lastDirection = 1;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * lastDirection;
        transform.localScale = scale;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed * sprintMultiplier : movementSpeed;
        transform.position += movement * currentSpeed * Time.deltaTime;

        // Animation nur abspielen, wenn Bewegung vorhanden ist
        if (animator != null)
        {
            bool WizardWalking = movement.x != 0 || movement.y != 0;
            animator.SetBool("WizardWalking", WizardWalking);
        }

        if (!canShoot)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= fireballCooldown)
            {
                canShoot = true;
                cooldownTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            ShootFireball();
            canShoot = false;
        }

        if (!canShoot)
        {
            Debug.Log($"Cooldown aktiv: {fireballCooldown - cooldownTimer:F1} Sekunden verbleibend");
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null && mana >= fireballManaCost)
        {
            GameObject fireballObj = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);

            Vector3 fireballDirection = new Vector3(lastDirection, 0, 0);

            Fireball fireballScript = fireballObj.GetComponent<Fireball>();
            if (fireballScript != null)
            {
                fireballScript.SetDirection(fireballDirection);
            }

            mana -= fireballManaCost;
            if (mana < 0) mana = 0;
        }
        else if (mana < fireballManaCost)
        {
            Debug.Log("Nicht genug Mana!");
        }
        else
        {
            Debug.LogWarning("FireballPrefab oder FireballSpawnPoint ist nicht zugewiesen!");
        }
    }
}
