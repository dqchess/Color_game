using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class MoveToNextObject : MonoBehaviour
{


    
    void Start()
    {
       

    }
    public void MoveNext(GameObject to)//For disabling the first and enabling the next obj
    {
        gameObject.SetActive(false);
        if (to.name != "LastHint")
        {
            to.SetActive(true);
        }
    }


}