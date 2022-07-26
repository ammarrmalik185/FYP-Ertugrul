namespace Story.Scripts.Controllers{
    public class QuestBuilder{
        private Quest _quest = new Quest();
        
        public QuestBuilder AddMission(Mission mission){
            _quest.addMission(mission);
            return this;
        }
        
        public QuestBuilder setData(QuestData data){
            _quest.setData(data);
            return this;
        }

        public Quest Build(){
            var returnQuest = _quest;

            _quest = new Quest();
            return returnQuest;
        }
    }
}