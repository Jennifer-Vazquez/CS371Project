using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyHome : MonoBehaviour
{
    public Transform[] target; 
    public float speed;
    public GameObject gameoverPanel;

    
    private int current; 

    void Start()
    {
        gameoverPanel.SetActive(false); 
        
    }



    void Update()
    {
        if (current != 8)
        {
            Debug.Log("current" + current); 
            
            if (transform.position != target[current].position)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos); 

            }
            else 
            {
                current = (current + 1) % target.Length; 
            }
        }
        else
        {
            gameoverPanel.SetActive(true); 

        }
        
    }
}
