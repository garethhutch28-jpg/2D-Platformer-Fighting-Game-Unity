using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreenChanger : MonoBehaviour
{
    public GameObject stage1;
    public GameObject BruteStage;
    public GameObject AssasinStage;
    public GameObject AllRounderStage;

    private bool p1Brute = false; //Tracks if Brute is selected
    private bool p1Assasin = false; //Tracks if Assasin is selected
    private bool p1AllRounder = false; //Tracks if AllRounder is selected

    public void Start()
    {
        BruteStage.SetActive(false); //Disable Brute stage at start
        AssasinStage.SetActive(false); //Disable Assasin stage at start
        AllRounderStage.SetActive(false); //Disable AllRounder stage at start
    }

    public void Update()
    {
        if (p1Brute)
        {
            BruteStage.SetActive(true); //Activate Brute stage if selected
        }
        else if (p1Assasin)
        {
            AssasinStage.SetActive(true); //Activate Assasin stage if selected
        }
        else if (p1AllRounder)
        {
            AllRounderStage.SetActive(true); //Activate AllRounder stage if selected
        }
    }

    public void changeStage()
    {
        if (stage1 != null && stage1.activeSelf)
        {
            stage1.SetActive(false); //Disable stage1 if active
        }
    }

    public void BruteSelected()
    {
        p1Brute = true;
        p1Assasin = false;
        p1AllRounder = false;
        //Brute option selected
    }

    public void AssasinSelected()
    {
        p1Brute = false;
        p1Assasin = true;
        p1AllRounder = false;
        //Assasin option selected
    }

    public void AllRounderSelected()
    {
        p1Brute = false;
        p1Assasin = false;
        p1AllRounder = true;
        //AllRounder option selected
    }

}
