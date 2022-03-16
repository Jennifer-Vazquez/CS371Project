using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public bool powerupSpawned = false;
    //Populate with points, each a different spawn location for the powerups (will be per level)
    public List<Vector3> points;
    // Start is called before the first frame update
    public List<GameObject> powerups;
    public float time; 
    void Start()
    {
        time = Time.time;       
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - time) > 7.0f && powerupSpawned == false)
        {
            int pointIndex = Random.Range(0, points.Count);
            int powerupIndex = Random.Range(0, powerups.Count);
            powerupSpawned = true;
            Instantiate(powerups[powerupIndex], points[pointIndex], Quaternion.identity);
            time = Time.time;
        }
    }

    public void reset()
    {
        time = Time.time;
        powerupSpawned = false;
    }
}
