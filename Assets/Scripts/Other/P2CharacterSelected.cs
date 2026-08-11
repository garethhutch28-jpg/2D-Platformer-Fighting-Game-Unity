using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2CharacterSelected : MonoBehaviour
{
    
    public string Player2;
    public P1CharacterSelected P1CharacterSelected;
    public bool Player2Brute = false;
    public bool Player2Assasin = false;
    public bool Player2AllRounder = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void BruteSelectedP2()
    {
        Debug.Log("Player 2 is Brute");
        Player1Character();
        Player2Brute = true;
    }

    public void AssasinSelectedP2()
    {
        Debug.Log("Player 2 is Assasin");
        Player1Character();
        Player2 = "Assasin";
        Player2Assasin = true;
    }

    public void AllrounderSelectedP2()
    {
        Debug.Log("Player 2 is Allrounder");
        Player1Character();
        Player2 = "All Rounder";
        Player2AllRounder = true;
    }

    public void Player1Character()
    {
        if(P1CharacterSelected.Player1Brute == true)
        {
            Debug.Log("Player 1 is brute");
        }
        else if (P1CharacterSelected.Player1Assasin == true)
        {
            Debug.Log("Player 1 is Assasin");
        }
        else if (P1CharacterSelected.Player1AllRounder == true)
        {
            Debug.Log("Player 1 is All Rounder");
        }
    }
    
}
