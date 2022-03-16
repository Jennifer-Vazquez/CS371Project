using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{


    public float speed = 1.0f;

    public Transform target;
    public Transform moveBack;

    Vector3 targetP;
    Vector3 moveBackP;
    private bool moveToTarget = true;


    void Start(){
        targetP = target.position;
        moveBackP = moveBack.position;
    }
    void Update()
    {

        float step =  speed * Time.deltaTime;
        if(moveToTarget){
        transform.position = Vector3.MoveTowards(transform.position, targetP, step);
        }
        else{
           transform.position = Vector3.MoveTowards(transform.position, moveBackP, step);

        }

        if (Vector3.Distance(transform.position, targetP) < 0.001f) // if its practically at the target position then move back
        {
            moveToTarget = false;
        }
         if (Vector3.Distance(transform.position, moveBackP) < 0.001f)
        {
            moveToTarget = true;
        }


    }
} 