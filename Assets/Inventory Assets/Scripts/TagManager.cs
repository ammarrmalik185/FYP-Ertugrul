using System.Collections.Generic;
using System.Linq;
using Invector.vItemManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory_Assets.Scripts{
    public class TagManager : MonoBehaviour{
        private vItem _currentItem;
        private Tag _currentTag;
        public Tag currentItem{
            get => _currentTag;
            set{
                _currentTag = value;
                _currentItem = value.currentItem;
                favButtonText.ForEach(t => t.text = _currentItem.isFavorite ? "Un favorite" : "Favorite");
                trashButtonText.ForEach(t => t.text = _currentItem.isTrash ? "Un Trash" : "Trash");
                Debug.Log(_currentItem);
            }
        }

        public List<Text> favButtonText;
        public List<Text> trashButtonText;

        public static TagManager Instance{ get; set; }
        // Start is called before the first frame update
        private void Start(){
            Instance = this;
        }

        public void FavoriteButtonPressed(){
            Debug.Log(_currentItem);
            if (_currentItem.isTrash) _currentItem.isTrash = false;
            _currentItem.isFavorite = !_currentItem.isFavorite;
            _currentTag.Refresh();
        }
        public void TrashButtonPressed(){
            if (_currentItem.isFavorite) _currentItem.isFavorite = false;
            _currentItem.isTrash = !_currentItem.isTrash;
            _currentTag.Refresh();
        }
        
    }
}
