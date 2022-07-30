using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowForceSlider : Bar
{
    [SerializeField] private PlayerControler _controler;

    private void OnEnable()
    {
        _controler.CurrentThrowForceChanged += FillSlider;
        _controler.IncreaseThrowForceActivated += ChangeSliderVisibility;
    }
    private void OnDisable()
    {
        _controler.CurrentThrowForceChanged -= FillSlider;
        _controler.IncreaseThrowForceActivated += ChangeSliderVisibility;
    }

    private void ChangeSliderVisibility(bool isActive)
    {
        Slider.gameObject.SetActive(isActive);
    }
}
