using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinText : MonoBehaviour
{
    //��� �����I���� ��� ���������� "�������I� �����"

    [Header("Object")]
    [SerializeField] private Transform _object; //��� �������
    [SerializeField] private Transform camera;  //������(�� ��i �������� �����)
    [SerializeField] private Vector3 posNow;    //����� ��� �������� ���������� �������� i ���i���� �� Y
    public TextMeshPro distText;                //3� �����
    public ScoreObject _scoreObject;            //������ � ���������� SCORE

    [Header("Set Settings")]
    [SerializeField] private float maxHeight;   //������ �� ��i� ���� �����

    [Header("Settings")]
    private float minDist;


    void FixedUpdate()
    {
        transform.LookAt(camera);     //����� �������� �� ������
        posNow = _object.position;    //���������� �������� �����������
        posNow.y += maxHeight;        //�� ��� ������ �i����� �����
        transform.position = posNow;  //�����i���� ����� �� ��������

        //����� ����� �������� �� �������, ��� �� ��������� �� Y
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z); 
        if (minDist <= (int)_scoreObject.dist) //���� ������� �������i� ���� ����� �� ������� �������i�
        {
            //�� ������� �������i� ����������� �� �i���� ����� + ���� ���� ===== ������I� �������I�
            minDist = (int)_scoreObject.dist;
            //����� �i������� (4/5 - ���������)
            distText.text = ((int)_scoreObject.dist).ToString() + "/5";
        }
    }
}
