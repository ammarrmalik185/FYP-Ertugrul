using System;
using Managers.Helper_Scripts;
using Player_Assets.Scripts;
using SimpleJSON;
using UnityEngine;

namespace Managers.Scripts{
    public class GameSaveManager : MonoBehaviour{
        private GameDirector director;

        public static Action<SaveSlots> OnSlotChange;

        private static SaveSlots currentSaveSlot;
        public static SaveSlots CurrentSaveSlot{
            get => currentSaveSlot;
            set{
                currentSaveSlot = value;
                OnSlotChange.Invoke(currentSaveSlot);
            }
        }

        private void Start(){
            director = GameDirector.getInstance();
        }

        public void saveGameData(){
            var saveData = new SteamCloudPrefs{
                lastSaveTime = DateTime.Now.ToString("g"),
                playerData = GetCurrentPlayerData().ToString(),
                questData = GetQuestData().ToString(),
                title = "Saved Game"
            };
            Debug.Log(saveData.questData);
            SaveLoadFile.Save(saveData, CurrentSaveSlot);
        }
        
        public void loadGameData(){
            var saveData = SaveLoadFile.Load(CurrentSaveSlot);
            
            if (saveData == null){
                Debug.Log("Save Data is null");
                saveData = new SteamCloudPrefs{
                    playerData = GetCurrentPlayerData(),
                    questData =  GetQuestData()
                };
                SaveLoadFile.Save(saveData, CurrentSaveSlot);
            }
            SetQuestData(JSON.Parse(saveData.questData));
            SetCurrentPlayerData(JSON.Parse(saveData.playerData));
        }
        
        private void SetCurrentPlayerData(JSONNode playerData){
            if (playerData == null) return; 

            var player = director.player.GetComponent<Player>();

            player.setLevel(playerData["currentLevel"].AsInt);
            player.setCurrentHealth(playerData["currentHealth"].AsFloat);
            player.setCurrentXp(playerData["currentXP"].AsFloat);
            player.skillManager.freeSkillPoints = playerData["currentSkillPoints"].AsInt;

            var transform1 = player.transform;
            transform1.position = new Vector3(
                playerData["currentTransform"]["position"]["x"].AsFloat,
                playerData["currentTransform"]["position"]["y"].AsFloat,
                playerData["currentTransform"]["position"]["z"].AsFloat
                );
            
            transform1.rotation = new Quaternion(
                playerData["currentTransform"]["rotation"]["x"].AsFloat,
                playerData["currentTransform"]["rotation"]["y"].AsFloat,
                playerData["currentTransform"]["rotation"]["z"].AsFloat,
                playerData["currentTransform"]["rotation"]["w"].AsFloat
            );

        }
        
        private JSONClass GetCurrentPlayerData(){
            var player = director.player.GetComponent<Player>();
            var transform1 = player.transform;
            var position = transform1.position;
            var rotation = transform1.rotation;
            
            var playerData = new JSONClass{
                ["currentHealth"] = new JSONData(player.controller.currentHealth),
                ["currentLevel"] = new JSONData(player.level),
                ["currentXP"] = new JSONData(player.currentXp),
                ["currentSkillPoints"] = new JSONData(player.skillManager.freeSkillPoints),
                ["currentTransform"] = new JSONClass{
                    ["position"] = new JSONClass{
                        ["x"] = new JSONData(position.x),
                        ["y"] = new JSONData(position.y),
                        ["z"] = new JSONData(position.z),
                    },
                    ["rotation"] = new JSONClass{
                        ["w"] = new JSONData(rotation.w),
                        ["x"] = new JSONData(rotation.x),
                        ["y"] = new JSONData(rotation.y),
                        ["z"] = new JSONData(rotation.z),
                    }
                }
            };
            return playerData;
        }

        private JSONClass GetQuestData(){
            var data = new JSONClass{
                ["quests"] = new JSONClass()
            };

            foreach (var questManagerQuest in director.questManager.quests){
                data["quests"][((int)questManagerQuest.Key).ToString()] = new JSONClass();
                foreach (var valueMission in questManagerQuest.Value.missions){
                    data["quests"][((int)questManagerQuest.Key).ToString()]
                        [valueMission.missionId.ToString()] = new JSONClass();
                    var id = 0;
                    foreach (var objective in valueMission.objectives){
                        data["quests"]
                            [((int)questManagerQuest.Key).ToString()]
                            [valueMission.missionId.ToString()][id.ToString()] = new JSONData(objective.isCompleted);
                        id++;
                    }
                }
            }
            
            return data;
        }

        private void SetQuestData(JSONNode data){
            if (data == null) return;
            foreach (var questManagerQuest in director.questManager.quests){
                foreach (var valueMission in questManagerQuest.Value.missions){
                    var id = 0;
                    foreach (var objective in valueMission.objectives){
                        objective.isCompleted = data["quests"]
                            [((int)questManagerQuest.Key).ToString()]
                            [valueMission.missionId.ToString()][id.ToString()].AsBool;
                        id++;
                    }
                }
            }
        }
        
    }
}