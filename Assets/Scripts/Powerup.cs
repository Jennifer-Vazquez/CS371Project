using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float spinSpeed = 1f;

    Vector3 _yRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        _yRotation = Vector3.up * (spinSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_yRotation);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
