using System;
using Cinemachine;
using Player_Assets.Networked_Player.Scripts;
using Player_Assets.Scripts;
using UnityEngine;

namespace Managers.Scripts{
    public class GameDirector : MonoBehaviour{
        internal GameManager gameManager;
        internal InputManager inputManager;
        internal MapManager mapManager;
        internal UIManager uiManager;
        internal QuestManager questManager;
        internal SettingsManager settingsManager;
        internal GameSaveManager gameSaveManager;

        [HideInInspector] public GameObject player;
        [HideInInspector] public GameObject thirdPersonCamera;
        [HideInInspector] public Camera thirdPersonCameraRoot;

        [HideInInspector] public PlayerOnlineInstance playerOnlineInstance;
        
        public CinemachineFreeLook thirdPersonCameraController;
        public GameObject miniMapCamera;
        public GameObject theSun;

        private static GameDirector _instance;

        private GameDirector(){ }

        private void Awake(){
            _instance = this;
        }
        
        private void Start(){
            settingsManager = GetComponent<SettingsManager>();
            gameManager = GetComponent<GameManager>();
            inputManager = GetComponent<InputManager>();
            mapManager = GetComponent<MapManager>();
            uiManager = GetComponent<UIManager>();
            questManager = GetComponent<QuestManager>();
            gameSaveManager = GetComponent<GameSaveManager>();
        }

        private void Update(){
            uiManager.enableCursor();
        }

        public static GameDirector getInstance(){
            return _instance;
        }
    }
}