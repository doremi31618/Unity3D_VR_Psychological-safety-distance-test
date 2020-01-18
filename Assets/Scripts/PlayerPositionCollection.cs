using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerPositionCollection : MonoBehaviour
{

    public bool isAutoSelectPlayerPosition = false;
    public Transform[] player_position_collection;

    private void Start()
    {
        if(isAutoSelectPlayerPosition)
        {
            autoSelectPlayerPosition();
        }
    }
    void autoSelectPlayerPosition()
    {
        List<Transform> playerTransformCollection = new List<Transform>();
        foreach (var player in  transform.GetComponentsInChildren<Transform>(false))
        {
            string playerName = player.name;
            string[] playerNameAnalize = playerName.Split('_');

            if (!playerNameAnalize[0].Equals("Player")) continue;
            playerTransformCollection.Add(player);



            /// use for debug
            //string debuger = "";
            //foreach(string t in playerNameAnalize)
            //{
            //    debuger += t + " ";

            //}

            //Debug.Log(debuger);

        }
        

        player_position_collection = playerTransformCollection.ToArray();
    }

    
}
