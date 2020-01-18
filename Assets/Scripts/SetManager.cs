using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    public SetData[] SetCollection;
    public GameObject[] EnvironmentSet;
    public bool isUseGUI;
    // Start is called before the first frame update
    void Start()
    {
        AutoSelect();
        changeEnvironment("A");
    }
    public int CalculateTotalPositionData()
    {
        int totalNumber = 0;
        foreach (var set in SetCollection)
        {
            if(set.still_Have_choosable_comingPerson())
                totalNumber += set.choosableIndex.Count;
        }
        return totalNumber;
    }
    void AutoSelect()
    {
        List<SetData> SetCollectionDataList = new List<SetData>();
        List<GameObject> EnvironmentList = new List<GameObject>();
        foreach (var obj in transform.GetComponentsInChildren<Transform>(false))
        {
            string[] obj_NameAnalize = obj.name.Split('_');

            if (obj_NameAnalize[0].Equals("Set"))
                SetCollectionDataList.Add(obj.GetComponent<SetData>());

            if (obj_NameAnalize[0].Equals("Environment"))
                EnvironmentList.Add(obj.gameObject);
        }
        EnvironmentSet = EnvironmentList.ToArray();
        SetCollection = SetCollectionDataList.ToArray();
    }

    public void changeEnvironment(string _enviornment)
    {
        foreach(var e in EnvironmentSet)
        {
            string[] obj_NameAnalize = e.name.Split('_');
            if(obj_NameAnalize[0].Equals("Environment")&
               obj_NameAnalize[1].Equals(_enviornment))
            {
                e.SetActive(true);
            }
            else
            {
                e.SetActive(false);
            }

        }
        
    }
    public List<int> choosableIndex = new List<int>();
    public bool still_Have_choosable_SetCollection()
    {
        choosableIndex = new List<int>();
        for (int i = 0; i < SetCollection.Length; i++)
        {
            if (SetCollection[i].still_Have_choosable_comingPerson())
            {
                choosableIndex.Add(i);
            }
        }
        return choosableIndex.Count > 0;
    }

    public Vector3 GetRandomSet(ref int positionState,ref  int run)
    {
        if (still_Have_choosable_SetCollection())
        {
            int rnd = choosableIndex[Mathf.Clamp(Random.Range(0, choosableIndex.Count), 0, choosableIndex.Count - 1)];
            positionState = rnd;
            return SetCollection[rnd].GetRandomCominPerson(ref run);
        }
       
        return Vector3.zero;

    }

    public void ReSetAllComingPerson_hasBeenhere()
    {
        foreach(var _SetData in SetCollection)
        {
            _SetData.ResetALLComingPerson_Attribute();
        }
    }

    private void OnGUI()
    {
        //Rect button = new Rect(900, 50, 150, 50);
        //if(GUI.Button(button,"ResetComingPerson_hasBeenThere"))
        //{
        //    ReSetAllComingPerson_hasBeenhere();
        //}
    }
}
