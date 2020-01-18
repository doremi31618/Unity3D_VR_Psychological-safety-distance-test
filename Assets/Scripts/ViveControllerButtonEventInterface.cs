using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Valve.VR;
using System;

/// <summary>
/// Refernece : https://medium.com/@sarthakghosh/a-complete-guide-to-the-steamvr-2-0-input-system-in-unity-380e3b1b3311
/// Step 0 : you need to downlad steamvr sdk and set up vive vr head set first
/// Step 1 : Locate the item called “SteamVR Input” in the Window menu.
/// Step 2 : Add Action set when finished setting click save and generate
/// Step 3 : click open binding ui
/// Step 4 : set up in the steam vr 
/// </summary>
[RequireComponent(typeof(SteamVR_ActivateActionSetOnLoad))]
public class ViveControllerButtonEventInterface : MonoBehaviour
{
    //public event EventHandler TriggerEventHandler;

    public SteamVR_Input_Sources handType;
    //controller button action set 
    public SteamVR_Action_Boolean controller_trigger;
    public SteamVR_Action_Vector2 controller_pad;
    public SteamVR_Action_Boolean controller_pad_button;
    //public Controller controller = Controller.Right;
    [Header("Menu Button")]
    public UnityEvent MenuButtonPressedDown;

    [Header("Touchpad Button")]
    public UnityEvent TouchpadButtonTrigger;

    [Header("Trigger")]
    public UnityEvent TriggerButtonPressedDown;
    public UnityEvent TriggerButtonPressedUp;
    // Start is called before the first frame update
    void Start()
    {
        InitSteamVRControllerEvent();
    }

    protected void InitSteamVRControllerEvent()
    {
        //trigger
        controller_trigger.AddOnStateUpListener(TriggerUP, handType);
        controller_trigger.AddOnStateDownListener(TriggerDown, handType);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller_pad_button.state)
        {
            TouchPadPressDown(controller_pad.axis);
        }
    }

    //Trigger event 
    public virtual void TriggerUP(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {

        TriggerButtonPressedUp.Invoke();
    }

    public virtual void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        TriggerButtonPressedDown.Invoke();
    }

    //
    public virtual void TouchpadPressedDown(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource)
    {
        TouchpadButtonTrigger.Invoke();
    }

    public virtual void TouchPadPressDown(Vector2 axis)
    {
        if (controller_pad.GetAxis(handType).y > 0)
        {
            Debug.Log("Press above");
        }
        else if (controller_pad.GetAxis(handType).y < 0)
        {
            Debug.Log("Press below");
        }
    }


}