
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
[RequireComponent(typeof(ThirdPersonCharacter))]
public class ComingPerson : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    [Header("Animation Attribute")]
    private ThirdPersonCharacter m_Character;
    public animationState state;

    public enum animationState
    {
        idle,
        autoMove,
        forward,
        backward
    }

    [Header("Moving attribute")]
    public Vector3 StartingPoint;
    public Transform destinate;
    public float movingSpeed = 0.1f;
    public float minDistance = 1f;
    public float maxDistance = 1000f;
    //float time = 0;
    [HideInInspector]public bool isPlay = false;
    public bool isAutoMove = false;
    public bool isManuelMove = false;
    public Vector3 setStartingPoint
    {
        set
        {
            StartingPoint = value;
            transform.position = value;
            Vector3 forwardVector = destinate.position;
            forwardVector.y = transform.position.y;
            transform.forward = forwardVector - transform.position;
            GetComponent<ThirdPersonCharacter>().firstRotation = transform.localEulerAngles.y;
        }
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        if (destinate == null) destinate = player.transform;
         m_Character = GetComponent<ThirdPersonCharacter>();
    }

    public float GetDistanceFromComingPersonToPlayer()
    {
        Vector2 playerPosition = new Vector2(destinate.transform.position.x, destinate.transform.position.z);
        Vector2 startPoint = new Vector2(transform.position.x, transform.position.z);

        float distance = Vector2.Distance(playerPosition, startPoint);
        return distance;
    }

    public float GetDistance()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 startPoint = new Vector2(transform.position.x, transform.position.z);

        float distance = Vector2.Distance(playerPosition, startPoint);
        return distance;
    }

    //button event
    //////////////////////////////////////////////////////
    public void PauseAndplay()
    {
        //print(isPlay);
        if(isPlay)
        {
            isAutoMove = true;
            AutoMove();
        }
        else
        {
            PauseAnimation();
        }

    }

    public void PauseAnimation()
    {
        //print("PauseAnimation");
        isAutoMove = false;
        m_Character.Move(Vector3.zero, false, false);
    }

    //public void PlayAnimation()
    //{
    //    state = animationState.forward;
    //}

    //public void PlayBackwardAnimation()
    //{
    //    state = animationState.backward;
    //}


    public void AutoMove()
    {
        isAutoMove = true;
    }
    int dir;
    public void moving(int _dir)
    {
        dir = _dir;
        isAutoMove = false;
        isManuelMove = true;
    }
    private void Update()
    {
        Vector3 m_Move = (destinate.position - StartingPoint).normalized;
        int _dir;
        //print("distance : " + GetDistanceFromComingPersonToPlayer() + " minDistance : " + minDistance);
        if (GetDistanceFromComingPersonToPlayer() > minDistance)
        {

            //print("isManuelMove : " + isManuelMove + " isAutoMove : " + isAutoMove);
            
            if (isAutoMove)
            {
                //print("auto move");
                _dir = 1;
            }
            else
            {
                _dir = 0;
            }
            if (isManuelMove)
            {
                _dir = dir;
                
            }
        }
        else if(dir == -1 && isManuelMove)
        {
            _dir = dir;
        }
        else
        {
            _dir = 0;
        }

        float moving_vector = (_dir * m_Move * movingSpeed).magnitude;

        if (moving_vector > 0)
            state = animationState.forward;
        if (moving_vector > 0 && _dir == -1)
            state = animationState.backward;
        if (moving_vector == 0)
            state = animationState.idle;

        m_Move.y = 0;

        string debuger = "_dir : " + _dir + " m_move : " + m_Move + " movingSpeed : " + movingSpeed;
        Debug.Log(debuger);
        m_Character.Move(_dir * m_Move  * movingSpeed, false, false);
        isManuelMove = false;
        
    }


}
