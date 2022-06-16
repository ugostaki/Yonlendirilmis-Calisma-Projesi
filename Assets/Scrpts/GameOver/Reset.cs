using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Reset : MonoBehaviour
{
   
    public void Resett()
    {
        PlayerPrefs.DeleteAll();
    }
}

