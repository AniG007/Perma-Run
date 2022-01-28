using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject storage;
    [SerializeField] GameObject location;
    [SerializeField] GameObject mic;
    public void removeCamera()
    {
        cam.SetActive(false);
    }

    public void removeStorage()
    {
        storage.SetActive(false);
    }

    public void removeLocation()
    {
        location.SetActive(false);
    }

    public void removeMic()
    {
        mic.SetActive(false);
    }
}
