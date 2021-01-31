using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateOwner : MonoBehaviour
{
    [SerializeField]
    private GameObject mustacheometer;

    [SerializeField]
    private GameObject owner;

    private BuildingDoor[] houses;
    private GameObject chosenHouse;
    // Start is called before the first frame update
    void Start()
    {
        houses = (BuildingDoor[]) FindObjectsOfType(typeof(BuildingDoor));
        ChoseRandomOwnerHouse();
        SetHouseOnMustacheometer();
        owner.transform.position = new Vector2(8, 20);
        owner.SetActive(false);
    }

    private void ChoseRandomOwnerHouse()
    {
        var id = Random.Range(0, houses.Length);
        chosenHouse = houses[id].gameObject;
    }

    private void SetHouseOnMustacheometer()
    {
        mustacheometer.GetComponent<Mustachometer>().SetOwnerPosition(chosenHouse.transform);
        owner.GetComponent<OwnerSettings>().SetHouse(chosenHouse.gameObject);
        chosenHouse.GetComponent<BuildingSettings>().SetIsOwnerHouse(true);
    }
}

