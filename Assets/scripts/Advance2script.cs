﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advance2script : MonoBehaviour {

    GameObject success;
    // Use this for initialization
    void Start()
    {
        success = GameObject.Find("Success");
    }


    public int width;
    public int height;

    void OnGUI()
    {
        int buttonWidth = width;
        int buttonHeight = height;

        // Affiche un bouton pour démarrer la partie
        if (
          GUI.Button(
            new Rect(
             0, 0,
              buttonWidth,
              buttonHeight
            ),
            "Passer"
          )
        )
        {
            GameObject suc = GameObject.Instantiate(success);
            suc.transform.position = new Vector3(0, 0, 0);


        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
