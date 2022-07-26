using System.Collections.Generic;
using Managers.Scripts;
using Mirror;
using UI_Assets.Scripts;
using UnityEngine;

namespace Multiplayer_Assets.Scripts{
    public class TeamManager : NetworkBehaviour{
        public readonly SyncDictionary<int, int> playerTeamMapping = new SyncDictionary<int, int>();

        [SerializeField] private List<GameObject> playerPrefabs = new List<GameObject>();

        public static TeamManager Instance;

        public TeamManager(){
            Instance = this;
        }
        
        public void RegisterSelf(int conId, int teamId){
            if (!playerTeamMapping.ContainsKey(conId))
                playerTeamMapping.Add(conId, teamId);
            else{
                playerTeamMapping[conId] = teamId;
            }

            UpdateTeamsOnClients();
        }

        public void JoinTeam(int conId, int teamId){
            Debug.Log("setting value :" + conId + ", " + teamId);
            if (playerTeamMapping.ContainsKey(conId))
                playerTeamMapping[conId] = teamId;

            UpdateTeamsOnClients();
        }
        
        public void LeaveTeam(int conId, int teamId){
            if (playerTeamMapping.ContainsKey(conId))
                playerTeamMapping.Remove(conId);

            UpdateTeamsOnClients();
        }
        
        public void DisplayTeams(){
            var str = "";
            foreach (var keyValuePair in playerTeamMapping){
                str += keyValuePair.Key + " - " + keyValuePair.Value + "\n";
            }
            Debug.Log(str);
        }

        [ClientRpc]
        public void UpdateTeamsOnClients(){
            ShowTeams.Instance.teamsChanged.Invoke();
        }
    }
}