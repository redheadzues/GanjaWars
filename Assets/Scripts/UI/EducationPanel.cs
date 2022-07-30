using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EducationPanel : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private List<Sprite> _educationImages;
    [SerializeField] private Image _image;
    
    private int _imageIndex;

    private void OnEnable()
    {
        _imageIndex = 0;
        _image.sprite = _educationImages[_imageIndex];
        _nextButton.onClick.AddListener(NextImage);
    }

    private void OnDisable()
    {
        _nextButton?.onClick.RemoveListener(NextImage);
    }

    private void NextImage()
    {
        if (_imageIndex+1 == _educationImages.Count)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _imageIndex++;
            _image.sprite = _educationImages[_imageIndex];
        }
    }
}
