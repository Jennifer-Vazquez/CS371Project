using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovements : MonoBehaviour
{

    // NOT GOOD WHEN THERE IS MULTIPLE SUNS THEY CLUMP TOGETHER 
    public float timer; 
    public float timerSpeed; 
    public float timeToMove; 
    public float speed = 2.0f;
    public float xPos;
    public float zPos;
    public float range_lower;
    public float range_upper;
    public Vector3 desiredPos;

    public float movement_speed  = 2.5f; 
    [SerializeField] private Vector3  _rotation; 
    [SerializeField] private float  rotation_speed; 
    void Start()
    {
        xPos = Random.Range(range_lower,range_upper);
        desiredPos = new Vector3(xPos, transform.position.y, transform.position.z);

        zPos = Random.Range(range_lower,range_upper);
        desiredPos = new Vector3( transform.position.x,  transform.position.y, zPos);
    }
 
    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        if (timer >= timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                // changes the x position 
                xPos = Random.Range(range_lower,range_upper);

                // changes the y position 
                zPos = Random.Range(range_lower,range_upper);

                desiredPos = new Vector3(xPos,  transform.position.y, zPos);

                timer = 0.0f;
            }
        }
        else
        {


        transform.position = new Vector3(Mathf.PingPong(Time.time * movement_speed, 5), transform .position .y, transform .position .z);
        transform.position = new Vector3(transform .position .x,  transform .position .y,  Mathf.PingPong(Time.time * movement_speed, 3)); 
        }

        transform.Rotate(_rotation *  rotation_speed * Time.deltaTime); 

       
    }




}
