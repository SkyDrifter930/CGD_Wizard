using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public GameObject targetPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            
            Destroy (gameObject);
            Destroy(collision.gameObject);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
