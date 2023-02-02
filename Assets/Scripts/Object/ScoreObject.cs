using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    // Очки + оптимiзацiя
    public float dist;
    private Vector3 startDist;
    public float maxDist = 5;
    public float minDist = 0;
    public float distWithPlayer;
    private float timer = 4;

    public float score;
    public bool isMax;


    private Rigidbody _rigidbody;
    public ScoreLVL _scoreLVL;
    public Transform player;
    public GameObject textDist;
    public GameObject shadow;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        this.startDist = transform.localPosition;
        this.isMax = false;
    }

    void FixedUpdate()
    {
        minDist = dist;
        dist = Vector3.Distance(this.startDist, this.transform.localPosition);
        dist = Mathf.Clamp(dist, 0, maxDist);
        distWithPlayer = Vector3.Distance(player.position, transform.position);

        if (distWithPlayer < 4)
        {
            _rigidbody.isKinematic = false;
            isTextVisible();
            isShadowVisible();
        }
        else if (distWithPlayer > 4 && _rigidbody.velocity.magnitude < 0.3f)
        {
            isTextVisible();
            isShadowVisible();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                _rigidbody.isKinematic = true;
                timer = 4;
            }
        }

        if (((dist > minDist) && (dist <= maxDist)) || dist == 0)
        {
            score = (int)(dist * 10);
            if (dist >= maxDist)
            {
                this.isMax = true;
            }
        }
        if (this.isMax)
        {
            _scoreLVL.allScore += score;
            this.isMax = false;
        }
    }

    public void isTextVisible()
    {
        if (player.position.z - 7 > transform.position.z)
        {
            textDist.SetActive(false);
        }
        else if (player.position.z < transform.position.z + 7)
        {
            textDist.SetActive(true);

            if (player.position.z < transform.position.z - 20 || (player.position.x < transform.position.x - 15 || player.position.x > transform.position.x + 15))
            {
                textDist.SetActive(false);
            }
            else
            {
                textDist.SetActive(true);

            }
        }
    }
    public void isShadowVisible()
    {
        if (player.position.z - 7 > transform.position.z)
        {
            shadow.SetActive(false);
        }
        else if (player.position.z < transform.position.z + 7)
        {
            shadow.SetActive(true);

            if (player.position.z < transform.position.z - 35 || (player.position.x < transform.position.x - 15 || player.position.x > transform.position.x + 15))
            {
                shadow.SetActive(false);
            }
            else
            {
                shadow.SetActive(true);

            }
        }
    }
}
