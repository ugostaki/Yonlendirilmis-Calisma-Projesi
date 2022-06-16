using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //scriptin kendisi verible oldu
    public static SoundManager instance;
    public bool muted;
    Toggle toggle;


     void Awake()
    {
        if(toggle == null)
        {
            toggle = FindObjectOfType<Toggle>();
        }
    }



    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

     void Update()
    {
        if (muted)
        {     
            AudioListener.volume = 0;
        }
        else
        {          
            AudioListener.volume = 1;
        }
    }
    public void ToggleMusic(bool newValue)
    {
        muted = newValue;

        if (muted)
        {
            toggle.isOn = true;
            AudioListener.volume = 0;
        }
        else
        {
            toggle.isOn = false;
            AudioListener.volume = 1;
        }
    }


}
