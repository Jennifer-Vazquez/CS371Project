using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ForceField : MonoBehaviour
{
    float startTime; 
    float elapsedTime; 
    float totalTime = 100f;
    float totalplayersTime; 
    public Slider forcefieldslider; 
    public GameObject forcefield;
    public int max_force_field_time = 100; 

    public void SetMaxForceFieldTime(float time)
    {
        forcefieldslider.maxValue = time;
        forcefieldslider.value = time;
    }

    public void SetForceFieldTime(float time)
    {
        forcefieldslider.value -= time ; 

    }
    void Start()
    {
       forcefield.SetActive(false); 
       SetMaxForceFieldTime(max_force_field_time); 
        
    }

    void Update()
    {

        if (totalplayersTime >= totalTime){
            forcefield.SetActive(false);
        }
        else{ 

        if (Input.GetKeyDown("f"))
        {
            startTime = Time.time;
            Debug.Log("F key down");
            
        }

        if (Input.GetKey("f") && (totalplayersTime < totalTime))
        { 
            Debug.Log("player's time so far " + totalplayersTime.ToString());
            Debug.Log("total time " + totalTime.ToString());
            Debug.Log("F get key");
            Debug.Log("Force field value" + forcefieldslider.value.ToString());
            forcefield.SetActive(true); 
            elapsedTime = Time.time - startTime; 
            SetForceFieldTime(elapsedTime); 
            totalplayersTime = totalplayersTime + elapsedTime; 

        }

        if (Input.GetKeyUp("f"))
        {
            Debug.Log("F  key up");
            forcefield.SetActive(false); 

        }
        }
    }
   
        
    void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "Bullet") && (forcefield.activeSelf) )
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "forcefieldinitiator") 
        {
            Destroy(other.gameObject);
            forcefieldslider.value += 20; 
        }
    
    }

    void OnCollisionEnter(Collision collision)
    {

        if ((collision.transform.tag == "Bullet") && (gameObject.tag == "Player") && (forcefield.activeSelf) )
        {
            Destroy(collision.gameObject);
        }
    }




 
}
