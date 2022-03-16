using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Weapon _gun;
    [SerializeField] float _ricochetSpeed = 10f;
    Rigidbody rBody;
    Vector3 velo;
    public void SetGun(Weapon gun) => _gun = gun;
    bool hasCollided = false;
   
    //public GameObject explosion;


    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        velo = rBody.velocity;
        Destroy(gameObject, 10f);
        //explosion.SetActive(false);
    }

  
    void OnCollisionEnter(Collision other)
    {
        Vector3 _wallCollision = other.GetContact(0).normal;
        //Debug.Log("Hit!");
     if (hasCollided) { Destroy(gameObject); }
    else
        rBody.velocity = Vector3.Reflect(velo, _wallCollision).normalized * _ricochetSpeed;
            hasCollided = true;
        
    }
}