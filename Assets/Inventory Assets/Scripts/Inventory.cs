using System.Collections.Generic;
using System.IO;
using Inventory_Assets.Items.Interfaces_and_Enums;
using Managers.Scripts;
using Player_Assets.Scripts;
using UnityEngine;

namespace Inventory_Assets.Scripts{
    public class Inventory : MonoBehaviour{
        public Dictionary<int, IItem> backPackItems;
        public Dictionary<ItemSlot, IItem> equippedItems;
        private Transform droppedItems;
        private GameDirector director;
        private void Awake(){
            director = GameDirector.getInstance();
            droppedItems = GameObject.Find("DroppedItems").transform;
            backPackItems = new Dictionary<int, IItem>();
            equippedItems = new Dictionary<ItemSlot, IItem>();
        }
        
        public void addItemToBackPack(IItem item){
            backPackItems.Add(item.itemId, item);
        }

        public void equipItem(IItem item){
            if (equippedItems.ContainsKey(item.itemSlot)){
                unEquipItem(item.itemSlot);
            }
            equippedItems.Add(item.itemSlot, item);
        }

        public void unEquipItem(ItemSlot slot){
            backPackItems.Add(equippedItems[slot].itemId, equippedItems[slot]);
            equippedItems.Remove(slot);
        }

        public void moveWeaponToWorldSpace(int id){
            var item = backPackItems[id];
            backPackItems.Remove(id);
            var location = director.player.transform.position + (director.player.transform.forward * 3);
            location.y += 2;
            var createdItem = Instantiate(item.prefab, location,  new Quaternion());
            createdItem.GetComponent<ItemReference>().reference = item;
        }
        
        public void moveWeaponToBackpackFromWorldSpace(GameObject worldItem){
            worldItem.SetActive(false);
            var item = worldItem.GetComponent<ItemReference>().reference;
            addItemToBackPack(item);
        }

        public void deleteItem(int id){
            backPackItems.Remove(id);
        }

        public void markItemAsFavorite(int id){
            var item = backPackItems[id];
            if (item.isTrash)
                item.isTrash = false;
            item.isFavorite = !item.isFavorite;
        }
        
        public void markItemAsTrash(int id){
            var item = backPackItems[id];
            if (item.isFavorite)
                item.isFavorite = false;
            item.isTrash = !item.isTrash;
        }
        
    }
}