using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    private static PositionKeeper instance;
    public Vector2 lastCheckPointPosition;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }
}
