﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandoMazeBoard : MonoBehaviour {

    //List of Players
    public List<TurnClass> playersGroup;

    //treasures
    public int treasuresForPlayer;
    public GameObject UiPlayer;

    public bool giveCards = false;
    public List<GameObject> treasure;
    private int randomRemove;
    private int nmbCards;
    public GameObject player3Bild;
    public GameObject player4Bild;
    //public PlayerPieces playerPieces = new PlayerPieces();
    //public List<PlayerPieces> playerPieces;

    //gameObjects of Players
    public GameObject playerPiecePrefab;

    //temp PlayerNum Solution
    const int playerIndex = 2;
    const int removeNum = 0;
    
    public int playerNum = 4;
    private GameObject usable;
    
	// Use this for initialization
	void Start ()
    {
        //temp Solution changing number of players
        playersGroup.RemoveRange(playerIndex, removeNum);
        usable = GameObject.Find("Numbber");
        //GenerateBoard();

        //Reset turns in the beginning
        ResetTurns();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTurns();
        if (giveCards)
        {
            treasure = new List<GameObject>();
            foreach (GameObject treasures in GameObject.FindGameObjectsWithTag("treasure"))
            {
                treasure.Add(treasures);
            }
            //treasurecount has to be x*4
            while (treasure.Count > 0)
            {
                for (int p = 0; p < playerNum; p++)
                {
                    randomRemove = UnityEngine.Random.Range(0, treasure.Count - 1);
                    playersGroup[p].playerGameObject.GetComponent<PlayerPieces>().treasures.Add(treasure[randomRemove]);
                    treasure.RemoveAt(randomRemove);
                }
            }
            treasuresForPlayer = playersGroup[0].playerGameObject.GetComponent<PlayerPieces>().treasures.Count;
            giveCards = false;
            for (int i = 0; i < playerNum; i++)
            {
                playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().hasTreasures = true;
                for (int k = 0; k < playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().treasures.Count; k++)
                {
                    playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().treasures[k].GetComponent<TreasureCard>().TreasureCardObj.transform.position = this.GetComponentInParent<maze>().playerTreasurePos[i]+this.UiPlayer.transform.position;
                }
            }
        }
    }

    public void startGame()
    {
        giveCards = true;
        if (playerNum != 4)
        {
            switch (playerNum)
            {
                case 2:
                    playersGroup[3].playerGameObject.SetActive(false);
                    playersGroup[2].playerGameObject.SetActive(false);
                    player3Bild.SetActive(false);
                    player4Bild.SetActive(false);
                    break;
                case 3:
                    playersGroup[3].playerGameObject.SetActive(false);
                    player4Bild.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }

    public void addPlayer()
    {
        if (playerNum < 4)
        {
            Debug.Log("add Player");
            playerNum++;
            usable.GetComponent<Text>().text = Convert.ToString(playerNum);
            }
    }
    public void removePlayer()
    {
        if (playerNum > 2)
        {
            Debug.Log("remove Player");
            playerNum--;
            usable.GetComponent<Text>().text = Convert.ToString(playerNum);
        }
    }

    //reset turn to Beginning
    //Player1 starts
    //rest didn't have their turn yet in this round
    private void ResetTurns()
    {
        for(int i = 0; i < playerNum; i++)     //go through each player one by one
        {
            if(i == 0)  //start with first player
            {
                playersGroup[i].isTurn = true;
                playersGroup[i].wasTurnPrev = false;
            }
            else        //rest set both false
            {
                playersGroup[i].isTurn = false;
                playersGroup[i].wasTurnPrev = false;
            }
        }
        GetComponentInParent<maze>().pattern = true;
    }

    //will be called in Update()
    //Update Player Turns
        //set current Player to isTurn = true
        //set previous Player 
    private void UpdateTurns()
    {
        for(int i = 0; i < playerNum; i++) //go through each player
        {
            //if Player didn't have his turn yet
                //set isTurn to true
                //break the loop so it doesn't get resetted
            if (!playersGroup[i].wasTurnPrev)   
            {
                playersGroup[i].isTurn = true;
                break;
            }
            
                //if iteration = amount of players And the last Player had his turn
                    //reset the turns -> new round
            else if (i == playerNum - 1 && playersGroup[i].wasTurnPrev)
                ResetTurns();
        }
    }

    //create pieces on board
    //private void GenerateBoard()
    //{
    //    for(int i = 0; i < playerNum; i++)
    //    {
    //        GeneratePlayerPiece(i, 0);
    //    }
    //}

    //private void GeneratePlayerPiece(int x, int y)
    //{
    //    GameObject go = Instantiate(playerPiecePrefab) as GameObject;
    //    go.transform.SetParent(transform);
    //    PlayerPieces pp = go.GetComponent<PlayerPieces>();
    //    playersNum.Add(go);
    //    // pp.transform.position = (Vector3.right * x)+ (Vector3.forward * y)+ (Vector3.up * 1.5f);      
    //}
}

    //needed for List
[System.Serializable]
public class TurnClass
{
    public GameObject playerGameObject;
    public bool isTurn = false;
    public bool wasTurnPrev = false;
}
