using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.QuestData.MapMaker_Quest.Objectives;

namespace Story.Scripts.QuestData.MapMaker_Quest{
    public class MapMakerQuest_MissionBuildDirector : IMissionBuildDirector{
        private readonly MissionBuilder builder = new MissionBuilder();

        public Mission GetMission(int number){
            return number switch{
                1=>GetMission1(),
                _=>null
            };
        }
        private Mission GetMission1(){
            return builder
                .initMission((int)Quests.LookingAround, 0)
                .setData(new MissionData{text = "The Mapmaker"})
                .AddObjective(new InteractWithNPCSideObjective1())
                .AddObjective(new GoToMapMakerArea1())
                .AddObjective(new GoToMapMakerArea2())
                .AddObjective(new GoToMapMakerArea3())
                .AddObjective(new GoToMapMakerArea4())
                .AddObjective(new GoToMapMakerArea5())
                .AddObjective(new InteractWithNPCSideObjective2())
                .Build();
        }
    }
}