using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BBEventSystem : MonoBehaviour
{
    public UnityEvent BB_Following;

    public UnityEvent BB_Stopped_Following;

    public void TriggerBBFollowing()
    {
        BB_Following.Invoke();
    }

    public void TriggerBBStoppedFollowing()
    {
        BB_Stopped_Following.Invoke();
    }
}
