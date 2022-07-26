using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Assets.Scripts{
    public class ShowSteamData : MonoBehaviour{
    
        [SerializeField] private RawImage userProfilePicture;
        [SerializeField] private TextMeshProUGUI userName;

        [SerializeField] private GameObject singleSteamFriendViewPrefab;
        [SerializeField] private GameObject steamFriendsView;

        public GameObject joinGameMenu;
        public TMP_InputField joinGameIpEnter;
        public GameObject loadingScreen;
        public GameObject errorScreen;
    
        public static ShowSteamData Instance;

        private void Awake(){
            Instance = this;
        }

        void Start()
        {
            if (SteamManager.Initialized){
                userName.text = SteamFriends.GetPersonaName();
                userProfilePicture.texture = GetSteamImageAsTexture2D(SteamFriends.GetMediumFriendAvatar(SteamUser.GetSteamID()));
                loadFriends();
            }
            else{
                Debug.Log("Steam Not Initialized");
            }
        }

        private void FixedUpdate(){
            // loadFriends();
        }

        public void loadFriends(){
            var friends = new List<CSteamID>();
            for (var i = 0; i < SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate); i++){
                friends.Add(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate));
            }

            friends.Sort((id, steamID) => {
                var friend1State = SteamFriends.GetFriendPersonaState(id);
                var friend2State = SteamFriends.GetFriendPersonaState(steamID);
                
                if (friend1State != friend2State) return (int)friend1State - (int)friend2State;

                var friend1GameStatus = SteamFriends.GetFriendGamePlayed(id, out var friend1Game);
                var friend2GameStatus = SteamFriends.GetFriendGamePlayed(steamID, out var friend2Game);

                if (!(friend1GameStatus & friend2GameStatus)){
                    if (friend1GameStatus & !friend2GameStatus) return 1;
                    if (friend2GameStatus) return -1;
                    return 0;
                }
                
                if (friend1Game.m_gameID.AppID().m_AppId == 480) return 1;
                if (friend2Game.m_gameID.AppID().m_AppId  == 480) return -1;
                
                return (int)friend1Game.m_gameID.AppID().m_AppId - (int)friend2Game.m_gameID.AppID().m_AppId;
            });
            
            friends.Reverse();

            var t = steamFriendsView.transform;
            for(int i = 0; i < t.childCount; ++i){
                Destroy(t.GetChild(i).gameObject);
            }
        
            foreach (var cSteamID in friends){
                var go = Instantiate(singleSteamFriendViewPrefab, steamFriendsView.transform);
                go.GetComponent<SingleFriendContainer>().UserId = cSteamID;
            }
        }
    
        public static Texture2D GetSteamImageAsTexture2D(int iImage) {
            Texture2D ret = null;
            var bIsValid = SteamUtils.GetImageSize(iImage, out var ImageWidth, out var ImageHeight);

            if (!bIsValid) return null;
            var Image = new byte[ImageWidth * ImageHeight * 4];

            bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
            if (!bIsValid) return null;
            ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
            ret.LoadRawTextureData(Image);
            ret.Apply();

            return ret;
        }
    
        public void quitGame(){
            Application.Quit();
        }
    }
}
