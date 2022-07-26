using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Managers.Scripts{
    public class SettingsMenuManager : MonoBehaviour{
        public AudioMixer mainAudioMixer;
        private Resolution[] resolutions;
        public Dropdown resolutionDropdown;

        private void Start(){
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            var currentResolutionIndex = 0;
            resolutionDropdown.AddOptions(resolutions.Select((value, index) => {
                if (value.width == Screen.currentResolution.width && value.height == Screen.currentResolution.height){
                    currentResolutionIndex = index;
                }
                return value.width + " x " + value.height;
            }).ToList());
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void SetVolume(float volume, VolumeTypes type){
            mainAudioMixer.SetFloat(type.ToString(), volume);
        }

        public void SetVolumeMaster(float volume){
            SetVolume(volume, VolumeTypes.masterVolume);
        }
        
        public void SetVolumeMusic(float volume){
            SetVolume(volume, VolumeTypes.musicVolume);
        }
        
        public void SetVolumeSfx(float volume){
            SetVolume(volume, VolumeTypes.sfxVolume);
        }
        
        public void SetVolumeDialog(float volume){
            SetVolume(volume, VolumeTypes.dialogVolume);
        }

        public void SetQuality(int index){
            QualitySettings.SetQualityLevel(index);
        }

        public void SetFullScreen(bool isFull){
            Screen.fullScreen = isFull;
        }

        public void SetResolution(int index){
            var resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
