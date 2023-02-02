using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCAT : MonoBehaviour
{
    //код управляе ПРИЖКОМ кота

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
        //задаемо значення ригидбоди i ставимо щоб isGround був на початку false(це щоб там треба)
        _rigidbody = GetComponent<Rigidbody>();
        isGround = false;
    }

    void Update()
    {
        // тут код який робить, щоб при ВIДПУСКАННI КЛIКА КIТ ПРИГАВ

        //якщо нажать бильше одного
        if (Input.touchCount > 0)
        {
            //чи нажАто
            Touch touch = Input.GetTouch(0);
            //якщо вiдпустив екран i кiт на землi
            if (touch.phase == TouchPhase.Ended && isGround)
            {
                //то вiдбудеться прижок(тут формули менi лiнь розписувати що я тут наробив)
                distNow = player.dist * 0.45f;
                _rigidbody.velocity = new Vector3(point.localPosition.x * (distNow), point.localPosition.y * (speed * (13.5f/Mathf.Clamp(player.dist, 2.5f, 5f))), point.localPosition.z * (distNow)) * speed;
                isGround = false;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //якщо КIТ торкнеться землi i KIT буде мати невелику швидiсть, то isGround = true;
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
