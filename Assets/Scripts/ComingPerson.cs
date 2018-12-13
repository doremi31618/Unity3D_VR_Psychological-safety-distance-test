using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingPerson : MonoBehaviour {

    GameObject player;
    GameObject gameManager;
    public float movingSpeed = 0.1f;
    public float minDistance = 0.1f;
    public float maxDistance = 10f;
    Vector3 firstPosition;
    float time = 0;
    float _distance;
    public float distance
    {
        get{
            return _distance;
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        firstPosition = this.transform.position;
        StartCoroutine(IMoveForward());
    }

    private void FixedUpdate()
    {
        

    }

    bool isMoveForward = true;
    IEnumerator IMoveForward()
    {
        while(isMoveForward)
        {
            _distance = Vector3.Distance(this.transform.position, player.transform.position);
            Vector3 playerPosition = player.transform.position;

            if (_distance > minDistance)
            {
                time += Time.deltaTime;
                transform.position = (Vector3.Lerp(firstPosition, player.transform.position, time * movingSpeed));
            }
            yield return new WaitForSeconds(0.05f);
        }

    }

    bool isMoveBack = false;
    IEnumerator IMoveback()
    {
        while(isMoveBack)
        {
            _distance = Vector3.Distance(this.transform.position, player.transform.position);
            Vector3 playerPosition = player.transform.position;

            if (_distance > minDistance)
            {
                time -= Time.deltaTime;
                transform.position = (Vector3.Lerp(firstPosition, player.transform.position, time * movingSpeed));
            }
            yield return new WaitForSeconds(0.05f);
        }

    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(50, 50, 500, 500));
        {
            GUILayout.Label( "distance : " + distance);

            if (GUILayout.Button("reset position"))
            {
                player.GetComponent<Player>().isTouchComingPerson = false;
                gameManager.GetComponent<GameManager>().resetButonFunction();

                Destroy(this.gameObject);
            }
            if (GUILayout.Button("pause"))
            {
                Time.timeScale = 0;
            }
            if (GUILayout.Button("continued"))
            {
                Time.timeScale = 1;

            }
            if (GUILayout.Button("play"))
            {
                isMoveForward = true;
                isMoveBack = false;
                Time.timeScale = 1;
                time = 0;
                StartCoroutine(IMoveForward());
            }
            //StartCoroutine(IMoveForward());
            if(GUILayout.Button("back"))
            {
                isMoveForward = false;
                isMoveBack = true;
                StartCoroutine(IMoveback());

            }
        }
        GUILayout.EndArea();
    }

}
