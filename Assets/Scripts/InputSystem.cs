using System;
using System.Collections;
using System.Collections.Generic;
using LanguageExt;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using static LanguageExt.Prelude;

public class InputSystem : MonoBehaviour
{
    private static readonly  Subject<Vector2> _move = new Subject<Vector2>();

    private Option<Vector2> currentVectorMove = None;

    public static IObservable<Vector2> OnMove() => _move.AsObservable();

    private void Update()
    {
        currentVectorMove.IfSome(v => _move.OnNext(v));
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        Debug.LogFormat("The control path is '{0}'", context.control);
        currentVectorMove = applyPhase(context.phase, context.ReadValue<Vector2>());
    }

    private Option<Vector2> applyPhase(InputActionPhase phase, Vector2 vector)
    {
        switch (phase)
        {
            case InputActionPhase.Started:
                return Some(vector);
            case InputActionPhase.Performed:
                return Some(vector);
            case InputActionPhase.Canceled:
                return None;
            default:
                return None;
        }
    }

    private void OnDestroy()
    {
        _move.OnCompleted();
        _move.Dispose();
    }
}
