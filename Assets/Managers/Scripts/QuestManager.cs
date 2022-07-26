using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Story.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.QuestData;
using UnityEngine;

namespace Managers.Scripts{
    public class QuestManager : MonoBehaviour{

        private GameDirector director;
        private Quest currentTrackedQuest;
        private Quests currentQuestId;
        private QuestBuildDirector questBuildDirector;
        public readonly Dictionary<Quests, Quest> quests = new Dictionary<Quests, Quest>();
        
        public Quest getCurrentTrackedQuest(){
            return currentTrackedQuest;
        }
        public Action<int, int, int, bool> ObjectiveUpdated;
        
        public void setCurrentTrackedQuest(Quests quest){
            currentQuestId = quest;
            currentTrackedQuest = quests[quest];
            foreach (var keyValuePair in quests){
                keyValuePair.Value.Tracked = currentQuestId == keyValuePair.Key;
            }
        }

        public Quest getCurrenTrackedQuest(){
            return currentTrackedQuest;
        }

        public void UpdateQuestFromTeam(int questId, int missionId, int objectiveId, bool status){
            quests[(Quests)questId].missions[missionId].objectives[objectiveId].isCompleted = status;
        }
        
        public void updateQuestGUI(){
            if (currentTrackedQuest.isCompleted){
                director.uiManager.setQuest(currentTrackedQuest.questData.text);
                director.uiManager.setMission("All Missions Completed");
                director.uiManager.setObjective("All Objectives Completed");
            }
            else{
                director.uiManager.setQuest(currentTrackedQuest.questData.text);
                director.uiManager.setMission(currentTrackedQuest.getCurrentMission().missionData.text);
                director.uiManager.setObjective(currentTrackedQuest.getCurrentMission().getCurrentObjective()
                    .objectiveData.text);
            }
        }
        
        // Start is called before the first frame update
        void Start(){
            director = GetComponent<GameDirector>();
            questBuildDirector = new QuestBuildDirector();
            
            ObjectiveUpdated += (questId, missionId, objectiveId, status) => {
                Debug.Log("Objective updated");
                director.playerOnlineInstance.QuestUpdateRequest(questId, missionId, objectiveId, status);
            };

            Quest quest = questBuildDirector.GetQuest(Quests.MainQuest);
            Quest sideQuest1 = questBuildDirector.GetQuest(Quests.HelpingPeople);
            Quest sideQuest2 = questBuildDirector.GetQuest(Quests.LookingAround);
            // Quest sideQuest3 = questBuildDirector.GetQuest(Quests.SideQuest1);
            
            quest.Start();
            sideQuest1.Start();
            sideQuest2.Start();
            // sideQuest3.Start();
            
            quests.Add(Quests.MainQuest, quest);
            quests.Add(Quests.HelpingPeople, sideQuest1);
            quests.Add(Quests.LookingAround, sideQuest2);
            // quests.Add(Quests.SideQuest1, sideQuest3);

            setCurrentTrackedQuest(Quests.MainQuest);
        }

        // Update is called once per frame
        private void Update(){
            if(GameDirector.getInstance().inputManager.getKeyDown(playerControlOptions.nextQuest)){
                setCurrentTrackedQuest((Quests)(((int)currentQuestId + 1) % quests.Count));
            }
            updateQuestGUI();
            foreach (var quest in quests){
                if(!quest.Value.isCompleted)
                    quest.Value.Update(); 
            }
        }
    }
}
