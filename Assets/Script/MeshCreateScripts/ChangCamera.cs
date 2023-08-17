using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChangCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] CamList;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            ChangCamClockwise();
        if(Input.GetKeyDown(KeyCode.E))
            ChangCamCounterClockwise();
    }

    public void ChangCamClockwise()
    {
        int oldIndex = i;
        i = i+1<=3? i+1 : 0;
        CamList[i].Priority = 5;
        CamList[oldIndex].Priority = oldIndex;
    }

    public void ChangCamCounterClockwise()
    {
        int oldIndex = i;
        i = i-1>=0? i-1 : 3;
        CamList[i].Priority = 5;
        CamList[oldIndex].Priority = oldIndex;
    }
}
