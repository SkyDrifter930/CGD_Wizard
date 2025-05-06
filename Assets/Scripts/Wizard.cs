using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wizard : MonoBehaviour
{

    public float movementSpeed = 5f; // Normal movement speed
    public float sprintMultiplier = 2f; // Multiplier for sprinting speed

    // Prefab des Feuerballs, im Unity-Editor zuzuweisen
    public GameObject fireballPrefab;

    // Position, von der der Feuerball abgeschossen wird
    public Transform fireballSpawnPoint;

    // Cooldown-Dauer für das Schießen eines Feuerballs
    public float fireballCooldown = 2f;

    private float cooldownTimer = 0f; // Timer für den Cooldown
    private bool canShoot = true; // Gibt an, ob der Wizard schießen kann

    void Start()
    {
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
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1, 0, 0);
        }

        // Check if the sprint key (LeftShift) is held down
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed * sprintMultiplier : movementSpeed;

        // Apply movement with the current speed
        transform.position += movement * currentSpeed * Time.deltaTime;

        // Cooldown-Logik
        if (!canShoot)
        {
            
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= fireballCooldown)
            {
                canShoot = true;
                cooldownTimer = 0f;
            }
        }

        // Schießt einen Feuerball, wenn "E" gedrückt wird und der Cooldown abgelaufen ist
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            ShootFireball();
            canShoot = false; // Setzt den Cooldown
        }

        // Sichtbare Anzeige des Cooldowns (Debug-Log)
        if (!canShoot)
        {
            Debug.Log($"Cooldown aktiv: {fireballCooldown - cooldownTimer:F1} Sekunden verbleibend");
        }

    }

    // Methode zum Schießen eines Feuerballs
    void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("FireballPrefab oder FireballSpawnPoint ist nicht zugewiesen!");
        }
    }
}
