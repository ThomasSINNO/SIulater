﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupprFleche : FlecheScript {

    void Start()
    {
        isActive = false;
        tab = new List<GameObject>();
        arrow = GameObject.Find("Arrow");
        type = typearrow.SUPPR;
        gameObject.tag = "ArrowButton";
    }
}
