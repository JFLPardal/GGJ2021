using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMustache : MonoBehaviour
{
    [SerializeField]
    private GameObject mustache;

    private GameObject player;
    private AttachableBehaviour attachableBehaviour;

    private void Start()
    {
        attachableBehaviour = GetComponent<AttachableBehaviour>();
    }

    public bool HasMustache()
    {
        return mustache.activeSelf;
    }

    public void AttachMustache()
    {
        if (mustache != null)
        {
            mustache.SetActive(true);
        }
    }

    public void DettachMustache()
    {
        if (mustache != null)
        {
            mustache.SetActive(false);
            player = null;
        }
    }

    public void SetPlayer(GameObject pl)
    {
        player = pl;
    }

    public void AllowPlayerToBeRevealed()
    {
        var position = new Vector2(transform.position.x, transform.position.y - 5);
        if(player != null)
            player.GetComponent<PlayerHidden>().AllowReveal(position);
    }

    public void DontAllowPlayerToBeRevealed()
    {
        player.GetComponent<PlayerHidden>().DisallowReveal();
    }
}
