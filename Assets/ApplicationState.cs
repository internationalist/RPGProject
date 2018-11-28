using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationState : MonoBehaviour {
    public int frameRate = 30;
    public GameObject message;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = frameRate;
    }

}
