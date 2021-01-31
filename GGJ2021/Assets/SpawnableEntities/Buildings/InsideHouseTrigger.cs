using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InsideHouseTrigger : MonoBehaviour
{
    public UnityEvent TriggerInsideHouse;

    public UnityEvent TriggerOutsideHouse;
    public void TriggerOpenHouse()
    {
        TriggerInsideHouse.Invoke();
    }

    public void TriggerLeaveHouse()
    {
        TriggerOutsideHouse.Invoke();
    }
}
