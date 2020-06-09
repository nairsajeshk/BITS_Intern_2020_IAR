using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using agora_gaming_rtc;

public class SelfVidSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VideoSurface vo = GetComponent<VideoSurface>();
        if (ReferenceEquals(vo, null))
        {
            Debug.Log("cant find VO");
        }
        else
        {
            Debug.Log("VO found and initialized");
            vo.SetForUser(0);
            vo.SetEnable(true);
        }
    }
    
}
