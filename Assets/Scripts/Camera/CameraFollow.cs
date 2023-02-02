using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject player;

    private float posX; private float posY; private float posZ;

    [Header("Set Settings")]
    // камера буде на вiдстаннi Y i Z
    [SerializeField] private float Y;
    [SerializeField] private float Z;

    void Update()
    {
        // запис поз. камери вiдносно гравц€
        posX = player.transform.position.x;
        posY = player.transform.position.y;
        posZ = player.transform.position.z;


        gameObject.transform.position = new Vector3(posX, posY + Y, posZ + Z);
    }
}
