using Managers.Scripts;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Assets.Scripts{
    public class SingleTeamUserViewer: MonoBehaviour{
        [SerializeField] private RawImage userImage;
        [SerializeField] private TextMeshProUGUI nameView;

        [SerializeField] private Color myColor;

        private int userId;

        public int UserId{
            get => userId;
            set{
                userId = value;
                UpdateUI();
            }
        }
        
        private void UpdateUI(){
            var steamID = MultiplayerManager.Instance.connectedUsers[UserId];
            nameView.text = SteamFriends.GetFriendPersonaName(steamID);
            userImage.texture = ShowSteamData.GetSteamImageAsTexture2D(SteamFriends.GetMediumFriendAvatar(steamID));
            if (steamID == SteamUser.GetSteamID()){
                nameView.color = myColor;
            }
        }
    }
    
    
}