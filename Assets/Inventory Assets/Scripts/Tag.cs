using System;
using Invector.vItemManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory_Assets.Scripts{
    public class Tag : MonoBehaviour {
        public Image backgroundImage;
        public vItem currentItem;
      
        private void SetFavorite(){
            backgroundImage.color = Color.green;
        }

        private void SetTrash(){
            backgroundImage.color = Color.red;
        }

        private void SetNormal(){
            backgroundImage.color = Color.white;
        }

        public void ItemChanged(vItem newItem){
            currentItem = newItem;
            Refresh();
        }

        public void SetItemRef(){
            if (TagManager.Instance != null){
                TagManager.Instance.currentItem = this;
            }
        }

        public void Refresh(){
            if (currentItem.isFavorite)
                SetFavorite();
            else if (currentItem.isTrash)
                SetTrash();
            else
                SetNormal();
        }
    }
}
