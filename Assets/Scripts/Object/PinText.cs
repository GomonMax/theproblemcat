using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinText : MonoBehaviour
{
    //код ПРИКРIПЛЮЕ над ПРЕДМЕТАМИ "ДИСТАНЦIЯ КИДКА"

    [Header("Object")]
    [SerializeField] private Transform _object; //сам предмет
    [SerializeField] private Transform camera;  //камера(на неi дивиться цифра)
    [SerializeField] private Vector3 posNow;    //треба щоб записати координати предмета i помiняти по Y
    public TextMeshPro distText;                //3д текст
    public ScoreObject _scoreObject;            //скрипт з обрахунком SCORE

    [Header("Set Settings")]
    [SerializeField] private float maxHeight;   //висота на якiй буде текст

    [Header("Settings")]
    private float minDist;


    void FixedUpdate()
    {
        transform.LookAt(camera);     //текст дивиться на камеру
        posNow = _object.position;    //координати предмета записуються
        posNow.y += maxHeight;        //на яку висоту пiдняти текст
        transform.position = posNow;  //прикрiпили текст до предмета

        //ТЕКСТ тепер дивиться на ПРЕДМЕТ, але НЕ КРУТИТЬСЯ по Y
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z); 
        if (minDist <= (int)_scoreObject.dist) //якщо ОСТАННЯ дистанцiя буде МЕНША за ЗАРАШНЮ дистанцiю
        {
            //то ОСТАННЯ дистанцiя ОКРУГЛИТЬСЯ до цiлого числа + вона буде ===== ОСТАННIЙ ДИСТАНЦIЇ
            minDist = (int)_scoreObject.dist;
            //текст мiняеться (4/5 - наприклад)
            distText.text = ((int)_scoreObject.dist).ToString() + "/5";
        }
    }
}
