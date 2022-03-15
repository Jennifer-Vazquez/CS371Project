using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] Vector3 rotationSpeedPerSecond;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeedPerSecond * Time.deltaTime);
    }
}
