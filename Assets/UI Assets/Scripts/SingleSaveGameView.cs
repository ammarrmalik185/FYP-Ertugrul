using Managers.Helper_Scripts;
using Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Assets.Scripts{
    public class SingleSaveGameView : MonoBehaviour{
        [SerializeField] private SaveSlots saveGameSlotId;
        [SerializeField] private Image displayImage;
        [SerializeField] private Button button;

        [SerializeField] private TextMeshProUGUI saveGameTitle;
        [SerializeField] private TextMeshProUGUI saveGameLastSaveTime;

        private void Start(){
            GameSaveManager.OnSlotChange += newSlot => UpdateUI();
            button.onClick.AddListener(() => GameSaveManager.CurrentSaveSlot = saveGameSlotId);

            var saveGameData = SaveLoadFile.Load(saveGameSlotId);
            if (saveGameData == null){
                saveGameTitle.text = "+ Create New Save Game";
                saveGameLastSaveTime.text = "Last Save Time: Never"; 
            }
            else{
                saveGameTitle.text = saveGameData.title;
                saveGameLastSaveTime.text = saveGameData.lastSaveTime;
            }
            UpdateUI();
        }

        private void UpdateUI(){
            displayImage.color = GameSaveManager.CurrentSaveSlot == saveGameSlotId ? Color.green : new Color(132, 132, 132);
        }
    }
}
