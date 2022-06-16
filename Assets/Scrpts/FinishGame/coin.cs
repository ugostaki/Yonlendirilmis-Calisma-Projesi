﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : Collectable
{
    [SerializeField] int coinValue = 1;

    protected override void Collected()
    {
        GameManager.MyInstance.AddCoins(coinValue);
        Destroy(this.gameObject);
    }
}

