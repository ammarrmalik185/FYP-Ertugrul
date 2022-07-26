using System.Collections.Generic;
using Multiplayer_Assets.Scripts;
using Story.Scripts.Others.Waypoints;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Scripts{
    public class MapManager : MonoBehaviour{
        private GameDirector gameDirector;
        private Transform sunTransform;
        private GameObject wayPointSpawn;

        public GameObject waypointPrefab;

        private List<GameObject> waypoints = new List<GameObject>();
        
        private void Start(){
            gameDirector = GetComponent<GameDirector>();
            sunTransform = gameDirector.theSun.transform;
            wayPointSpawn = GameObject.Find("Waypoints");
        }

        private void Update(){
            doDayNightCycle();
        }

        private void doDayNightCycle(){
            sunTransform.Rotate(new Vector3(0.1f * Time.deltaTime, 0, 0));
            // var sunRotation = sunTransform.rotation.eulerAngles;
            // sunRotation.x += 0.1f;
            // sunTransform.rotation = Quaternion.Euler(sunRotation);
        }
        
        public GameObject spawnWaypoint(Transform location){
            var waypoint = Instantiate(waypointPrefab, wayPointSpawn.transform);
            waypoint.GetComponent<WaypointScript>().location = location;
            waypoints.Add(waypoint);
            return waypoint;
        }

        public void purgeWaypoints(){
            foreach (var waypoint in waypoints){
                waypoint.SetActive(false);
            }
        }

        public void disConnect(){
            CustomNetworkManager.Instance.LeaveGame();
        }

        public void goToScene(string sceneName){
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
        public void quitGame(){
            Application.Quit();
        }
    }
}