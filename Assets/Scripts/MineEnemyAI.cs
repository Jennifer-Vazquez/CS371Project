using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MineEnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public NavMeshAgent agent;
    private bool seen = false;
    public float followDistance = 3.0f;
    public float stopFollowDistance = 10.0f;
    public int curHealth = 100;
    private bool isColliding = false;
    public ParticleSystem _onHitEffect;

    Vector3 direction;
    Vector3 bulletdirection;
    Queue<AIBullet> _pool = new Queue<AIBullet>();
    Vector3 offset = new Vector3(0, 0, 3);

    //Assumes:  1. GameObject tied to this script has a NavMeshAgent Component tied to agent
    //          2. The player's transform is tied to playerTransform
    //              - Player gameobject must be in a layer labeled "Player"
    //          3. A NavMesh has already been baked
    void Update()
    {
        if (!seen) {
           // Debug.Log("Seen");
            if (playerInView()) {
                seen = true;
               // Debug.Log("Seen = true");
            }
        }

        else {
            float distance = distanceToPlayer();
            if (!playerInView()) {
                //Debug.Log("Player in view");
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
            }
            
            else if (distance < stopFollowDistance && distance >= followDistance) {
               // Debug.Log("Enemy in distance range");
                if (agent.isStopped == true)
                {
                   // Debug.Log("Agent is no longer stopped");
                    agent.isStopped = false;
                }
                agent.SetDestination(playerTransform.position);

            bulletdirection = (playerTransform.position - gameObject.transform.position).normalized;
            bulletdirection = new Vector3(bulletdirection.x, 0, bulletdirection.z);
            transform.forward = bulletdirection;

    
            }
            
            else {
                agent.isStopped = true;
               // Debug.Log("Stopped");
                //Debug.Log(seen);
                //Debug.Log(playerInView());
                //Debug.Log(distance);
                //Shoot here
            }
        }
    }
    
    bool playerInView()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Vector3 origin = transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 30) && 
            hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")) {
                return true;
        }
        return false;
    }

    float distanceToPlayer()
    {
        return Vector3.Distance(playerTransform.position, transform.position);
    }



void takeDamage(int damage){
    curHealth -= damage; 
}


//ALL ENEMY AI MUST HAVE KINEMATIC CHECKED 
void OnTriggerEnter (Collider other)
 {
     if(isColliding) return;
     isColliding = true;
     // Rest of the code
     if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other);
            takeDamage(20);
            _onHitEffect.Play();
            StartCoroutine("waitForExplosion");
            if (curHealth < 0){
                Destroy(gameObject);
            }
            

        }

        
    if (other.CompareTag("PlayerMine"))
        {
            Debug.Log("MINE HIT, hopefully gonna die");
            Destroy(other.gameObject);
            takeDamage(50);
            if(curHealth < 0){
                Destroy(gameObject);
            }
        
        }
     StartCoroutine(Reset());
 }


    private IEnumerator waitForExplosion()
    {
        yield return new WaitForSeconds(.25f);
        _onHitEffect.Stop();
    }

    IEnumerator Reset()
 {
      yield return new WaitForEndOfFrame();
      isColliding = false;
 }


}
