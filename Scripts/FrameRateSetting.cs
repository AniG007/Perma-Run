using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 60;
//        Debug.Log(Screen.currentResolution);
#endif
    }
}
