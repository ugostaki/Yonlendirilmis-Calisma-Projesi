using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UlManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtCoins, txtVictoryCondition;
    [SerializeField] GameObject victoryCondition;

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
    private static UlManager instance;
    public static UlManager MyInstance
    {
        get
        {
            if (instance == null)
                instance = new UlManager();
            return instance;
        }
    }
    public void UpdateCoinUI(int _coins, int _victoryCondition)
    {
        txtCoins.text = "Point :" + _coins + " / " + _victoryCondition;
    }

    public void ShowVictoryCondition(int _coins, int _victoryCondition)
    {
        victoryCondition.SetActive(true);
        txtVictoryCondition.text = "YOU NEED " + (_victoryCondition - _coins) + "MORE COINS";
    }

    public void HideVictoryCondition()
    {
        victoryCondition.SetActive(false);
       
    }
}
