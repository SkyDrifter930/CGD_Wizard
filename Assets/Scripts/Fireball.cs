using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
   
    public float speed = 5f;

   
    void Update()
    {
        
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wizard"))
        {
            Destroy(gameObject);
        }
    }
}
