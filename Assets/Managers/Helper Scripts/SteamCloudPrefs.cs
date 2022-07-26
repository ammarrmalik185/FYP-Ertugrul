using System;
using UnityEngine;
using SimpleJSON;
using Steamworks;

namespace Managers.Helper_Scripts{
    [Serializable]
    public class SteamCloudPrefs{
        public string title = "Beginning of the Journey";
        public string lastSaveTime = DateTime.Now.ToString("g");

        public string playerData;
        public string questData;
        public string mapData;
    }
}