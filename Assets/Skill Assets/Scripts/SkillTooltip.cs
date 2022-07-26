using System;
using Skill_Assets.Scripts.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Skill_Assets.Scripts{
    public class SkillTooltip : MonoBehaviour{
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleDisplay;
        [SerializeField] private TextMeshProUGUI descriptionDisplay;

        private ISkill _skillRef;
        public ISkill SkillRef{
            get => _skillRef;
            set{
                _skillRef = value;
                image.sprite = _skillRef.SkillSprite;
                titleDisplay.text = _skillRef.Name;
                descriptionDisplay.text = _skillRef.Description + "\n" + _skillRef.Stats;
            }
        }
        
        private RectTransform rectTransform;

        private void Start(){
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update(){
            if (Input.anyKey){
                Hide();
            }
        }

        public void Show(){
            var rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                Input.mousePosition,
                null, //this is the thing for your camera
                out var localPosition);
            rectTransform.position = rectTransform.TransformPoint(localPosition);
        }

        public void Hide(){
            gameObject.SetActive(false);
        }
    }
}
