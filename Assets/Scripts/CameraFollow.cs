using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;

    public Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        cameraPosition = new Vector3(playerTransform.position.x, playerTransform.position.y +30 , playerTransform.position.z);
        transform.position = cameraPosition;
        transform.LookAt(playerTransform);
    }
}
