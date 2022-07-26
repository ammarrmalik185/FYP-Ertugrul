using System;
using System.Linq;
using Managers.Scripts;
using Multiplayer_Assets.Scripts;
using UnityEngine;

namespace UI_Assets.Scripts{
    public class ShowTeams: MonoBehaviour{
        [SerializeField] private GameObject teamPrefab;
        [SerializeField] private GameObject teamsContainer;

        [SerializeField] private GameObject teamUI;

        public static ShowTeams Instance;

        private void Awake(){
            Instance = this;
            teamsChanged += () => {
                if (teamUI.activeInHierarchy){
                    UpdateUI();
                }
            };
        }

        public Action teamsChanged;
        
        public void showTeams(){
            if (teamUI.activeInHierarchy) return;
            
            teamUI.SetActive(true);
            UpdateUI();
        }
        
        
        private void Update(){
            if(GameDirector.getInstance().inputManager.getKey(playerControlOptions.showTeams)){
                showTeams();
            } else {
                hideTeams();
            }
        }
        
        public void hideTeams(){
            if (!teamUI.activeInHierarchy) return;
            
            teamUI.SetActive(false);
        }
        
        private void UpdateUI(){
            foreach (var stvGo in teamsContainer.transform.GetComponentsInChildren<SingleTeamViewer>().Select(stv => stv.gameObject)){
                Destroy(stvGo);
            }

            var num = 1;
            foreach (var teamId in TeamManager.Instance.playerTeamMapping.Values.Distinct()){
                var go = Instantiate(teamPrefab, teamsContainer.transform, true);
                var stv = go.GetComponent<SingleTeamViewer>();
                stv.TeamId = teamId;
                stv.TeamNumber = num++;
            }
        }

    }
    
    
}