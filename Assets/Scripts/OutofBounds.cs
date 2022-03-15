using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutofBounds : MonoBehaviour
{
    private float timeToAppear = 1f;
    private float timeWhenDisappear;
    public GameObject gameoverPanel;

    void Start()
    {
        gameoverPanel.SetActive(false); 
        
    }

    void Update()
    {
        //Debug.Log("update"); 
        if ( gameoverPanel.activeSelf && (Time.time >= timeWhenDisappear))
        {
            gameoverPanel.SetActive(false); 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision"); 
        //Debug.Log("COLLISION: " + collision.transform.tag + gameObject.tag); 
        
        if ((collision.transform.tag == "barrier") && (gameObject.tag == "Player"))
        {
            EnableText(); 

        }
    }

    public void EnableText()
    {
        gameoverPanel.SetActive(true); 
        timeWhenDisappear = Time.time + timeToAppear;
    }

}


