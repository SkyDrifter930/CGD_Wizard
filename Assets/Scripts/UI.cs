using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wizard = GameObject.FindGameObjectsWithTag("Player");
        Wizard = GameObject.Find("Wizard");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
