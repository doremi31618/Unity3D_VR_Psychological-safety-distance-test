using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Valve.VR;
using System;

public class ViveController_VRTest : ViveControllerButtonEventInterface
{
    [Header("new custom event")]
    public UnityEvent play_pause;
    public UnityEvent forward;
    public UnityEvent backward;
    void Start()
    {
        InitSteamVRControllerEvent();
    }
    void Update()
    {
        if (controller_pad_button.state)
        {
            TouchPadPressDown(controller_pad.axis);
        }
    }
    public override void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        TriggerButtonPressedDown.Invoke();
        play_pause.Invoke();
        Debug.Log("Press play/pause");
    }

    public override void TouchPadPressDown(Vector2 axis)
    {
        if (controller_pad.GetAxis(handType).y > 0)
        {
            Debug.Log("Press above");
            forward.Invoke();
        }
        else if (controller_pad.GetAxis(handType).y < 0)
        {
            Debug.Log("Press below");
            backward.Invoke();
        }
    }

    void OnGUI()
    {
        string StateIndex = (controller_pad_button == null)  || (controller_pad == null) || (controller_trigger == null) ? "action not been setting" : "";
        GUI.Label(new Rect(900, 300, 200, 50), StateIndex);
    }
}
