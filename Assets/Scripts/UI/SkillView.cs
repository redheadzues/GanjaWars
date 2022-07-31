using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillView : Bar
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _buttonText;

    public event UnityAction<Skill, SkillView> OnButtonClick;
    
    private Skill _skill;
    private string _baseText;

    private void Awake()
    {
        _baseText = _buttonText.text; 
    }

    public void FillSkillTemplate(Skill skill, PlayerSkills playerSkill)
    {
        _skill = skill;
        skill.SetValues(playerSkill);
        _description.text = skill.Description + " " + skill.Value;
        _buttonText.text = _baseText + " " + skill.UpgradeCost.ToString();
        FillSlider(skill.Value, skill.MaxValue);
    }

    public void LockUpgradeButton()
    {
        _upgradeButton.interactable = false;
        _buttonText.text = "Максимально улучшенно";
    }

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    private void OnUpgradeButtonClick()
    {
        OnButtonClick?.Invoke(_skill, this);
    }

}
