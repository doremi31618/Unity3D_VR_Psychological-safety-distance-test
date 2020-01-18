using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingPersonData : MonoBehaviour
{


    public bool Destinate_is_player = true;
    public bool hasBeenHere = false;
    public Transform _destinate;
    private void Start()
    {
        _destinate = destinate;
    }
    public Transform destinate
    {
        get
        {
            if(Destinate_is_player && _destinate == null)
            {
                _destinate = GameObject.Find("Player").transform;
            }

            return _destinate;
        }
    }
    public Transform m_transform
    {
        get
        {
            return this.transform;
        }
    }

}
