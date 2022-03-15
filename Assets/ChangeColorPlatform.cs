using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class ChangeColorPlatform : MonoBehaviour
{
    public Material clear;
    public Material fire; 
    public float colourChangeDelay = 4f;
    float currentDelay = 0f;
    bool colourChangeCollision = false;


    void Start()
    {
        GetComponent<MeshRenderer>().material = clear;

    }

    void Update()
    {
        checkColourChange();
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Collision: " + other.tag); 

        // GetComponent<MeshRenderer>().material.color = Color.black; 
        
        if (other.tag == "deadlysun")
        {
            Debug.Log("Collision: deadly sun");
            colourChangeCollision = true;
            currentDelay = Time.time + colourChangeDelay;

        }
        

    }

    void checkColourChange()
    {        
        if(colourChangeCollision)
        {
            GetComponent<MeshRenderer>().material = fire;
            if(Time.time > currentDelay)
            {
                GetComponent<MeshRenderer>().material = clear;
                colourChangeCollision = false;
            }
        }
    }
}
