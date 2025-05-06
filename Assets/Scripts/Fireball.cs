using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballCooldown = 2f; // Standard-Cooldown in Sekunden

    // Geschwindigkeit des Feuerballs, anpassbar im Unity-Editor
    public float speed = 5f;

    // Flugzeit des Feuerballs, anpassbar im Unity-Editor
    public float flightDuration = 2f;

    // Richtung des Feuerballs, anpassbar im Unity-Editor
    public Vector3 direction = Vector3.left;

    private float flightTimeElapsed = 0f;

    void Update()
    {
        // Bewegt den Feuerball in die eingestellte Richtung mit der eingestellten Geschwindigkeit
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Aktualisiert die verstrichene Flugzeit
        flightTimeElapsed += Time.deltaTime;

        // Zerstört den Feuerball, wenn die Flugzeit abgelaufen ist
        if (flightTimeElapsed >= flightDuration)
        {
            Destroy(gameObject);
        }
    }
}
