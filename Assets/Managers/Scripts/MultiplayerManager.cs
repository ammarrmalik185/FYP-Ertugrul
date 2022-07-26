using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Steamworks;
using UnityEngine;

namespace Managers.Scripts{
    public class MultiplayerManager : NetworkBehaviour{
        public readonly SyncDictionary<int, CSteamID> connectedUsers = new SyncDictionary<int, CSteamID>();

        public static MultiplayerManager Instance;

        private void Awake(){
            Instance = this;
        }
        
        public void AddPlayer(int connId, string steamID){
            Debug.Log(connectedUsers);
            connectedUsers.Add(connId, new CSteamID(ulong.Parse(steamID)));
        }
        

        [Command]
        public void RemovePlayer(){
            connectedUsers.Remove(connectionToClient.connectionId);
        }
        
        public CSteamID GetPlayerId(int connId){
            return connectedUsers[connId];
        }
    }
}
