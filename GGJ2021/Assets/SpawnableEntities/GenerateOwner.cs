using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GenerateOwner : MonoBehaviour
{
    [SerializeField]
    private Mustachometer mustacheometer;

    [SerializeField]
    private GameObject owner;

    private BuildingDoor[] houses;
    private GameObject chosenHouse;
    // Start is called before the first frame update
    void Start()
    {
        /*houses = (BuildingDoor[]) FindObjectsOfType(typeof(BuildingDoor));
        ChoseRandomOwnerHouse();
        SetHouseOnMustacheometer();
        owner.transform.position = new Vector2(-15, 20);
        owner.SetActive(false);*/
    }

    public void OnGameOver()
    {
        owner.SetActive(true);
    }

    public void OnStartGame()
    {
        Debug.Log("generate owner on start");
        SetNecessaryObjects();
        houses = (BuildingDoor[])FindObjectsOfType(typeof(BuildingDoor));
        ChoseRandomOwnerHouse();
        SetHouseOnMustacheometer();
        owner.transform.position = new Vector2(-15, 20);
        owner.SetActive(false);
    }

    private void SetNecessaryObjects()
    {
        mustacheometer = (Mustachometer)FindObjectOfType(typeof(Mustachometer));
        owner = GameObject.FindGameObjectWithTag(Constants.owner_tag);
        Debug.Log(owner == null);
    }

    private void ChoseRandomOwnerHouse()
    {
        var id = Random.Range(0, houses.Length);
        chosenHouse = houses[id].gameObject;
    }

    private void SetHouseOnMustacheometer()
    {
        mustacheometer.SetOwnerPosition(chosenHouse.transform);
        owner.GetComponent<OwnerSettings>().SetHouse(chosenHouse.gameObject);
        chosenHouse.GetComponent<BuildingSettings>().SetIsOwnerHouse(true);
    }
}

