using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReadyButtonColorSetter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color _targetColorOnReady;

    private Image _imageButton;
    private Color _startColor;

    private void Awake()
    {
        _imageButton = GetComponent<Image>();
        _startColor = _imageButton.color;
    }

    private void OnEnable()
    {
        _imageButton.color = _startColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_imageButton.color == _startColor)
            _imageButton.color = _targetColorOnReady;
        else
            _imageButton.color = _startColor;
    }
}
