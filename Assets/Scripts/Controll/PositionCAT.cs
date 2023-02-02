using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCAT : MonoBehaviour
{
    //��� �������� ������� ����

    [Header("Object")]
    [SerializeField] private Transform playerBox;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform point;
    public ControllPlayer player;

    [Header("Set Settings")]
    public float speed;
    public float distNow;
    public bool isGround;


    void Start()
    {
        //������� �������� ��������� i ������� ��� isGround ��� �� ������� false(�� ��� ��� �����)
        _rigidbody = GetComponent<Rigidbody>();
        isGround = false;
    }

    void Update()
    {
        // ��� ��� ���� ������, ��� ��� �I��������I ��I�� �I� ������

        //���� ������ ������ ������
        if (Input.touchCount > 0)
        {
            //�� ������
            Touch touch = Input.GetTouch(0);
            //���� �i������� ����� i �i� �� ����i
            if (touch.phase == TouchPhase.Ended && isGround)
            {
                //�� �i��������� ������(��� ������� ���i �i�� ����������� �� � ��� �������)
                distNow = player.dist * 0.45f;
                _rigidbody.velocity = new Vector3(point.localPosition.x * (distNow), point.localPosition.y * (speed * (13.5f/Mathf.Clamp(player.dist, 2.5f, 5f))), point.localPosition.z * (distNow)) * speed;
                isGround = false;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //���� �I� ���������� ����i i KIT ���� ���� �������� ����i���, �� isGround = true;
        if (collision.gameObject.tag == "Ground")
        {
            if (_rigidbody.velocity.magnitude <= 1.5f)
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }
        }
    }
}
