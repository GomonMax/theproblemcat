using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionControll : MonoBehaviour
{
    // ��� ������� I �������� ������ �����

    [Header("Object")]
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject point;


    void Start()
    {
        //� ������� ������ ��������i
        cursor.SetActive(false);
        point.SetActive(false);
    }

    void Update()
    {
        //���� ������ �� �����, �� ���������� ������ �����
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            cursor.SetActive(true);
            point.SetActive(true);
        }

        //���� �� ������ �� �����, �� ����������� ������ �����
        if (Input.touchCount == 0 || Input.GetMouseButtonUp(0))
        {
            cursor.SetActive(false);
            point.SetActive(false);
        }
    }
}
