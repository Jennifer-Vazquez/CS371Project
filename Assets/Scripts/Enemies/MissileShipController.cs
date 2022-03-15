using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MissileShipController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] int health;

    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(target.transform.position);
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
        ParticleSystem ps = Instantiate(explosion, position, Quaternion.identity);
        ps.Play();
        StartCoroutine(waitForExplosion(ps));
    }

    IEnumerator waitForExplosion(ParticleSystem ps)
    {
        yield return new WaitForSeconds(.25f);
        ps.Stop();
        if( health <= 0 ) Destroy(gameObject);
    }
}