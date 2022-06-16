using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Zaman : MonoBehaviour
{
    public Text Zaman_T;
    public float time;
    public GameObject panelim;
   

    private void Start()
    {
        time = 35;
        Zaman_T.text = "" + time;
        panelim.SetActive(false);
        Time.timeScale = 1;

    }
    private void Update()
    {
        time -= Time.deltaTime;
        Zaman_T.text = "" + (int)time;
        if (time <= 0)
        {
            panelim.SetActive(true);
            Time.timeScale = 0;
           
        }
    }
}
