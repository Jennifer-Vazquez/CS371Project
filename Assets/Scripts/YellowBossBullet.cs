using UnityEngine;

public class YellowBossBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if( !collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}
