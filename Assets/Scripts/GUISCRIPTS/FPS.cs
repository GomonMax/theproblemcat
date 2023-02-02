using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(FpsCounter))]
public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public FpsCounter fpsCounter;

    void Awake()
    {
        fpsCounter = GetComponent<FpsCounter>();
    }

    void Update()
    {
        fpsText.text = fpsCounter.FPS.ToString() + " FPS";
    }

}
