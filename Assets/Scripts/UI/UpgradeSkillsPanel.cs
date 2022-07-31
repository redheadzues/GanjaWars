using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillsPanel : MonoBehaviour
{
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private SkillView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private PlayerSkills _playerSkill;
    [SerializeField] private Player _player;

    private void Start()
    {
        for(int i = 0; i < _skills.Count; i++)
        {
            AddSkill(_skills[i]);
        }
    }

    private void AddSkill(Skill skill)
    {
        var view = Instantiate(_template, _container.transform);
        view.FillSkillTemplate(skill, _playerSkill);
        view.OnButtonClick += TryUpgradeSkill;
    }

    private void TryUpgradeSkill(Skill skill, SkillView view)
    {
        if(skill.UpgradeCost < _player.Score)
        {
            _player.SpendScore(skill.UpgradeCost);
            skill.Increase(_playerSkill);
            view.FillSkillTemplate(skill, _playerSkill);
            
            if(skill.Value == skill.MaxValue)
            {
                view.OnButtonClick -= TryUpgradeSkill;
                view.LockUpgradeButton();
            }
        }
    }
}