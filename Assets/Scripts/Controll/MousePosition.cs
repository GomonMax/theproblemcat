using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // код щоб мишка рухалася у WORLD.POSITION i поiнт був вiдзеркалений

    [Header("Object")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform point;

    [Header("Set Settings")]
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.down, 0);

    void FixedUpdate()
    {
        //курсор дивиться на гравця
        transform.LookAt(player);
        // щоб мишка була на одному рівні з гравцем
        worldPosition.y = 1;
        //берем ШИРИНУ i ВИСОТУ екрану
        Vector2 screen = new Vector2(Screen.width, Screen.height);

        //if (Input.mousePosition.y < 1700)
        //if (Input.mousePosition.y < screen.y - ((screen.y * 10) / 100))
        //{
            //берем КООРДИНАТИ мишки
            var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //сам не памятаю нашо
            var normalizePos = mousePos / screen;
            //робимо рамки по яким курсор i поiнт крутиться
            var clampPos = new Vector2(normalizePos.x * 6 - 3, normalizePos.y * 10 - 5);

            //присоюемо цi значення WORLD.POSITION
            worldPosition.z = clampPos.y;
            worldPosition.x = clampPos.x;

            //присвоюемо значення ЛОКАЛЬНIU ПОЗИЦII мишки позицiю WORLD.POSITION 
            transform.localPosition = worldPosition;
            
            //поiтну(ЛОКАЛНIU ПОЗИЦII) присвоюемо iнверсивнi КООРДИНАТИ мишки
            point.localPosition = new Vector3(-worldPosition.x, worldPosition.y + 1, -worldPosition.z);
            point.transform.LookAt(player);
        //}
    }


}


