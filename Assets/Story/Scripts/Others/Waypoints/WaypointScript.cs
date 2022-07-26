using Managers.Scripts;
using TMPro;
using UnityEngine;

namespace Story.Scripts.Others.Waypoints{
    public class WaypointScript : MonoBehaviour{
        public Transform location;

        private GameDirector director;

        private Camera tpsCamera => director.thirdPersonCameraRoot;
        private Transform player => director.player.transform;
        private TextMeshProUGUI textMesh;
        private float distanceScale;
    
        private CanvasGroup container;
    
        // Start is called before the first frame update
        private void Start(){
            director = GameDirector.getInstance();
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            container = GetComponent<CanvasGroup>();
            distanceScale = director.gameManager.distanceScale;
        }

        // Update is called once per frame
        private void Update(){
            if (director.player == null) return;
            var position = location.position;
            var screenPoint = transform.position = tpsCamera.WorldToScreenPoint(position);
            textMesh.SetText((Vector3.Distance(position, player.position) * distanceScale).ToString("0.0") + "m");
            container.alpha = screenPoint.z > 0 ? 1f : 0f;
        }
    }
}
