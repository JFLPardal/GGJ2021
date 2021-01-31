using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject owner;

    private GameObject house_opened;

    public void ShowOwner(GameObject house)
    {
        Debug.Log("show owner");
        Debug.Log(owner.activeSelf);
        Debug.Log(house_opened);
        house_opened = house;
        owner.SetActive(house_opened.GetComponent<BuildingSettings>().IsOwnerHouse());
    }
}
