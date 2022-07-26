using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers.Scripts;
using Mirror;
using Multiplayer_Assets.Scripts;
using Player_Assets.Scripts;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Player_Assets.Networked_Player.Scripts{
    public class PlayerOnlineInstance : NetworkBehaviour{
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject thirdPersonCamera;
        [SerializeField] private Camera thirdPersonCameraRoot;

        [SerializeField] private List<GameObject> objectsToSetActive;
        [SerializeField] private List<GameObject> scriptsToSetActive;
        [SerializeField] private List<SpriteRenderer> imagesToChangeColor;
        [SerializeField] private Color changeColor;

        [HideInInspector] public int teamId;

        // Start is called before the first frame update
        private void Start(){

            if (isLocalPlayer){
                GameDirector.getInstance().player = player;
                GameDirector.getInstance().thirdPersonCamera = thirdPersonCamera;
                GameDirector.getInstance().thirdPersonCameraRoot = thirdPersonCameraRoot;
                GameDirector.getInstance().playerOnlineInstance = this;

                foreach (var objectToSetActive in objectsToSetActive){
                    objectToSetActive.SetActive(isLocalPlayer);
                }

                foreach (var script in scriptsToSetActive.SelectMany(scriptToSetActive =>
                             scriptToSetActive.GetComponents<MonoBehaviour>())){
                    script.enabled = isLocalPlayer;
                }
                
                RegisterPlayer(SteamUser.GetSteamID().m_SteamID.ToString());
                RegisterNewTeam();
            }
            else{
                foreach (var imageToChangeColor in imagesToChangeColor){
                    imageToChangeColor.color = changeColor;
                }
            }
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.J)){
                Debug.Log("teams");
                TeamManager.Instance.DisplayTeams();
            }

            if (Input.GetKeyDown(KeyCode.K)){
                JoinTeam(TeamManager.Instance.playerTeamMapping.First().Value);
            }
        }

        [Command]
        private void RegisterPlayer(string steamId){
            MultiplayerManager.Instance.AddPlayer(connectionToClient.connectionId, steamId);
        }

        public void RegisterNewTeam(){
            teamId = Random.Range(1, int.MaxValue);
            RegisterTeam(teamId);
        }
        
        [Command]
        private void RegisterTeam(int newTeamId){
            TeamManager.Instance.RegisterSelf(connectionToClient.connectionId, newTeamId);
        }

        private void OnDisable(){
            LeaveTeam(teamId);
        }
        
        private void OnDestroy(){
            LeaveTeam(teamId);
        }

        public override void OnStopServer(){
            base.OnStopServer();
            LeaveTeam(teamId);
        }

        public override void OnStopClient(){
            base.OnStopClient();
            LeaveTeam(teamId);
        }

        public override void OnStopLocalPlayer(){
            base.OnStopLocalPlayer();
            LeaveTeam(teamId);
        }

        public void OnDeath(){
            StartCoroutine(OnDeathCoroutine());
        }

        private IEnumerator OnDeathCoroutine(){
            yield return new WaitForSeconds(5);
            GameDirector.getInstance().gameSaveManager.loadGameData();
        }
        

        public override void OnStopAuthority(){
            base.OnStopAuthority();
            LeaveTeam(teamId);
        }

        [Command]
        private void LeaveTeam(int currentTeamId){
            TeamManager.Instance.LeaveTeam(connectionToClient.connectionId, currentTeamId);
        }

        [Command]
        private void JoinTeam(int newTeamId){
            TeamManager.Instance.JoinTeam(connectionToClient.connectionId, newTeamId);
        }

        public void JoinTeamRequest(int newTeamId){
            JoinTeam(newTeamId);
            Debug.Log(teamId);
            teamId = newTeamId;
            Debug.Log(teamId);
        }

        public void QuestUpdateRequest(int questId, int missionId, int objectiveId, bool status){
            Debug.Log("Quest Update Request");
            QuestUpdate(questId, missionId, objectiveId, status);
        }
        
        [Command]
        public void QuestUpdate(int questId, int missionId, int objectiveId, bool status){
            Debug.Log("Quest Update Request Server");
            QuestUpdateClient(TeamManager.Instance.playerTeamMapping[connectionToClient.connectionId], questId, missionId, objectiveId, status);
        }
        
        [ClientRpc]
        public void QuestUpdateClient(int requestTeamId, int questId, int missionId, int objectiveId, bool status){
            Debug.Log("Quest Update Request Client");
            Debug.Log(requestTeamId);
            Debug.Log(teamId);
            
            if (requestTeamId == GameDirector.getInstance().playerOnlineInstance.teamId){
                Debug.Log("Quest Update Request Team Mached");
                GameDirector.getInstance().questManager.UpdateQuestFromTeam(questId, missionId, objectiveId, status);
            }
        }
    }
}
