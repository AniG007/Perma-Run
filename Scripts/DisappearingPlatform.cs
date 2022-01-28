using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] SpriteRenderer platform;
    [SerializeField] Collider2D col;
    [SerializeField] byte speed;
    [SerializeField] byte speed2;
    Color32 color;
    bool decrease;
    int i = 0;
    void Start()
    {
        color =  platform.color;
        if (color.a == 0)
            decrease = false;
        else
            decrease = true;
    }

    private void Update()
    {
        if (decrease == true)
        {
            color.a -= speed;
            platform.color = color;
            if (color.a == 0)
                decrease = false;

            if (color.a == 100)
                col.enabled = false;

        }

        if (decrease == false)
        {
            color.a += speed2;
            platform.color = color;

            if (color.a == 255)
                decrease = true;

            if (color.a == 150)
                col.enabled = true;
        }
    }
}
