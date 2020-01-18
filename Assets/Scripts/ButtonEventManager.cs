using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// an interface to call gameManager's event 
/// </summary>
[RequireComponent(typeof(GameManager))]
public class ButtonEventManager : MonoBehaviour
{
    public GameManager manager;
    public bool isUseGUI;
    public Dropdown dropdown;
    bool isUseHostCanvas;
    bool isUseVRButtonSimulator;
    bool isPressed;
    void CheckGameObjectisExist()
    {
        if (manager == null) manager = GetComponent<GameManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CheckGameObjectisExist();
        
    }
   


    void Play()
    {
        manager.state = GameManager.GameState.gaming;
        manager.chooseComingperson(dropdown);
        manager.Initialized();
        dropdown.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //deal with character control
        if(Input.GetKeyDown(KeyCode.Space))
        {
            manager.PauseAndplay();
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            manager.MoveBackward();
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            manager.MoveForward();
        }

        //dealwith canvas switch
        switch (manager.state)
        {
            case GameManager.GameState.start:
                isUseHostCanvas = false;
                break;
            case GameManager.GameState.gaming:
                isUseHostCanvas = true;
                break;
        }
        //print("value : " + dropdown.value + "option : " + dropdown.options);
    }

    

    private void OnGUI()
    {
        if(isUseGUI)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;
            if (isUseHostCanvas)
            {


                string runIndex = "Run : " + manager.run;
                string StateIndex = "State : " + manager.positionState;
                string Distance = "Distance To Player : " + manager.distanceFromComingPersonToPlayer + " (cm)";
                string DistanceToDestinate = "Distance To Destinate : " + manager.distanceToDestinate + " (cm)";
                string RestofRunNumber = "Rest of Run Number : " +  manager.getRestOfRunNumber() + "";

                GUI.Box(new Rect(80, 180, 400, 270), "");
                // display info
                GUI.Label(new Rect(100, 200, 400, 100), StateIndex, style);
                GUI.Label(new Rect(100, 250, 400, 100), runIndex, style);
                GUI.Label(new Rect(100, 300, 400, 100), Distance, style);
                GUI.Label(new Rect(100, 350, 400, 100), DistanceToDestinate, style);
                GUI.Label(new Rect(100, 400, 400, 100), RestofRunNumber, style);
                ////button
                //if (GUI.Button(new Rect(100, 500, 200, 50), "Next state"))
                //{
                //    manager.NextState();
                //}

                //if (GUI.Button(new Rect(350, 500, 200, 50), "Reset State"))
                //{
                //    manager.ResetState(manager.positionState);
                //}

                if (GUI.Button(new Rect(100, 500, 200, 50), "Rendom Run"))
                {
                    manager.NextRun();
                }

                if (GUI.Button(new Rect(100, 600, 200, 50), "Reset Run"))
                {
                    manager._ResetRun();
                }

                if (GUI.Button(new Rect(100, 700, 200, 50), "Last Run"))
                {
                    manager.LastRun();
                }

                if (GUI.Button(new Rect(100, 800, 200, 50), "Use VR Simulator Button"))
                {
                    isUseVRButtonSimulator = !isUseVRButtonSimulator;
                }

                if (GUI.Button(new Rect(100, 900, 200, 50), "Restart"))
                {
                    SceneManager.LoadScene(0);
                }

                // VR 端按鈕
                if (isUseVRButtonSimulator)
                {
                    if (GUI.Button(new Rect(1620, 100, 200, 50), "Pause/Play"))
                    {
                        manager.PauseAndplay();
                    }

                    if (GUI.RepeatButton(new Rect(1620, 200, 200, 50), "MoveForward"))
                    {
                        manager.MoveForward();
                        //print("MoveForward");
                    }

                    if (GUI.RepeatButton(new Rect(1620, 300, 200, 50), "MoveBackforward"))
                    {

                        manager.MoveBackward();
                        //print("MoveBackward");
                    }
                }

            }
            else
            {

                if (GUI.Button(new Rect(100, 100, 200, 50), "Play"))
                {
                    
                    
                    Play();
                    
                }

                
            }

        }

    }
}
