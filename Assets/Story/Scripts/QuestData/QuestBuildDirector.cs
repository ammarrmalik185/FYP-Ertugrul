using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.QuestData.FiazQuest;
using Story.Scripts.QuestData.MainQuest;
using Story.Scripts.QuestData.MapMaker_Quest;

namespace Story.Scripts.QuestData{
    public class QuestBuildDirector{
        private readonly QuestBuilder builder = new QuestBuilder();

        private readonly IMissionBuildDirector mainQuest_missionBuildDirector = new MainQuest_MissionBuildDirector();
        private readonly IMissionBuildDirector helpingOutPeopleQuest_missionBuildDirector = new HelpingOutPeopleQuest_MissionBuildDirector();
        private readonly IMissionBuildDirector mapMakerQuest_missionBuildDirector = new MapMakerQuest_MissionBuildDirector();

        public Quest GetQuest(Quests quest){
            return quest switch{
                Quests.MainQuest => GetMainQuest(),
                Quests.HelpingPeople => GetSideQuest1(),
                Quests.LookingAround => GetSideQuest2(),
                // Quests.ElonMuskDestroyCoal => GetSideQuest1(),
                _ => null
            };
        }

        private Quest GetMainQuest(){
            return builder
                .setData(new Controllers.QuestData{text = "Main Quest"})
                .AddMission(mainQuest_missionBuildDirector.GetMission(1))
                .AddMission(mainQuest_missionBuildDirector.GetMission(2))
                .AddMission(mainQuest_missionBuildDirector.GetMission(3))
                .AddMission(mainQuest_missionBuildDirector.GetMission(4))
                .Build();
        }

        private Quest GetSideQuest1(){
            return builder
                .setData(new Controllers.QuestData{text = "Helping out the people"})
                .AddMission(helpingOutPeopleQuest_missionBuildDirector.GetMission(1))
                .AddMission(helpingOutPeopleQuest_missionBuildDirector.GetMission(2))
                .Build();
        }

        private Quest GetSideQuest2(){
            return builder
                .setData(new Controllers.QuestData{text = "The Map Maker"})
                .AddMission(mapMakerQuest_missionBuildDirector.GetMission(1))
                .Build();
        }
        
    }
}