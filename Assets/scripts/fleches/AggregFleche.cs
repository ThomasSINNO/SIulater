﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregFleche : FlecheScript {

    
	void Start () {
        isActive = false;
        tab = new List<GameObject>();
        arrow = GameObject.Find("Arrow");
        type = typearrow.AGGREG;
        gameObject.tag = "ArrowButton";
    }
	
	
}
