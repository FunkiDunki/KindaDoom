using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class MouseOverListener : MonoBehaviour
{
    bool _mouseOver = false;

    public event Action<bool> stateChange;

    public bool mouseOver
    {
        get => _mouseOver;
        set { 
            stateChange?.Invoke(value);
            _mouseOver = value; 
        }
    }

    private void OnMouseEnter()
    {
        
    }
    private void OnMouseExit()
    {
        
    }
}
