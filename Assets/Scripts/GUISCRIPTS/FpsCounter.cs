using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public int FPS { get; private set; }
    private float Timer = 0.3f;

    void Update()
    {
        if (Timer <= 0)
        {
            FPS = (int)(1f / Time.unscaledDeltaTime);
            FPS = Mathf.Clamp(FPS, 0, 144);
            Timer = 0.3f;
        }
        Timer -= Time.deltaTime;
    }
}
