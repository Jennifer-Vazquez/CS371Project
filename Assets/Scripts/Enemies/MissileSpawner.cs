using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject missilePrefab;
    public float timeTracking;
    public float spawnRate;
    public float spawnNumber;
    [SerializeField] float spawnDistance;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("launchMissle", spawnRate, spawnRate);
    }

    void launchMissle()
    {
        float shipSide = -spawnDistance;
        for (int i = 0; i < spawnNumber; i++)
        {
            Vector3 missilePosition = new Vector3(spawnPoint.position.x + shipSide * i, spawnPoint.position.y, spawnPoint.position.z);
            shipSide = shipSide * -1;
            GameObject missile = Instantiate(missilePrefab, missilePosition, Quaternion.identity);
            GuidedMissle guidedMissle = missile.GetComponent<GuidedMissle>();
            guidedMissle.timeTracking = timeTracking;
            guidedMissle.target = target;
        }
    }
}