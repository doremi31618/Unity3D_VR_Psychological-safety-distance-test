using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComingPersonPositionSet : MonoBehaviour
{
    [Header("Coming Persong Trnaform set")]
    public ComingPersonDataSet[] set_collection;

}

[System.Serializable]
public class ComingPersonDataSet
{
    public ComingPersonData[] set;
}
