﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieces: MonoBehaviour
{

    public RandoMazeBoard boardTurnSystem;
    public TurnClass turnClass;
    public EndTurn endPlayerTurn;
    keyMove2 move;
    navMashMove NVM;
    public bool isTurn = false;     //bool to check if itz the Player's turn

    private void Start()
    {
        boardTurnSystem = GameObject.Find("Plane").GetComponent<RandoMazeBoard>();
        move = GetComponent<keyMove2>();
        NVM = GetComponent<navMashMove>();

        //for each gameObject of Turnclass in the List of "Board"
        foreach (TurnClass element in boardTurnSystem.playersGroup)
        {
            //if the gameObject Name matches the gameObject name in "Board" /check if gameObject is registered in "Board"
            //add reference of element instance into turnclass
            if (element.playerGameObject.name == gameObject.name)
                turnClass = element;
        }

    }

    private void Update()
    {
        //set value of isTurn equal to value that current Player holds
        isTurn = turnClass.isTurn;

        //if isTurn = true
        if(isTurn)
        {
            move.moving = true;
            NVM.moving2 = true;

            //check if Button was pressed
            if (endPlayerTurn.buttonPressed == true)
            {
                isTurn = false;     //set isTurn false again
                turnClass.isTurn = isTurn;      //turnClass.isTurn = false
                turnClass.wasTurnPrev = true;   //set the Players wasTurnPrev to true

                endPlayerTurn.buttonPressed = false;    //change EndTurn Button was pressed to false again

                move.moving = false;
                NVM.moving2 = false;
            }
        }
    }
}