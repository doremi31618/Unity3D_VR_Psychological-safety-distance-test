using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //public GameObject ComingPersonModel;
    public GameObject PerspectiveCamera;
    GameObject _ComingPersonModelClone;
    GameObject gameManager;
    public GameObject PlayerCamera;
    public Vector3 SetPosition
    {
        set
        {
                transform.position = value;

        }
    }

    public Vector3 SetRotation
    {
        set
        {
            transform.eulerAngles = value;
            PerspectiveCamera.transform.rotation = transform.rotation;
        }
    }

    [HideInInspector]
    public bool isTouchComingPerson = false;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
        //MouseDragToMove();
	}


}
