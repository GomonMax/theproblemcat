using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private int sceneNow;
    void Start()
    {
        sceneNow = SceneManager.GetActiveScene().buildIndex;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(sceneNow);
    }
}
