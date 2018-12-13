using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject ComingPersonModel;
    GameObject _ComingPersonModelClone;
    GameObject gameManager;

    [HideInInspector]
    public bool isTouchComingPerson = false;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
        MouseDragToMove();
	}
    void MouseDragToMove()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();
            //&& hit.collider.gameObject.tag == "Player"
            if(Physics.Raycast(ray,out hit) )
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                if(hit.collider.gameObject.tag == "Player")
                    this.transform.position = new Vector3(hit.point.x,transform.parent.position.y + 1f,hit.point.z);

                if (hit.collider.gameObject.tag == "ComingPerson" && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pressed Coming Person");
                    isTouchComingPerson = true;

                    gameManager.GetComponent<GameManager>().RemoveAllComingPerson();

                    _ComingPersonModelClone = Instantiate(ComingPersonModel, hit.collider.gameObject.transform.position,hit.collider.transform.rotation);
                    _ComingPersonModelClone.transform.parent = this.transform.parent;
                }
            }
        }
    }


}
