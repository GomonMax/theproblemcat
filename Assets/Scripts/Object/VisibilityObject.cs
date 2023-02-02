using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    public float maxDistance;
    public GameObject obj;
    private bool Misha = false;


    void Update()
    {
        Ray ray = new Ray(player.transform.position, camera.transform.position - player.transform.position);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.tag == "Ground")
            {
                obj = hit.collider.gameObject;
                //
                Debug.Log(hit.collider.gameObject.name + " FALSE");
                Misha = true;
            }
        }
        else if(Misha == true)
        {
            //
            Debug.Log(obj.gameObject.name + " TRUE");
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
    }
}
