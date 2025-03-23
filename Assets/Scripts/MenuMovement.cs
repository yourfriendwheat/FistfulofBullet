using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{

    public float speed = 2f; // Adjust this for your desired speed


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object to the left
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
