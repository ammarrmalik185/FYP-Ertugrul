using System;
using Managers.Scripts;
using Multiplayer_Assets.Scripts;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Assets.Scripts{
    public class SingleFriendContainer : MonoBehaviour{

        [SerializeField] private TextMeshProUGUI nameView;

        [SerializeField] private TextMeshProUGUI statusView;

        [SerializeField] private RawImage avatarView;

        [SerializeField] private Color onlineColor;
        [SerializeField] private Color offlineColor;
        [SerializeField] private Color awayColor;
        [SerializeField] private Color inGameColor;

        private bool playingGame;
        
        private CSteamID userId;
        public CSteamID UserId{
            get => userId;
            set{
                userId = value;
                if (userId != null){
                    UpdateFromSteam();
                }
            }
        }

        public void OnJoinPressed(){
            ShowSteamData.Instance.joinGameIpEnter.text = userId.ToString();
            ShowSteamData.Instance.joinGameMenu.SetActive(true);
        }
        
        private void UpdateFromSteam(){
            nameView.text = SteamFriends.GetFriendPersonaName(userId);
            avatarView.texture = ShowSteamData.GetSteamImageAsTexture2D(SteamFriends.GetMediumFriendAvatar(userId));
            var userStatus = SteamFriends.GetFriendPersonaState(userId);
            if (userStatus == EPersonaState.k_EPersonaStateOnline){
                statusView.text = "Online";
                statusView.color = onlineColor;
                if (!SteamFriends.GetFriendGamePlayed(userId, out var gameInfo)) return;

                var appId = gameInfo.m_gameID.AppID().m_AppId;
                if (appId == 480){
                    statusView.text = "In Game";
                    statusView.color = inGameColor;
                }else if (appId != 0){
                    statusView.text = "Playing Other Game";
                    statusView.color = onlineColor;
                }
            }
            else{
                statusView.text = userStatus switch{
                    EPersonaState.k_EPersonaStateOffline => "Offline",
                    EPersonaState.k_EPersonaStateBusy => "Busy",
                    EPersonaState.k_EPersonaStateAway => "Away",
                    EPersonaState.k_EPersonaStateSnooze => "Snooze",
                    EPersonaState.k_EPersonaStateLookingToTrade => "Looking to Trade",
                    EPersonaState.k_EPersonaStateLookingToPlay => "Looking To Play",
                    EPersonaState.k_EPersonaStateInvisible => "Invisible",
                    EPersonaState.k_EPersonaStateMax => "Max",
                    _ => throw new ArgumentOutOfRangeException()
                };
                if (userStatus == EPersonaState.k_EPersonaStateAway || userStatus == EPersonaState.k_EPersonaStateBusy){
                    statusView.color = awayColor;
                }
                else{
                    statusView.color = offlineColor;
                }
            }
            // if (appStatus != -1){
                
            // }else{
                // statusView.text = "Not found";
            // }
        }
    }
}
