using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{
    //��� ��� ����i��� ART(�� �����) --- ��� ����� ��� ������i���(������� �����)

    [Header("Object")]
    [SerializeField] private Transform point;
    [SerializeField] private Transform cursor;

    [Header("Art")]
    // ���
    [SerializeField] private Transform art;
    //������� ��������
    [SerializeField] private Transform model;
    [Header("Set Settings")]
    public float dist;

    private void FixedUpdate()
    {
        // ������ ��� ART ������� �� ��������
        art.rotation = point.localRotation;
        transform.position = model.transform.position;

        dist = Vector3.Distance(point.position, transform.position);  
    }
}
