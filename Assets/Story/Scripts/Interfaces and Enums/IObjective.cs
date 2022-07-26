using Story.Scripts.Controllers;

namespace Story.Scripts.Interfaces_and_Enums{
    public interface IObjective{
        ObjectiveData objectiveData{ get; }

        bool Tracked{ set; }
        void doInitialSetup();
        void Update();
        bool isCompleted{ get; set; }
        void doExitCondition();
    }
}