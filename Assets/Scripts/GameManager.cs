using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectMode
{
    Manual,
    Automatic,
    random
}
/// <summary>
/// control the stage of game processing & all the event 
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class GameManager : MonoBehaviour {
    #region Manual setup
    [Header("Prefab")]
    public List<ComingPerson> comingPeople;
    public ComingPerson comingPerson;
    public Player Player;
    public GameObject IsometricCamera;

    [Header("Other Manager")]
    public SetManager setManager;
    //public PlayerPositionCollection playerPositionCollection;
    //public ComingPersonPositionSet comingPersonPositionSet;
    public ButtonEventManager buttonManager;

    [Header("Manager Attributes")]
    [Tooltip("Automatic:randomly choose next position")]
    public SelectMode selectMode;
    public int positionState;
    [Tooltip("what index you want proceed")]
    [SerializeField]
    public int run;
    public float distanceFromComingPersonToPlayer;
    public float distanceToDestinate;
    //other setting
    public enum GameState { start, gaming};
    public GameState state = GameState.start;

    #endregion
    // Use this for initialization
    void Start()
    {
        Initialized();
    }
    void UpdateInfo()
    {
        distanceFromComingPersonToPlayer = comingPerson.GetDistance() * 100;
        distanceToDestinate = comingPerson.GetDistanceFromComingPersonToPlayer() * 100;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateInfo();

    }
    public void Initialized()
    {

        //reset player position
        positionState = 0;
        resetPlayerPosition();

        //reset coming person positiont & animation state;
        run = 0;
        ResetRun();
        setManager.ReSetAllComingPerson_hasBeenhere();
    }
    
    public void chooseComingperson(Dropdown dropdown)
    {
        for(int i =0;i< comingPeople.Count;i++)
        {
            if(i==dropdown.value)
            {
                comingPeople[i].gameObject.SetActive(true);
                comingPerson = comingPeople[i];
            }
            else
            {
                comingPeople[i].gameObject.SetActive(false);
            }
        }
        ResetRun();


    }

    void resetPlayerPosition()
    {

        Player.SetPosition = setManager.SetCollection[positionState].player_transform.position;
        Player.SetRotation = setManager.SetCollection[positionState].player_transform.eulerAngles;

        IsometricCamera.transform.position = Player.transform.position + Vector3.up * 10;

        resetEnvironment();


    }
    private int lastPositionState;
    private int lastRun;
    void resetComingPersonPosition(SelectMode mode)
    {
        Vector3 startPoint = Vector3.zero;
        lastPositionState = positionState;
        lastRun = run;
        switch (mode)
        {
            case SelectMode.Automatic:
                startPoint = setManager.SetCollection[positionState].ComingPerson_set[run].transform.position;
               
                break;
            case SelectMode.random:
                startPoint = setManager.GetRandomSet(ref positionState, ref run);
                resetPlayerPosition();
                break;
            default:
                startPoint = comingPerson.setStartingPoint = setManager.SetCollection[positionState].ComingPerson_set[run].transform.position;
                break;
        }
        
        comingPerson.destinate = setManager.SetCollection[positionState].ComingPerson_set[run].destinate;
        comingPerson.setStartingPoint = startPoint;

        setManager.SetCollection[positionState].ComingPerson_set[run].hasBeenHere = true;
    }

   private void lastComingPersonPosition()
    {
        setManager.SetCollection[positionState].ComingPerson_set[run].hasBeenHere = false;
        positionState = lastPositionState;
        run = lastRun;

        Vector3 startPoint = setManager.SetCollection[positionState].ComingPerson_set[run].transform.position;
        comingPerson.destinate = setManager.SetCollection[positionState].ComingPerson_set[run].destinate;
        comingPerson.setStartingPoint = startPoint;
        setManager.SetCollection[positionState].ComingPerson_set[run].hasBeenHere = true;
        comingPerson.GetComponent<ComingPerson>().PauseAnimation();
    }

    public void LastRun()
    {
        lastComingPersonPosition();
        resetPlayerPosition();
    }

    /// <summary>
    /// reset state with _state index 
    /// </summary>
    /// <param name="_state"></param>
    public void ResetState(int _state)
    {
        //reset player position
        positionState = _state;
        resetPlayerPosition();

        //reset coming person positiont & animation state;
        run = 0;
        ResetRun();

    }

    public void NextState()
    {
        //deprecated 
        //positionState < comingPersonPositionSet.set_collection.Length - 1
        if (positionState < setManager.SetCollection.Length - 1)
            ResetState(positionState += 1);
        else
        {
            positionState = 0;
            ResetState(positionState);
        }

        //reset player position
        resetPlayerPosition();

        Debug.Log("positionState : " + positionState);
        resetEnvironment();

        //reset coming person positiont & animation state;
        run = 0;
        ResetRun();
    }

    void resetEnvironment()
    {
        if (positionState == 0 || positionState == 6 || positionState == 7 || positionState == 8)
            setManager.changeEnvironment("A");
        else if (positionState == 1 || positionState == 2 || positionState == 3)
            setManager.changeEnvironment("B");
        else if (positionState == 4 || positionState == 5)
            setManager.changeEnvironment("C");
    }

    // setting run 
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// if directly call this function ,currecnt run will be reset 
    /// </summary>
    public void ResetRun()
    {
        resetComingPersonPosition(selectMode);
        comingPerson.GetComponent<ComingPerson>().PauseAnimation();
    }
    public void _ResetRun()
    {
        Vector3 startPoint = setManager.SetCollection[positionState].ComingPerson_set[run].transform.position;
        comingPerson.destinate = setManager.SetCollection[positionState].ComingPerson_set[run].destinate;
        comingPerson.setStartingPoint = startPoint;
        // setManager.SetCollection[positionState].ComingPerson_set[run].hasBeenHere = true;
        comingPerson.GetComponent<ComingPerson>().PauseAnimation();
    }

    /// <summary>
    /// "run" will automatic increse 1 until "run + 1" greater then "Length of array"
    /// when that happen automatic increse position state
    /// </summary>
    public void NextRun()
    {
        
        
        if(selectMode.Equals(SelectMode.Automatic))
        {
            int indexCount = setManager.SetCollection[positionState].ComingPerson_set.Length;
            AutoModeRun(indexCount);
        }
           
        ResetRun();

    }
    //List<int> next_run_list;
    //void RandomRun(List<int> list_of_nextrun,)
    //{

    //}
    void AutoModeRun(int _indexCount)
    {
        //run increse 1
        if (run < _indexCount - 1)
        {
            run += 1;

        }
        //position state increse 1
        //if position state is final state then trigger ending event  
        else
        {
            NextState();
        }
    }
    public int getRestOfRunNumber()
    {
        return setManager.CalculateTotalPositionData();
    }
    /////////////////////////////////////////////////////////////////////////

    //when all the position state finished trigger 
    void EndingEvent()
    {

    }

    //VR 端
    ///////////////////////////////////////////////////////////////////////
    ///

    
    public void PauseAndplay()
    {
        if (state != GameState.gaming) return;
        comingPerson.isPlay = !comingPerson.isPlay;
        comingPerson.PauseAndplay();
    }

    public void MoveForward()
    {
        if (state != GameState.gaming) return;
        comingPerson.moving(1);
    }

    public void MoveBackward()
    {
        if (state != GameState.gaming) return;
        comingPerson.moving(-1);
    }
}


