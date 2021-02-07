using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableBehaviour : MonoBehaviour
{
    private DisplayMustache displayMustache;
    private bool can_attach_detach;

    // Start is called before the first frame update
    private void Start()
    {
        can_attach_detach = false;
        displayMustache = GetComponent<DisplayMustache>();
    }

    public void SetCanInteract(bool interact)
    {
        can_attach_detach = interact;
    }

    public bool CanInteract()
    {
        return can_attach_detach;
    }
}
