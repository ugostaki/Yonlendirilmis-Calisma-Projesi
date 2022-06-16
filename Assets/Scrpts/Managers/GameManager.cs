using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int collectedCoins,victoryCondition =5;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    private static GameManager instance;
    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }
    

   
   
    Player player;

    //UI
    public Slider healthBar;
   

    
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBar.maxValue = player.maxPlayerHealth;
        UlManager.MyInstance.UpdateCoinUI(collectedCoins, victoryCondition);
    }

    
    void Update()
    {
        


        if (player.isDead)
        {
            Invoke("RestartGame",2);
        }
        UpdateUI();
    }
    public void RestartGame()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.name);

        player.RecoverPlayer();
    }

    void UpdateUI()
    {
        healthBar.value = player.currentPlayerHealth;
        if (player.currentPlayerHealth <= 0)
            healthBar.minValue = 0;
    }
    
    public void AddCoins(int _coins)
    {
        collectedCoins += _coins;
        UlManager.MyInstance.UpdateCoinUI(collectedCoins, victoryCondition);
    }

    public void Finish()
    {
        if (collectedCoins >= victoryCondition)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UlManager.MyInstance.ShowVictoryCondition(collectedCoins, victoryCondition);
        }
    }
   
}
