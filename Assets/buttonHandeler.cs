using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if(UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif

public class buttonHandeler : MonoBehaviour
{
    static agoraInterface app = null;

    void Start()
    {
#if (UNITY_2018_3_OR_NEWER)
        while (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        while (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
    }

    public void onButtonClick()
    {
        Debug.Log("button clicked");

        if (name.CompareTo("joinButton") == 0)
        {
            Debug.Log("joinButton");
            joinButtonC();
        }
        else if(name.CompareTo("leaveButton") == 0)
        {
            Debug.Log("leaveButton");
            leaveButtonC();
        }
    }

    private void joinButtonC()
    {
        GameObject cNameGO = GameObject.Find("channelName");
        InputField input = cNameGO.GetComponent<InputField>();

        if (ReferenceEquals(app, null))
        {
            app = new agoraInterface();
            app.loadEngine();
        }

        app.joinChannel(input.text);
        SceneManager.LoadScene("chatRoom", LoadSceneMode.Single);
    }

    private void leaveButtonC()
    {
        if (!ReferenceEquals(app, null))
        {
            app.leaveChannel();
            app.unloadEngine();
            app = null;
            SceneManager.LoadScene("startupScene", LoadSceneMode.Single);
        }
    }
}
