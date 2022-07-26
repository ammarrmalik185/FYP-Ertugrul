using System.Collections.Generic;
using Managers.Scripts;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.Controllers{
    public class Mission{
        public readonly List<IObjective> objectives = new List<IObjective>();
        public bool isCompleted;

        public MissionData missionData{ get; private set; }

        private bool _tracked;
        public bool Tracked{
            set{
                _tracked = value;
                currentObjective.Tracked = value;
            }
        }
        
        private IObjective currentObjective;
        private int currentObjectiveNo;
        public int missionId;
        private int questId;

        public Mission(int questId, int missionId, int currentObjectiveNo = 0){
            this.currentObjectiveNo = currentObjectiveNo;
            this.missionId = missionId;
            this.questId = questId;
        }

        public void Start(){
            getCurrentObjective().doInitialSetup();
            currentObjective = getCurrentObjective();
        }
        
        public void End(){
            getCurrentObjective().doExitCondition();
            currentObjective = null;
        }
        
        public void addObjective(IObjective objective){
            objectives.Add(objective);
        }
        
        public void setData(MissionData data){
            missionData = data;
        }

        public IObjective getCurrentObjective(){
            return objectives[currentObjectiveNo];
        }
        
        public void Update(){
            getCurrentObjective().Update();

            if (!getCurrentObjective().isCompleted) return;
            
            if (currentObjectiveNo >= objectives.Count - 1)
                isCompleted = true;
            else{
                GameDirector.getInstance().questManager.ObjectiveUpdated.Invoke(questId, missionId, currentObjectiveNo, true);
                setCurrentObjective(currentObjectiveNo + 1);
            }
        }
        
        public void setCurrentObjective(int number){
            currentObjective.doExitCondition();
            currentObjective = objectives[number];
            currentObjective.doInitialSetup();
            currentObjective.Tracked = _tracked;
            currentObjectiveNo = number;
        }
    }
}