using System.Collections.Generic;
using UnityEngine;

namespace Story.Scripts.Controllers{
    public class Quest{
        public readonly List<Mission> missions = new List<Mission>();

        public QuestData questData{ get; private set; }

        private bool _tracked;
        public bool Tracked{
            set{
                _tracked = value;
                currentMission.Tracked = value;
            }
        }
        
        private Mission currentMission;
        private int currentMissionNo;
        public bool isCompleted;

        public void Start(){
            setCurrentMission(0);
        }
        
        public void addMission(Mission mission){
            missions.Add(mission);
        }
        
        public void setData(QuestData data){
            questData = data;
        }

        public Mission getCurrentMission(){
            return missions[currentMissionNo];
        }
        
        public void Update(){
            getCurrentMission().Update();
            
            if (getCurrentMission().isCompleted)
                setCurrentMission(currentMissionNo + 1);
        }

        private void setCurrentMission(int number){
            currentMission?.End();
            if (number > missions.Count - 1)
                isCompleted = true;
            else{
                currentMissionNo = number;
                currentMission = missions[number];
                currentMission.Start();
                currentMission.Tracked = _tracked;
            }
            
        }
        
    }
}