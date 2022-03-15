using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public GameObject laserPowerup;

    public void showPowerup(string name)
    {
        if (name == "laser")
        {
            laserPowerup.SetActive(true);
            Debug.Log("Laser powerup");
        }
    }    
    public void clearPowerup()
    {
        laserPowerup.SetActive(false);
    }
}
