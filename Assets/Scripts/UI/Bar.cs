using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public void FillSlider(float value, float maxValue)
    {
        Slider.value = value / maxValue;
    }
}
