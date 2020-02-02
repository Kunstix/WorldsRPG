using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntranceController : MonoBehaviour
{
    public string areaTransitionName;

        void Start()
    {
        if (areaTransitionName == PlayerController.player.areaTransitionName)
        {
            PlayerController.player.transform.position = transform.position;
        }
    }

    void Update()
    {
        
    }
}
