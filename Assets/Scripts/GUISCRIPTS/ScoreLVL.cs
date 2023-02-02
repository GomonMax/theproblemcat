using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreLVL : MonoBehaviour
{
    public float allScore;
    public TextMeshProUGUI scoreText;


    private void Start()
    {
        allScore = 0;
    }

    void Update()
    {
        scoreText.text = "score:" + allScore.ToString();
    }
}
