using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //Player V Player selected, Load CharacterSelectScreen
    public void CharacterSelectScreen()
    {
        SceneManager.LoadSceneAsync(2);
    }
    //player 2 character select screen
    public void CharacterSelectScreen2()
    {
        SceneManager.LoadSceneAsync(3);
    }
    //load map select screen
    public void MapScreen()
    {
        SceneManager.LoadSceneAsync(4);
    }
  

}
