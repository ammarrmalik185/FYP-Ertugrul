using System;
using System.Collections;
using Managers.Scripts;
using Mirror;
using Steamworks;
using TMPro;
using UI_Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer_Assets.Scripts{
    public class CustomNetworkManager : NetworkManager{
        [SerializeField] private TMP_InputField networkAddressField;

        public static CustomNetworkManager Instance;

        public CustomNetworkManager(){
            Instance = this;
        }
        
        public void StartGame(){
            StartHost();
            StartCoroutine(TimeoutCoroutine());
        }
        
        public void JoinGame(){
            networkAddress = string.IsNullOrWhiteSpace(networkAddressField.text) ? "localhost" : networkAddressField.text;
            StartClient();
            StartCoroutine(TimeoutCoroutine());
        }

        private IEnumerator TimeoutCoroutine(){
            yield return new WaitForSeconds(10);
            if (ShowSteamData.Instance == null) yield break;
            if (ShowSteamData.Instance.loadingScreen != null)
                ShowSteamData.Instance.loadingScreen.SetActive(false);
        }

        public void LeaveGame(){
            SceneManager.LoadScene("Main UI Scene", LoadSceneMode.Single);
            if (NetworkServer.active && NetworkClient.isConnected){
                StopHost();
            }
            else if (NetworkClient.isConnected){
                StopClient();
            }
            else if (NetworkServer.active){
                StopServer();
            }
        }
        
        public override void OnClientDisconnect(){
            base.OnClientDisconnect();
            Debug.Log("Error Detected");
            ShowSteamData.Instance.loadingScreen.SetActive(false);
        }
    }
}
