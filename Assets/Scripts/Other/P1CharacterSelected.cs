using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class P1CharacterSelected : MonoBehaviour
{
    
    //public string Player1;
    public bool Player1Brute = false;
    public bool Player1Assasin = false;
    public bool Player1AllRounder = false;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void BruteSelectedP1()
    {
        Debug.Log("Player 1 is Brute");
        //Player1 = "Brute";
        Player1Brute = true;
    }

    public void AssasinSelectedP1()
    {
        Debug.Log("Player 1 is Assasin");
        //Player1 = "Assasin";
        Player1Assasin=true;
    }

    public void AllrounderSelectedP1()
    {
        Debug.Log("Player 1 is Allrounder");
        //Player1 = "All Rounder";
        Player1AllRounder=true;
    }

    
}
