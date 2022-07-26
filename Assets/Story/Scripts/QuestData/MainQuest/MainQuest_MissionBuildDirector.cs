using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.QuestData.MainQuest.Objectives;

namespace Story.Scripts.QuestData.MainQuest{
    public class MainQuest_MissionBuildDirector : IMissionBuildDirector{
        private readonly MissionBuilder builder = new MissionBuilder();

        public Mission GetMission(int number){
            return number switch{
                1=>GetMission1(),
                2=>GetMission2(),
                3=>GetMission3(),
                4=>GetMission4(),
                _=>null
            };
        }
        public Mission GetMission1(){
            return builder
                .initMission((int)Quests.MainQuest, 0)
                .setData(new MissionData{text = "Getting Started"})
                .AddObjective(new InteractWithNPCObjective1())
                .AddObjective(new GoToArea1())
                .AddObjective(new DefeatTargets1())
                .AddObjective(new InteractWithNPCObjective2())
                .Build();
        }

        public Mission GetMission2(){
            return builder
                .initMission((int)Quests.MainQuest, 1)
                .setData(new MissionData{text = "The Castle Of Knights"})
                .AddObjective(new GoToCastle())
                .Build();
        }
        
        public Mission GetMission3(){
            return builder
                .initMission((int)Quests.MainQuest, 2)
                .setData(new MissionData{text = "Mission 3?"})
                .AddObjective(new GoToArea1())
                .Build();
        }
        
        public Mission GetMission4(){
            return builder
                .initMission((int)Quests.MainQuest, 3)
                .setData(new MissionData{text = "Mission 4?"})
                .AddObjective(new GoToArea1())
                .Build();
        }
    }
}