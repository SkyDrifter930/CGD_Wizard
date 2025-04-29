using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float movementSpeed = 5f;

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

        transform.position += movement * movementSpeed * Time.deltaTime;
    }
    
}
