 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReversePlayerCC : MonoBehaviour 

{ 

[SerializeField] float _speed = 5f; 

[SerializeField] float _jumpSpeed = 7f; 

[SerializeField] float _gravity = 2f; 

[SerializeField] float _gravityJumpModifier = 2f; 

[SerializeField] int maxHealth = 100; 

public int curHealth = 100; 

public HealthBar hb; 

public Vector3 wormholePos1; 

public Vector3 wormholePos2; 

CharacterController _characterController; 

Vector3 _moveDirection = Vector3.zero; 

float _gravityDownBoost = 2; 

 
 

public ParticleSystem explosion; 

 
 

void Start(){ 

explosion.Stop(); 

_characterController = GetComponent<CharacterController>(); 

curHealth = maxHealth; 

hb.setMaxHealth(maxHealth); 

} 

 
 

void Update() 

{ 

 
 
 

if (_characterController.isGrounded) 

{ 

// We are grounded, so recalculate 

// move direction directly from axes 

_moveDirection = new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal")); 

_moveDirection *= _speed; 

 
 

// Face in the right direction 

if (_moveDirection != Vector3.zero) 

{ 

transform.forward = _moveDirection; 

} 

 
 

// Handle jumping 

if (Input.GetButton("Jump")) 

{ 

_moveDirection.y = _jumpSpeed; 

} 

 
 
 

} 

 
 

// Need to continually apply gravity to player 

_moveDirection.y -= _gravityJumpModifier * _gravity * Time.deltaTime; 

 
 

// If jumping, apply more gravity on the way down to make jump "feel" better 

if (_moveDirection.y < 0) 

{ 

_moveDirection.y -= _gravityDownBoost * _gravityJumpModifier * _gravity * Time.deltaTime; 

} 

 
 

// Move the controller 

_characterController.Move(_moveDirection * Time.deltaTime); 

 
 

} 

 
 

public void takeDamage(int damage){ 

curHealth -= damage;  

} 

 
 
 

void OnTriggerEnter(Collider other) 

{ 

if (other.CompareTag("Bullet")) 

{ 

Destroy(other); 

takeDamage(20); 

hb.setHealth(curHealth); 

explosion.Play(); 

StartCoroutine("waitForExplosion"); 

} 

 
 

if (other.CompareTag("Wormhole1")) 

{ 

StartCoroutine("TeleportToP2"); 

} 

 
 

if (other.CompareTag("Wormhole2")) 

{ 

StartCoroutine("TeleportToP1"); 

} 

} 

 
 

IEnumerator TeleportToP2(){ 

_characterController.enabled = false; 

yield return new WaitForSeconds(0.1f); 

gameObject.transform.position = wormholePos2; 

yield return new WaitForSeconds(0.1f); 

_characterController.enabled = true; 

} 

 
 

IEnumerator TeleportToP1(){ 

_characterController.enabled = false; 

yield return new WaitForSeconds(0.1f); 

gameObject.transform.position = wormholePos1; 

yield return new WaitForSeconds(0.1f); 

_characterController.enabled = true; 

} 

 
 
 

private IEnumerator waitForExplosion(){ 

yield return new WaitForSeconds(.25f); 

explosion.Stop(); 

} 

} 