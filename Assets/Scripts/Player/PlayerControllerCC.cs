using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Based on Unity documentation for CharacterController.Move */

public class PlayerControllerCC : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpSpeed = 7f;
    [SerializeField] float _gravity = 2f;
    [SerializeField] float _gravityJumpModifier = 2f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float gasPowerUpTime = 15f;
    public int curHealth = 100;
    public TrackLives lifeTracker;
    public HealthBar hb;
    public Vector3 wormholePos1;
    public Vector3 wormholePos2;
    public string powerUp = "none";
    public Countdown countdown;
    public PowerupSpawner powerupSpawner;
    public PowerupSpawner gasSpawner;
    public AudioSource laserAudio;

    CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    float _gravityDownBoost = 2;

    public ParticleSystem explosion;
    public bool laserActivated = false;
    public LineRenderer laser;
    public Transform shootPoint;
    public bool huangsMode = false;

    public PowerupUI powerupUI;

    public int mineCount = 3; 
    [SerializeField] GameObject playerMinePrefab;
    public bool canPlaceMine;


    void Start()
    {
        explosion.Stop();
        _characterController = GetComponent<CharacterController>();
        lifeTracker = FindObjectOfType<TrackLives>();
        curHealth = maxHealth;
        hb.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if(huangsMode == false){
            gasOut(countdown.currentTime);
        }
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= _speed;

            // Face in the right direction
            if (_moveDirection != Vector3.zero)
            {
                transform.forward = _moveDirection;
            }

            // Handle jumping
            // if (Input.GetButton("Jump"))
            // {
            //     _moveDirection.y = _jumpSpeed;
            // } 
        }

        // Need to continually apply gravity to player
        _moveDirection.y -= _gravityJumpModifier * _gravity * Time.deltaTime;

        // If jumping, apply more gravity on the way down to make jump "feel" better
        if (_moveDirection.y < 0)
        {
            _moveDirection.y -= _gravityDownBoost * _gravityJumpModifier * _gravity * Time.deltaTime;
        }

         if (Input.GetKeyDown(KeyCode.R)){
              if(mineCount > 0 && canPlaceMine){
                  placeMine();
                  mineCount--;
              }
            }
         if (Input.GetKeyDown(KeyCode.H)){
              if(huangsMode == false){
                  huangsMode = true;
                }else{
                      huangsMode = false;
                  }
              }
            


        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void LateUpdate()
    {
        if(laserActivated)
        {
            laser.gameObject.SetActive(true);
            laserAudio.gameObject.SetActive(true);
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var mouseHit, Mathf.Infinity);
            Vector3 direction = new Vector3(mouseHit.point.x, shootPoint.position.y ,mouseHit.point.z) - shootPoint.position;
            Physics.Raycast(shootPoint.position, direction, out var raycastHit, Mathf.Infinity);
            laser.SetPosition(0, shootPoint.position);
            laser.SetPosition(1, new Vector3(raycastHit.point.x, shootPoint.position.y, raycastHit.point.z));
            // Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var mouseHit, Mathf.Infinity);
            // Vector3 direction = new Vector3(mouseHit.point.x, transform.position.y ,mouseHit.point.z) - transform.position;
            // Physics.Raycast(transform.position, direction, out var raycastHit, Mathf.Infinity);
            // laser.SetPosition(0, transform.position);
            // laser.SetPosition(1, new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
            if(raycastHit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyAI enemy = raycastHit.collider.gameObject.GetComponent<EnemyAI>();
                enemy.takeDamage(1);
                Debug.Log(enemy.curHealth);
            }
            Debug.Log(raycastHit.collider.gameObject.tag);
        }
    }
    public void takeDamage(int damage)
    {
        curHealth -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            if(huangsMode == false)
            HandleDestructiveCollision(30);
        }

        if (other.CompareTag("BossBullet"))
        {
            if(huangsMode == false)
            HandleDestructiveCollision(20);
        }
        
        if (other.CompareTag("Bullet"))
        {
            if(huangsMode == false){
            Destroy(other.gameObject);
            HandleDestructiveCollision(20);
            }
        }

        if (other.CompareTag("Mine"))
        {
            if(huangsMode == false){
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);
            HandleDestructiveCollision(50);
            }
        }

        if (other.CompareTag("Wormhole1"))
        {
            StartCoroutine("TeleportToP2");
        }


        if (other.CompareTag("Wormhole2"))
        {
            StartCoroutine("TeleportToP1");
        }

        if (other.CompareTag("Missile"))
        {
            if(huangsMode == false){
            Debug.Log("Missile hit player.");
            takeDamage(10);
            hb.setHealth(curHealth);
            isDead(curHealth);
            explosion.Play();
            StartCoroutine("waitForExplosion");
            }
        }

        if (other.CompareTag("GasPowerup"))
        {
            countdown.currentTime = Mathf.Min(countdown.duration, countdown.currentTime + gasPowerUpTime);
            gasSpawner.reset();
        }
        if (other.CompareTag("HealthPowerup"))
        {
            Debug.Log(curHealth);
            curHealth = Mathf.Min(maxHealth, curHealth + 50);
            hb.setHealth(curHealth);
            powerupSpawner.reset();
            Debug.Log(curHealth);
        }

        if(other.CompareTag("Powerup") && powerUp == "none")
        {
            Debug.Log("Pickup");
            powerUp = other.name;
            powerupUI.showPowerup("laser");
            Destroy(other);
            powerupSpawner.reset();
        }

        if (other.CompareTag("MinePowerup") && powerUp == "none")
        {
            mineCount += 1;
            Destroy(other);
            Debug.Log(mineCount);
            powerupSpawner.reset();
        }
    }

    public void HandleDestructiveCollision(int damage)
    {
             takeDamage(damage);
             hb.setHealth(curHealth);
             isDead(curHealth);
             explosion.Play();
             StartCoroutine("waitForExplosion");       
    }

    IEnumerator TeleportToP2()
    {
        _characterController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = wormholePos2;
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled = true;
    }

    IEnumerator TeleportToP1()
    {
        _characterController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = wormholePos1;
        yield return new WaitForSeconds(0.5f);
        _characterController.enabled = true;
    }


    private IEnumerator waitForExplosion()
    {
        yield return new WaitForSeconds(.25f);
        explosion.Stop();
    }

    
      void placeMine()
    {
        Vector3 placement = new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z); //had to hard code in the y 
        var mine = Instantiate(playerMinePrefab, placement, gameObject.transform.rotation);
    }

    void isDead(int curHealth)
    {
        if (curHealth <= 0)
        {
            lifeTracker.numLives -= 1;
            int numLives = lifeTracker.numLives;
            if (numLives >= 1)
            {
                Debug.Log("Lives left: " + numLives);
               // SceneManager.LoadScene(40); //LoseLife scene
                 SceneManager.LoadScene("LoseLife");
            }
            else
            {
                Debug.Log("die now");
                SceneManager.LoadScene("Death Screen"); //Death Screen scene
            }
        }
    }

    
      void gasOut(float curGas)
    {
    if(huangsMode == false){
        if (curGas <= 0f)
        {
            lifeTracker.numLives -= 1;
            int numLives = lifeTracker.numLives;
            if (numLives >= 1)
            {
                Debug.Log("Lives left: " + numLives);
                SceneManager.LoadScene(45); //LoseLifeGas scene
            }
            else
            {
                Debug.Log("die now");
                SceneManager.LoadScene(44); //Death Screen scene
            }
        }
    }
    }

}