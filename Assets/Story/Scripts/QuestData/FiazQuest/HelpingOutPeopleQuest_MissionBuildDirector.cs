using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.QuestData.FiazQuest.Objectives;
using Story.Scripts.QuestData.MainQuest.Objectives;

namespace Story.Scripts.QuestData.FiazQuest{
    public class HelpingOutPeopleQuest_MissionBuildDirector : IMissionBuildDirector{
        private readonly MissionBuilder builder = new MissionBuilder();

        public Mission GetMission(int number){
            return number switch{
                1=>GetMission1(),
                2=>GetMission2(),
                _=>null
            };
        }

        private Mission GetMission1(){
            return builder
                .initMission((int)Quests.HelpingPeople, 0)
                .setData(new MissionData{text = "Fiaz and his Onions"})
                .AddObjective(new InteractWithFiazObjective1())
                .AddObjective(new WatchOnions())
                .AddObjective(new InteractWithFiazObjective2())
                .Build();
        }

        private Mission GetMission2(){
            return builder
                .initMission((int)Quests.HelpingPeople, 1)
                .setData(new MissionData{text = "Waleed is Hungry"})
                .AddObjective(new InteractWithWaleedObjective1())
                .AddObjective(new GetFood())
                .AddObjective(new InteractWithWaleedObjective2())
                .Build();
        }
    }
}