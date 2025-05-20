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
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fireballObj = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);

            Vector3 fireballDirection = new Vector3(lastDirection, 0, 0);

            Fireball fireballScript = fireballObj.GetComponent<Fireball>();
            if (fireballScript != null)
            {
                fireballScript.SetDirection(fireballDirection);
            }
        }
        else
        {
            Debug.LogWarning("FireballPrefab oder FireballSpawnPoint ist nicht zugewiesen!");
        }
    }
}

