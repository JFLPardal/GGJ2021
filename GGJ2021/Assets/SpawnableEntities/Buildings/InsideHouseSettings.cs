using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject owner;

    private GameObject house_opened;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void ShowOwner(GameObject house)
    {
        if(source == null) source = GetComponent<AudioSource>();
        source.Play();
        house_opened = house;
        owner.SetActive(house_opened.GetComponent<BuildingSettings>().IsOwnerHouse());
        owner.GetComponent<OwnerSettings>().UpdateAnimation();
    }
}
