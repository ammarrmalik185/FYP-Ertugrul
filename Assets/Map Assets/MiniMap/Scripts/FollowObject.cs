using Managers.Scripts;
using UnityEngine;

namespace Map_Assets.MiniMap.Scripts{
    public class FollowObject : MonoBehaviour{

        public Transform followObject;
    
        // LateUpdate is called once per frame
        private void LateUpdate(){
            if (followObject == null){
                if (GameDirector.getInstance().player == null) return;
                followObject = GameDirector.getInstance().player.transform;
            }
            var position = followObject.transform.position;
            transform.position = new Vector3(position.x, position.y + 30, position.z);
            transform.rotation = Quaternion.Euler(90f, followObject.rotation.eulerAngles.y, 0f);
        }
    }
}
