using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoxicatedBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.IntoxicationChanged += FillSlider;
        Slider.value = 0;
    }

    private void OnDisable()
    {
        _player.IntoxicationChanged -= FillSlider;
    }
}
