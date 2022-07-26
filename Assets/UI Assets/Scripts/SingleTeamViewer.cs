using System;
using System.Linq;
using Managers.Scripts;
using Multiplayer_Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Assets.Scripts{
    public class SingleTeamViewer : MonoBehaviour{
        [SerializeField] private Image headerImage;
        [SerializeField] private Image memberImage;
        [SerializeField] private GameObject userPrefab;
        [SerializeField] private GameObject userViewer;
        [SerializeField] private GameObject joinTeamButton;
        [SerializeField] private GameObject leaveTeamButton;
        [SerializeField] private TextMeshProUGUI teamHeaderText;

        [SerializeField] private Color myTeamColor;
        [SerializeField] private Color enemyTeamColor;
        [SerializeField] [Range(0, 100)] private int memberOpacity;
    
        private int _teamId;
        public int TeamId{
            get => _teamId;
            set{
                _teamId = value;
                UpdateUI();
            }
        }

        public int TeamNumber{
            set => teamHeaderText.text = "Team " + value + " id:" + TeamId;
        }

        private void UpdateUI(){
            if (GameDirector.getInstance().playerOnlineInstance.teamId == TeamId){
                joinTeamButton.SetActive(false);
                leaveTeamButton.SetActive(true);
                headerImage.color = myTeamColor;
                memberImage.color = new Color(myTeamColor.r, myTeamColor.g, myTeamColor.b, memberOpacity);
            }
            else{
                joinTeamButton.SetActive(true);
                leaveTeamButton.SetActive(false);
                headerImage.color = enemyTeamColor;
                memberImage.color = new Color(enemyTeamColor.r, enemyTeamColor.g, enemyTeamColor.b, memberOpacity);
            }
            foreach (var user in TeamManager.Instance.playerTeamMapping.Where(kv => kv.Value == TeamId)){
                var go = Instantiate(userPrefab, userViewer.transform, true);
                go.GetComponent<SingleTeamUserViewer>().UserId = user.Key;
            }
        }
        
        
        public void JoinTeamPressed(){
            GameDirector.getInstance().playerOnlineInstance.JoinTeamRequest(TeamId);
        }

        public void LeaveTeamPressed(){
            GameDirector.getInstance().playerOnlineInstance.RegisterNewTeam();
        }
    
    }
}
