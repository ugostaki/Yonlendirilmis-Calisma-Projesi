using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
   

    public void yenidenBasla()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
   

}
