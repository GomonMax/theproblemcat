using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // ��� ��� ����� �������� � WORLD.POSITION i ��i�� ��� �i�����������

    [Header("Object")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform point;

    [Header("Set Settings")]
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.down, 0);

    void FixedUpdate()
    {
        //������ �������� �� ������
        transform.LookAt(player);
        // ��� ����� ���� �� ������ ��� � �������
        worldPosition.y = 1;
        //����� ������ i ������ ������
        Vector2 screen = new Vector2(Screen.width, Screen.height);

        //if (Input.mousePosition.y < 1700)
        //if (Input.mousePosition.y < screen.y - ((screen.y * 10) / 100))
        //{
            //����� ���������� �����
            var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //��� �� ������� ����
            var normalizePos = mousePos / screen;
            //������ ����� �� ���� ������ i ��i�� ���������
            var clampPos = new Vector2(normalizePos.x * 6 - 3, normalizePos.y * 10 - 5);

            //��������� �i �������� WORLD.POSITION
            worldPosition.z = clampPos.y;
            worldPosition.x = clampPos.x;

            //���������� �������� �������IU �����II ����� �����i� WORLD.POSITION 
            transform.localPosition = worldPosition;
            
            //��i���(������IU �����II) ���������� i��������i ���������� �����
            point.localPosition = new Vector3(-worldPosition.x, worldPosition.y + 1, -worldPosition.z);
            point.transform.LookAt(player);
        //}
    }


}


