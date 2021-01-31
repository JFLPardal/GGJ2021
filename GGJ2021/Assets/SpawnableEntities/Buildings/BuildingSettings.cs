using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSettings : MonoBehaviour
{
    private bool is_owner_house = false;

    public void SetIsOwnerHouse(bool value)
    {
        is_owner_house = value;
    }

    public bool IsOwnerHouse()
    {
        return is_owner_house;
    }
}
