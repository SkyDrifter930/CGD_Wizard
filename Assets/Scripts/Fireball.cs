using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public float flightDuration = 2f;

    private Vector3 direction = Vector3.right; 
    private float flightTimeElapsed = 0f;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction.x != 0 ? direction.x : 1);
        transform.localScale = scale;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        flightTimeElapsed += Time.deltaTime;
        if (flightTimeElapsed >= flightDuration)
        {
            Destroy(gameObject);
        }
    }
}
