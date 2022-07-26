using Story.Scripts.Interfaces_and_Enums;

namespace Story.Scripts.Controllers{
    public class MissionBuilder{
        private Mission _mission;

        public MissionBuilder initMission(int questId, int missionId){
            _mission = new Mission(questId, missionId);
            return this;
        }
        
        public MissionBuilder AddObjective(IObjective objective){
            _mission.addObjective(objective);
            return this;
        }
        
        public MissionBuilder setData(MissionData data){
            _mission.setData(data);
            return this;
        }

        public Mission Build(){
            var returnMission = _mission;
           
            return returnMission;
        }
    }
}