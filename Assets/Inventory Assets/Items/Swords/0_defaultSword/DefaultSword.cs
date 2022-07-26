using Inventory_Assets.Items.Interfaces_and_Enums;
using UnityEngine;

namespace Inventory_Assets.Items.Swords._0_defaultSword{
    public class DefaultSword : IItem{
        public int itemId{ get; set; }
        public bool isFavorite{ get; set; }
        public bool isTrash{ get; set; }
        public Sprite sprite{ get; set; }
        public string itemName{ get; set; }
        public ItemType ItemType{ get; set; }
        public GameObject prefab{ get; set; }
        
        public ItemSlot itemSlot{ get; set; }
        
        public ItemType itemType{ get; set; }

        public DefaultSword(){
            sprite = Resources.Load<Sprite>("items/0_defaultSword/sprite");
            prefab = Resources.Load<GameObject>("items/0_defaultSword/0_defaultSwordPrefab");
            itemId = 0;
            itemName = "Ertugrul's Sword";
            ItemType = ItemType.Sword;
            itemSlot = ItemSlot.MainWeapon;
            itemType = ItemType.Sword;
            isFavorite = false;
            isTrash = false;
        }
        
    }
}