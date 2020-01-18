using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetData : MonoBehaviour
{
    public Transform player_transform;
    public ComingPersonData[] ComingPerson_set;

    private void OnEnable()
    {
        AutoSelect();
        still_Have_choosable_comingPerson();
    }

    void AutoSelect()
    {
        List<ComingPersonData> ComingPersonDataList = new List<ComingPersonData>();
        foreach (var obj in transform.GetComponentsInChildren<Transform>(false))
        {
            string[] obj_NameAnalize = obj.name.Split('_');

            if (obj_NameAnalize[0].Equals("Player"))
                player_transform = obj.transform;

            if (obj_NameAnalize[0].Equals("ComingPerson"))
                ComingPersonDataList.Add(obj.GetComponent<ComingPersonData>());

        }
        ComingPerson_set = ComingPersonDataList.ToArray();
    }

    public void ResetALLComingPerson_Attribute()
    {
        foreach(var _comingPersonData in ComingPerson_set)
        {
            _comingPersonData.hasBeenHere = false;
        }
    }

    public List<int> choosableIndex = new List<int>();
    public bool still_Have_choosable_comingPerson()
    {
        choosableIndex = new List<int>();
        for (int i = 0; i < ComingPerson_set.Length; i++)
        {
            if (!ComingPerson_set[i].hasBeenHere)
            {
                choosableIndex.Add(i);
            }
        }
        return choosableIndex.Count > 0;
    }

    public Vector3 GetRandomCominPerson(ref int run)
    {
        Vector3 position = new Vector3(0,0,0);
        if(still_Have_choosable_comingPerson())
        {
            run = choosableIndex[Random.Range(0, choosableIndex.Count - 1)];

        }
        else
        {
            run = 0;

        }
        position = ComingPerson_set[run].m_transform.position;
        return position;
        
    }
}
