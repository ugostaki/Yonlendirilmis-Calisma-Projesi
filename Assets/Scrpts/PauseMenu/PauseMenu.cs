using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausep;
    public GameObject pauseMenuUI;
    public int deger;
    
    void Start()
    {
        pausep.SetActive(false);
        deger = PlayerPrefs.GetInt("SampleScene");

    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                
            }
        }
    }
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    [System.Obsolete]
    public void butondan_gelen(string ne_geldi)
    {
        if (ne_geldi == "pause")
        {
            pausep.SetActive(true);
            Time.timeScale = 0;
        }
        else if (ne_geldi == "devamet")
        {
            pausep.SetActive(false);
            Time.timeScale = 1;
        }
        else if (ne_geldi == "SampleScene")
        {
            Application.LoadLevel(1);
            Time.timeScale = 1;
        }
        else if(ne_geldi == "MainMenu")
        {
            Application.LoadLevel(0);
            Time.timeScale = 1;
        }
    }
    public void hangi_level(int level)
    {
        deger = level;
        PlayerPrefs.SetInt("SampleScene", deger);
    }
}
