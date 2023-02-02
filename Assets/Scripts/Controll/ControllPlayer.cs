using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{
    //Код для крутiння ART(це кубик) --- код треба для управлiння(курсору мишки)

    [Header("Object")]
    [SerializeField] private Transform point;
    [SerializeField] private Transform cursor;

    [Header("Art")]
    // АРТ
    [SerializeField] private Transform art;
    //Основна моделька
    [SerializeField] private Transform model;
    [Header("Set Settings")]
    public float dist;

    private void FixedUpdate()
    {
        // робить щоб ART дивився за курсором
        art.rotation = point.localRotation;
        transform.position = model.transform.position;

        dist = Vector3.Distance(point.position, transform.position);  
    }
}
