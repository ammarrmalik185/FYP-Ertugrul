using UnityEngine;

namespace Inventory_Assets.Items.Interfaces_and_Enums{
    public interface IItem{
        public int itemId{ get; set; }
        public bool isFavorite{ get; set; }
        public bool isTrash{ get; set; }
        public Sprite sprite{ get; set; }
        public string itemName{ get; set; }
        public ItemType ItemType{ get; set; }
        public GameObject prefab{ get; set; }
        public ItemSlot itemSlot{ get; set; }
        public ItemType itemType{ get; set; }
    }
}