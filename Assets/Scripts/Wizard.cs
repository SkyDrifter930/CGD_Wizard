using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {             // Move the object to the right
            transform.position = transform.position + new Vector3(0, 1, 0) * Time.deltaTime;
            

        }

        if (Input.GetKey(KeyCode.S))
        {             // Move the object to the right
            transform.position = transform.position + new Vector3(0, -1, 0) * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.A))
        {             // Move the object to the right
            transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.D))
        {             // Move the object to the right
            transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime;
        }
    }
}
