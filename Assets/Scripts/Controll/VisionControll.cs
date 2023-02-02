using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionControll : MonoBehaviour
{
    // код ВКЛЮЧАЕ I ВИКЛЮЧАЕ курсор мишки

    [Header("Object")]
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject point;


    void Start()
    {
        //з початку обекти виключенi
        cursor.SetActive(false);
        point.SetActive(false);
    }

    void Update()
    {
        //якщо нажато на екран, то включается курсор мишки
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            cursor.SetActive(true);
            point.SetActive(true);
        }

        //якщо не нажато на екран, то виключается курсор мишки
        if (Input.touchCount == 0 || Input.GetMouseButtonUp(0))
        {
            cursor.SetActive(false);
            point.SetActive(false);
        }
    }
}
