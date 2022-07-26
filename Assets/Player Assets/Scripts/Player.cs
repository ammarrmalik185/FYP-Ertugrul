using System;
using System.Collections.Generic;
using System.Linq;
using Invector.vCharacterController;
using Invector.vMelee;
using Inventory_Assets.Items.Interfaces_and_Enums;
using Inventory_Assets.Items.Swords._0_defaultSword;
using Inventory_Assets.Scripts;
using Managers.Scripts;
using Mirror;
using Multiplayer_Assets.Scripts;
using Skill_Assets.Scripts;
using Steamworks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player_Assets.Scripts{
    public class Player : MonoBehaviour{
        
        private float totalXp;
        public float currentXp;
        public int level;

        private GameDirector director;
        // public Inventory inventory;
        public vThirdPersonController controller;
        public vMeleeCombatInput meleeManager;
        public SkillManager skillManager;

        private void Awake(){
            director = GameDirector.getInstance();
        }

        private void Start(){
            level = 1;
            controller = GetComponent<vThirdPersonController>();
            meleeManager = GetComponent<vMeleeCombatInput>();
            skillManager = GetComponent<SkillManager>();
            controller.maxHealth = LevelScaling.getTotalHp(level);
            
            totalXp = LevelScaling.getTotalXp(level);
            currentXp = 0;
            controller.onChangeHealth.AddListener(v => updateUI());
            updateUI();

            director.gameSaveManager.loadGameData();
        }

        private void Update(){
            handleLevelup();
            meleeManager.lockMeleeInput = !GameDirector.getInstance().gameManager.canPlayerMove;
            controller.lockMovement = !GameDirector.getInstance().gameManager.canPlayerMove;
        }

        private void handleLevelup(){
            if (currentXp >= totalXp){
                setLevel(level + 1);
                currentXp %= totalXp;
                skillManager.freeSkillPoints += 1;
                updateUI();
            }
        }

        public void setLevel(int newLevel){
            level = newLevel;
            totalXp = LevelScaling.getTotalXp(level);
            controller.maxHealth = LevelScaling.getTotalHp(level);
            controller.currentHealth = controller.maxHealth;
        }
        
        public void setCurrentXp(float newXp){
            currentXp = newXp;
        }
        
        public void setCurrentHealth(float newHealth){
            controller.currentHealth = newHealth;
        }

        private void updateUI(){
            director.uiManager.setHp(controller.currentHealth, controller.maxHealth);
            director.uiManager.setExp(currentXp, totalXp);
            director.uiManager.setLevel(level);
        }

        public void giveXp(float xp){
            currentXp += xp;
            updateUI();
        }

        public void giveDamage(float damage){
            controller.currentHealth -= damage;
            if (controller.currentHealth < 0){
                controller.currentHealth = 0;
            }
            updateUI();
        }

        public void giveHp(float hp){
            controller.currentHealth += hp;
            if (controller.currentHealth > controller.maxHealth){
                controller.currentHealth = controller.maxHealth;
            }
            updateUI();
        }
    }
}