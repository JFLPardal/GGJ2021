using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject door_lock;

    private bool is_locked;
    private void Start()
    {
        door_lock.SetActive(false);
    }
    public void LockDoor()
    {
        door_lock.GetComponent<SpriteRenderer>().flipX = true;
        door_lock.SetActive(true);
        is_locked = true;
    }

    public void UnlockDoor()
    {
        door_lock.SetActive(false);
        is_locked = false;
    }

    public bool IsDoorLocked()
    {
        return is_locked;
    }
}
