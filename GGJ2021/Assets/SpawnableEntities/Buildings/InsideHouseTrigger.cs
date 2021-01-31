using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventGameObject : UnityEvent<GameObject> { }

public class InsideHouseTrigger : MonoBehaviour
{
    //public UnityEvent TriggerInsideHouse;

    public UnityEvent TriggerOutsideHouse;

    public _UnityEventGameObject TriggerInsideHouse;
    public void TriggerOpenHouse(GameObject house)
    {
        TriggerInsideHouse.Invoke(house);
        ((InsideHouseSettings)FindObjectOfType(typeof(InsideHouseSettings))).ShowOwner(house);
    }

    public void TriggerLeaveHouse()
    {
        TriggerOutsideHouse.Invoke();
    }



}
