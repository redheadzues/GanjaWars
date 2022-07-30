using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerControler))]
public class Renderer : MonoBehaviour
{
    [SerializeField] private float _holdDistance;

    private SpriteRenderer _renderer;
    private PlayerControler _controler;
    private Vector2 _lastDirection;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _controler = GetComponent<PlayerControler>();
    }

    private void Update()
    {
        SetLastDirrection();
        RenderFlip();

        if(_controler.TakenObject != null)
            RenderDirectionTakenObject();
    }

    private void RenderFlip()
    {
        if (_controler.Direction.x < 0)
            _renderer.flipX = true;
        else if (_controler.Direction.x > 0)
            _renderer.flipX = false;
    }

    private void RenderDirectionTakenObject()
    {
        if (_lastDirection.x > 0)
        {
            _controler.TakenObject.transform.localPosition = new Vector2(_holdDistance, 0);
        }
        else if(_lastDirection.x < 0)
        {
            _controler.TakenObject.transform.localPosition = new Vector2(_holdDistance * -1, 0);
        }
    }

    private void SetLastDirrection()
    {
        if (_controler.Direction != Vector2.zero)
            _lastDirection = _controler.Direction;
    }
}
