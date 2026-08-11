using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    //checks
    private bool stage2Loaded = false;
    private bool mapSelected = false;
    //refernces to canvas game objects
    public GameObject canvas1;
    public GameObject canvas2;
    //players options
    public int P1MapSelected = 0;
    public int P2MapSelected = 0;
    //final map choice
    public int finalMap = 0;


    public void Start()
    {
        DontDestroyOnLoad(this);
        //diable the player 2 option and enable the player one option
        canvas2.SetActive(false);
        canvas1.SetActive(true);
    }

    //functions for buttons
    public void map1selectedP1()
    {
        P1MapSelected = 1;
    }
    public void map2selectedP1()
    {
        P1MapSelected = 2;
    }
    public void map3selectedP1()
    {
        P1MapSelected= 3;
    }
    public void map1selectedP2()
    {
        P2MapSelected = 1;
    }
    public void map2selectedP2()
    {
        P2MapSelected = 2;
    }
    public void map3selectedP2()
    {
        P2MapSelected = 3;
    }

    private void Update()
    {
        //check if a map has been selected in stage one and if stage 2 hasnt already loaded
        if (P1MapSelected > 0 && stage2Loaded == false)
        {
            //load enable canvas 2 and disable canvas one
            stage2Loaded = true;
            canvas1.SetActive(false);
            canvas2.SetActive(true);
        }
        if (!mapSelected)
        {
            //run map selector function
            mapSelector();
        }
        MapLoader();
    }
    public void mapSelector()
    {
        //if both maps have been selected
        if(P1MapSelected > 0 && P2MapSelected > 0)
        {
            //if map one is equal to map two
            if (P1MapSelected == P2MapSelected)
            {
                //debug.log for validation
                Debug.Log("load map " + P1MapSelected);
                //set final map to either of the options as they are the same option
                finalMap = P1MapSelected;
            }
            //if they are not the same option
            else if (P1MapSelected != P2MapSelected)
            {
                //select a random number between the two integers and set finalmap equal to that
                finalMap = Random.Range(0, 2) == 0 ? P1MapSelected : P2MapSelected;
                Debug.Log("random map is map" + finalMap);
            }
            mapSelected = true;
        }
       
    }
    //load the next scene once final map has been chosen
    private void MapLoader()
    {
        if (finalMap == 1)
        {
            SceneManager.LoadSceneAsync(5);
        }
        if (finalMap == 2)
        {
            SceneManager.LoadSceneAsync(6);
        }
        if (finalMap == 3)
        {
            SceneManager.LoadSceneAsync(7);
        }
    }

}
