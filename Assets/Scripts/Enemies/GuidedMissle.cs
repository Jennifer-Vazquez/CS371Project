using UnityEngine;
using UnityEngine.AI;

public class GuidedMissle : MonoBehaviour
{
    // Note: using public, not serializefield, so can access in missilespawner script
    public GameObject target;
    public float timeTracking;
    [SerializeField] GameObject forceField;

    NavMeshAgent _navMeshAgent;
    Rigidbody _rigidbody;
    bool _canCollide;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTracking > 0)
        {
            _navMeshAgent.SetDestination(target.transform.position);
            timeTracking -= Time.deltaTime;
        }
        else
        {
            if (!_canCollide)
            {
                // Make into a projectile
                _canCollide = true;
                _navMeshAgent.enabled = false;
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                
                // Lose force field protection
                Destroy(forceField);

                // Go fast in current direction
                _rigidbody.velocity = transform.forward * _navMeshAgent.speed * 2f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Missle hit something: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (_canCollide) Destroy(gameObject);
    }
}