using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppFeatures : MonoBehaviour
{
    [SerializeField] Button close; 
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
