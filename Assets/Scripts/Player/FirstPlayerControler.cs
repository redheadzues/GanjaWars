using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class FirstPlayerControler : PlayerControler
{
    private void Awake()
    {
       Controler = new PlayerInput();

        Controler.Player1.TakeOrThrow.started += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
                IncreaseThrowForce();
        };
        Controler.Player1.TakeOrThrow.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
                ChooseAction();
            if (ctx.interaction is SlowTapInteraction)
            {
                if (_isTaken)
                    Throw();
            }
        };
        Controler.Player1.OpenMenu.performed += ctx => OpenMenu();
        Controler.Player1.NextWeapon.performed += ctx => NextWeapon();
        Controler.Player1.UseWeapon.performed += ctx => UseWeapon();
        Controler.Player1.OpenMainMenu.performed += ctx => OpenMainMenu();
    }

    protected override void SetMoveDirection()
    {
        Direction = Controler.Player1.Move.ReadValue<Vector2>();
    }
}
