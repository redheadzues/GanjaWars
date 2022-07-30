using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class SecondPlayerControler : PlayerControler
{
    private void Awake()
    {
        Controler = new PlayerInput();

        Controler.Player2.TakeOrThrow.started += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
                IncreaseThrowForce();
        };
        Controler.Player2.TakeOrThrow.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
                ChooseAction();
            if (ctx.interaction is SlowTapInteraction)
            {
                if (_isTaken)
                    Throw();
            }
        };
        Controler.Player2.OpenMenu.performed += ctx => OpenMenu();
        Controler.Player2.NextWeapon.performed += ctx => NextWeapon();
        Controler.Player2.UseWeapon.performed += ctx => UseWeapon();
    }

    protected override void SetMoveDirection()
    {
        Direction = Controler.Player2.Move.ReadValue<Vector2>();
    }
}
