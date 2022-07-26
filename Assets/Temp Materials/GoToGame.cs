using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour{
    [SerializeField] private NetworkManager manager;
    
    public void goToGame(){
        manager.StartHost();
        //SceneManager.LoadScene("Intro Map", LoadSceneMode.Single);
    }
}
