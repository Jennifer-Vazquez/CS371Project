using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] int health;

    NavMeshAgent _navMeshAgent;
    public bool charging = false;
    public bool firing = false;
    public float startCharge;
    public float startLaser;
    public LineRenderer laser;
    public AudioSource laserAudio;
    public Transform shootPoint;
    public ParticleSystem _onHitEffect;
    Transform laserTarget;
    float startTime;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (charging == false)
        {
            if (playerInView() && firing == false && ((Time.time - startTime) > 1.0f))
            {
                charging = true;
                laserTarget = target.transform;
                startCharge = Time.time;
                _navMeshAgent.isStopped = true;
                transform.forward = (target.transform.position - transform.position);
                //Debug.Log("Charging");
            }

            else if(firing == false) 
            {
                _navMeshAgent.SetDestination(target.transform.position);
                //Debug.Log("Finding Player");
            }
            
            if(firing == true)
            {
                //Debug.Log("Firing at player");
                laser.gameObject.SetActive(true);
                Vector3 direction = laserTarget.position - shootPoint.position;
                Physics.Raycast(transform.position, /*direction*/transform.forward, out var raycastHit, Mathf.Infinity);
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
                if(raycastHit.collider.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("Hit player");
                    
                    PlayerControllerCC player = raycastHit.collider.gameObject.GetComponent<PlayerControllerCC>();
                    if(player.huangsMode == false){
                    player.HandleDestructiveCollision(1);
                    }
                }
                //Debug.Log(raycastHit.collider.gameObject.tag);
                //Debug.Log(Time.time - startLaser);
                if ((Time.time - startLaser) > 0.5f)
                {
                    laser.gameObject.SetActive(false);
                    _navMeshAgent.isStopped = false;
                    firing = false;
                    //Debug.Log("Stopping laser");
                }
            }
        }
        else
        {
            if(Time.time - startCharge > 1.0f)
            {
                laserAudio.Play();
                charging = false;
                firing = true;
                startLaser = Time.time;
                //Debug.Log("Starting to fire at player");
            }
        } 
        
    }

    bool playerInView()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 origin = transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && 
            hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")) {
                return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            HandleDamageCollision(20, other.gameObject.transform.position);
        }
    }

    void HandleDamageCollision(int damage, Vector3 position)
    {
        health -= damage;
        //  Healthbar for Boss?  hb.setHealth(health);
        // ParticleSystem ps = Instantiate(_onHitEffect, position, Quaternion.identity);
        //ps.Play();
        //StartCoroutine(waitForExplosion(ps));
        if( health <= 0 ) Destroy(gameObject);
    }

    // IEnumerator waitForExplosion(ParticleSystem ps)
    // {
    //     yield return new WaitForSeconds(.25f);
    //     ps.Stop();
    //     if( health <= 0 ) Destroy(gameObject);
    // }
}
