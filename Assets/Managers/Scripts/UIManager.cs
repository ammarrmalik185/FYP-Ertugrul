using System;
using System.Collections.Generic;
using Inventory_Assets.Items.Interfaces_and_Enums;
using Inventory_Assets.Scripts;
using Player_Assets.Scripts;
using Skill_Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Scripts{
    public class UIManager : MonoBehaviour{
        public GameDirector director;
        
        public GameObject hudDisplay;
        public Image healthBar;
        public TextMeshProUGUI healthText;
        public Image expBar;
        public TextMeshProUGUI expText;
        public TextMeshProUGUI levelIndicator;
            
        public TextMeshProUGUI questTextView;
        public TextMeshProUGUI missionTextView;
        public TextMeshProUGUI objectiveTextView;
        
        public GameObject interactionPrompt;
        public GameObject pickupPrompt;
        public GameObject skillTooltip;
        
        public GameObject dialogDisplay;
        public TextMeshProUGUI dialogDisplayText;

        public GameObject pauseMenu;
        public GameObject skillsWindow;

        // public GameObject itemContainer;
        // public GameObject itemViewPrefab;
        // public Sprite defaultSprite;
        // public GameObject inventory;
        
        public TextMeshProUGUI chatTextView;
        public TMP_InputField chatInputField;
        public GameObject chatWindow;
        
        public List<EquippedSkillManager> equippedSkillUIManagers;
        
        private void Start(){
            director = GetComponent<GameDirector>();
        }

        private void Update(){
            if (director.inputManager.getKeyDown(playerControlOptions.pause)){
                if (director.gameManager.isInventoryOpen){
                    // TODO: Hide Inventory
                    // hideInventory();
                }
                else{
                    if (director.gameManager.isPauseMenuOpen){
                        hidePauseMenu();
                    }
                    else{
                        showPauseMenu();
                    }
                }
            }
            

            // if (director.inputManager.getKeyDown(playerControlOptions.openInventory)){
            //     if (director.gameManager.isPauseMenuOpen) return;
            //     if (!director.gameManager.isInventoryOpen)
            //         showInventory();
            //     else
            //         hideInventory();
            // }
            
            if (director.inputManager.getKeyDown(playerControlOptions.openSkills)){
                if (director.gameManager.isPauseMenuOpen) return;
                if (!director.gameManager.isSkillMenuOpen)
                    showSkills();
                else
                    hideSkills();
            }
        }
        

        public void setHp(float current, float max){
            healthBar.fillAmount = current/max;
            healthText.text = Math.Round(current) + "/" + Math.Round(max);
        }
        
        public void setExp(float current, float max){
            expBar.fillAmount = current/max;
            expText.text = Math.Round(current) + "/" + Math.Round(max) + " XP";
        }

        public void setLevel(int level){
            levelIndicator.text = level.ToString();
        }

        public void setObjective(string objectiveText){
            objectiveTextView.text = objectiveText;
        }
        public void setMission(string missionText){
            missionTextView.text = missionText;
        }
        public void setQuest(string questText){
            questTextView.text = questText;
        }

        public void showInteractionPrompt(){
            interactionPrompt.SetActive(true);
        }

        public void hideInteractionPrompt(){
            interactionPrompt.SetActive(false);
        }
        
        public void showPickupPrompt(){
            pickupPrompt.SetActive(true);
        }

        public void hidePickupPrompt(){
            pickupPrompt.SetActive(false);
        }

        public void showDialogText(string message){
            dialogDisplayText.text = message;
            dialogDisplay.SetActive(true);
        }

        public void hideDialogText(){
            dialogDisplay.SetActive(false);
        }

        public void showHUD(){
            hudDisplay.SetActive(true);
        }

        public void hideHUD(){
            hudDisplay.SetActive(false);
        }
        
        public void enableCursor(){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        public void disableCursor(){
            Cursor.visible = false;
        }
        
        public void showPauseMenu(){
            pauseMenu.SetActive(true);
            director.gameManager.canPlayerMove = false;
            director.gameManager.isPauseMenuOpen = true;
            pauseCamera();
            enableCursor();
        }
        
        public void hidePauseMenu(){
            pauseMenu.SetActive(false);
            director.gameManager.canPlayerMove = true;
            director.gameManager.isPauseMenuOpen = false;
            unPauseCamera();
            disableCursor();
        }

        public void pauseCamera(){
            director.thirdPersonCameraController.m_XAxis.m_MaxSpeed = 0;
            director.thirdPersonCameraController.m_YAxis.m_MaxSpeed = 0;
        }

        public void unPauseCamera(){
            director.thirdPersonCameraController.m_XAxis.m_MaxSpeed = 300;
            director.thirdPersonCameraController.m_YAxis.m_MaxSpeed = 2;
        }

        // public void showInventory(){
        //     updateInventory();
        //     inventory.SetActive(true);
        //     director.gameManager.canPlayerMove = false;
        //     pauseCamera();
        //     director.gameManager.isInventoryOpen = true;
        //     hideHUD();                
        //     enableCursor();
        // }
        //
        // public void hideInventory(){
        //     inventory.SetActive(false);
        //     director.gameManager.canPlayerMove = true;
        //     unPauseCamera();
        //     director.gameManager.isInventoryOpen = false;
        //     showHUD();
        //     disableCursor();
        // }
        
        public void showSkills(){
            skillsWindow.SetActive(true);
            director.gameManager.canPlayerMove = false;
            pauseCamera();
            director.gameManager.isSkillMenuOpen = true;
            hideHUD();                
            enableCursor();
        }
        
        public void hideSkills(){
            skillsWindow.SetActive(false);
            director.gameManager.canPlayerMove = true;
            unPauseCamera();
            director.gameManager.isSkillMenuOpen = false;
            showHUD();
            disableCursor();
        }
        

        private IItem item;
        public void inventory_setSelectedItem(IItem reference){
            item = reference;
        }

        // public void inventory_deleteSelected(){
        //     if (item == null) return;
        //     director.player.GetComponent<Inventory>().deleteItem(item.itemId);
        //     updateInventory();
        // }
        //
        // public void inventory_dropSelected(){
        //     if (item == null) return;
        //     director.player.GetComponent<Inventory>().moveWeaponToWorldSpace(item.itemId);
        //     updateInventory();
        // }
        //
        // public void inventory_markSelectedAsFavorite(){
        //     if (item == null) return;
        //     director.player.GetComponent<Inventory>().markItemAsFavorite(item.itemId);
        //     updateInventory();
        // }
        //
        // public void inventory_markSelectedAsTrash(){
        //     if (item == null) return;
        //     director.player.GetComponent<Inventory>().markItemAsTrash(item.itemId);
        //     updateInventory();
        // }
        
        

        private readonly Color fav = new Color32(125, 164, 102, 255);
        private readonly Color trash = new Color32(164, 106, 102, 255);
        private Color normal = new Color32(164, 134, 102, 255);
        // public void updateInventory(){
        //
        //     for (var i = 0; i < itemContainer.transform.childCount; i++){
        //         itemContainer.transform.GetChild(i).gameObject.SetActive(false);
        //     }
        //
        //     if (director.player.GetComponent<Inventory>() == null) return;
        //     foreach (var item in director.player.GetComponent<Inventory>().backPackItems){
        //         var inventoryItem = Instantiate(itemViewPrefab, itemContainer.transform);
        //         inventoryItem.GetComponent<ItemReference>().reference = item.Value;
        //         if (item.Value.isFavorite)
        //             inventoryItem.transform.GetChild(0).GetComponent<Image>().color = fav;
        //         if (item.Value.isTrash)
        //             inventoryItem.transform.GetChild(0).GetComponent<Image>().color = trash;
        //         inventoryItem.GetComponent<Button>().onClick.AddListener (delegate { inventory_setSelectedItem(item.Value); });
        //         inventoryItem.transform.GetChild(2).GetComponent<Image>().sprite = item.Value.sprite ? item.Value.sprite : defaultSprite;
        //     }
        // }
    }
}