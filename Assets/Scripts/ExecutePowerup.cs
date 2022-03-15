using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePowerup : MonoBehaviour
{
    PlayerControllerCC Player;
    public PowerupUI powerupUI;
    private float laserStartTime;
    void Start()
    {
        Player = FindObjectOfType<PlayerControllerCC>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Execute(Player.powerUp);
            Player.powerUp = "none";
            powerupUI.clearPowerup();
        }

        if (Player.laserActivated == true && (Time.time - laserStartTime > 0.3f))
        {
            Player.laser.gameObject.SetActive(false);
            Player.laserAudio.gameObject.SetActive(false);
            Player.laserActivated = false;
        }
    }

    private void Execute(String equipped)
    {
        Debug.Log(equipped);
        if (equipped == "none"){
            Debug.Log("No Powerup");
        }
        else if (equipped == "LaserPowerup(Clone)")
        {
            Debug.Log("laser used");
            Player.laser.gameObject.SetActive(true);
            Player.laserAudio.gameObject.SetActive(true);
            Player.laserActivated = true;
            laserStartTime = Time.time;
        }
        
    }
}
