﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy
{
    public override void Hit()
    {
        base.Hit();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}