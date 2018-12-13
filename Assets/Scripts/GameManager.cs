using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// control the stage of game processing 
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class GameManager : MonoBehaviour {
    #region Manual setup
    public GameObject comingPersonPrefab;
    public GameObject Player;
    public Button resetButon;
    public Button deleteButton;
    #endregion

    #region public setting up

    [Range(1, 10)]
    public float distance = 1;

    [Range(1,180)]
    public int minAngle = 1;

    #endregion


    #region private attribute
    private Bounds bounds;
    private BoxCollider boxCollider;

    #endregion

    BoundaryInfo m_boundary;
    public struct BoundaryInfo 
    {
        public Vector3 corner1_1;
        public Vector3 corner1_2;
        public Vector3 corner2_1;
        public Vector3 corner2_2;
    }

    ComingPersonPositionSet m_comingPersonData;
    public struct ComingPersonPositionSet
    {
        /// <summary>
        /// array length
        /// </summary>
        public int howManyPosition;

        /// <summary>
        /// all coming person position 
        /// </summary>
        public Vector3[] positionSet; 

        /// <summary>
        /// save all the comingPerson
        /// </summary>
        public List<GameObject> comingPersons;
    }

    //setting first position in game stage
    void Initialized()
    {
        
        boxCollider = GetComponent<BoxCollider>();
        PlayerPositionReset();
        BoundarySetup();
        isGenerate = false;
    }

    /// <summary>
    /// reset player position
    /// </summary>
    void PlayerPositionReset()
    {
        Player.transform.position = boxCollider.center + Vector3.up;
    }

    /// <summary>
    /// if adjust the boundary ,need to reset the boundary Info
    /// </summary>
    void BoundarySetup()
    {
        //set up boundary info
        m_boundary.corner1_1 = new Vector3(boxCollider.bounds.min.x, 0, boxCollider.bounds.min.z);
        m_boundary.corner1_2 = new Vector3(boxCollider.bounds.max.x, 0, boxCollider.bounds.min.z);
        m_boundary.corner2_1 = new Vector3(boxCollider.bounds.min.x, 0, boxCollider.bounds.max.z);
        m_boundary.corner2_1 = new Vector3(boxCollider.bounds.max.x, 0, boxCollider.bounds.max.z);
    }
    void realTimeShowComingPerson()
    {
        Vector3 playerPosition = Player.transform.position;

        //setting m_comingPerson
        m_comingPersonData.comingPersons = new List<GameObject>();
        m_comingPersonData.howManyPosition = (int)Mathf.Floor(360 / minAngle);
        m_comingPersonData.positionSet = new Vector3[m_comingPersonData.howManyPosition];
        Debug.Log("howManyPosition : " + m_comingPersonData.howManyPosition);
        Debug.Log("index : " + m_comingPersonData.positionSet.Length);
        for (int i = 0; i < m_comingPersonData.positionSet.Length; i++)
        {
            //Mathf.Deg2Rad
            m_comingPersonData.positionSet[i] = new Vector3(distance * Mathf.Cos(Mathf.Deg2Rad * (minAngle * i)),
                                                        0,
                                                        + distance * Mathf.Sin(Mathf.Deg2Rad * (minAngle * i)));
            
            if(checkCoordinateX(m_comingPersonData.positionSet[i].x) && checkCoordinateZ(m_comingPersonData.positionSet[i].z))
            {
                
                GameObject cloneComingPersong = Instantiate(comingPersonPrefab);
                m_comingPersonData.comingPersons.Add(cloneComingPersong);
                cloneComingPersong.name += "(clone)";
                cloneComingPersong.transform.parent = Player.transform;
                cloneComingPersong.transform.position = m_comingPersonData.positionSet[i];
                cloneComingPersong.transform.LookAt(Player.transform);
            }
               
        }
        isGenerate = true;
    }
    #region check the coordinate
    bool checkCoordinateX(float x)
    {
        if (x > boxCollider.bounds.min.x && x < boxCollider.bounds.max.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool checkCoordinateZ(float z)
    {
        if (z > boxCollider.bounds.min.z && z < boxCollider.bounds.max.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    public void RemoveAllComingPerson()
    {
        //for (int i = 0; i < m_comingPersonData.comingPersons.Count; i++)
        //{
        //    Destroy(m_comingPersonData.comingPersons[i]);
        //}

        foreach(GameObject mc in m_comingPersonData.comingPersons)
        {
            Destroy(mc);
        }
        m_comingPersonData.comingPersons.Clear();
    }
    void CheckAlltheComingPerson()
    {
        if (m_comingPersonData.comingPersons.Count == 0)
            return;
        for (int i = 0; i < m_comingPersonData.howManyPosition; i++)
        {
            bool isVisible = m_comingPersonData.comingPersons[i].GetComponent<MeshRenderer>().enabled;
            Vector3 _comingPerson = m_comingPersonData.comingPersons[i].transform.position;
            if (!checkCoordinateX(_comingPerson.x) || !checkCoordinateZ(_comingPerson.z))
            {
                m_comingPersonData.comingPersons[i].GetComponent<MeshRenderer>().enabled = false;
               
            }
            else if
                (checkCoordinateX(_comingPerson.x) && checkCoordinateZ(_comingPerson.z) && !isVisible)
            {
                m_comingPersonData.comingPersons[i].GetComponent<MeshRenderer>().enabled = true;
            }

        }
    }

    public void resetButonFunction()
    {
        
        Initialized();
        RemoveAllComingPerson();
        realTimeShowComingPerson();
    }

    // Use this for initialization
    void Start ()
    {
        Initialized();
        resetButon.onClick.AddListener(resetButonFunction);
        deleteButton.onClick.AddListener(RemoveAllComingPerson);
	}
    bool isGenerate = false;
	// Update is called once per frame
	void Update ()
    {
        
        if(!isGenerate)
            realTimeShowComingPerson();

        if (m_comingPersonData.comingPersons != null)
            CheckAlltheComingPerson();


	}
}


