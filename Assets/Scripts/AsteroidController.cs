using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float health = 10;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BossBullet"))
        {
            health--;
            if (health < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
