using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject door_lock;

    private AudioSource source;

    private bool is_locked;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        door_lock.SetActive(false);
    }
    public void LockDoor()
    {
        source.Play();
        door_lock.GetComponent<SpriteRenderer>().flipX = true;
        door_lock.SetActive(true);
        is_locked = true;
    }

    public void UnlockDoor()
    {
        source.Play();
        door_lock.SetActive(false);
        is_locked = false;
    }

    public bool IsDoorLocked()
    {
        return is_locked;
    }
}
