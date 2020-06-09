using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using agora_gaming_rtc;

public class agoraInterface : MonoBehaviour
{
    private static string appId = "4b15c856c4d844d4beff7556627ed041";
    public IRtcEngine mRtcEngine;
    public uint mRemotePeer;


  

    public void loadEngine()
    {
        Debug.Log("Loading engine");
       if(mRtcEngine != null)
        {
            Debug.Log("Engine already loaded");
            return;
       }

        mRtcEngine = IRtcEngine.getEngine(appId);
    }

    public void joinChannel(string channelName)
    {
        Debug.Log("joining channel");
        if (mRtcEngine == null)
        {
            Debug.Log("engine not found");
            return;
        }

        mRtcEngine.OnJoinChannelSuccess = onJoinChannelSuccess;
        mRtcEngine.OnUserJoined = onUserJoined;
        mRtcEngine.OnUserOffline = onUserOffline;


        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();
        mRtcEngine.JoinChannel(channelName, null, 0);

        Debug.Log("joined");
    }


    public void leaveChannel()
    {
        Debug.Log("leavig channel");
        if (mRtcEngine == null)
        {
            Debug.Log("leaving engine not found");
            return;
        }

        mRtcEngine.LeaveChannel();
        mRtcEngine.DisableVideoObserver();
    }

    public void unloadEngine()
    {

        Debug.Log("destroying engine");
        if (mRtcEngine == null)
        {
            Debug.Log("no engine to destroy");
            return;
        }

        IRtcEngine.Destroy();
        mRtcEngine = null;
    }


    //callbacks
    private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("onJoinChannelSuccess");
    }

    private void onUserJoined(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined");
        GameObject go = GameObject.Find("other");
       // go.name = uid.ToString();

        VideoSurface o = go.AddComponent<VideoSurface>();
        o.SetForUser(uid);
        o.SetEnable(true);
       // o.transform.Rotate(-90, 0, 0);
       
        //o.transform.localScale = new Vector3(1, 1, 1);

        mRemotePeer = uid;

    }

    private void onUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        Debug.Log("onUserOffline");
        GameObject go;
        go = GameObject.Find("other");
        VideoSurface o = go.GetComponent<VideoSurface>();
        if (!ReferenceEquals(o, null))
        {
            Destroy(o);
        }
    }

}
