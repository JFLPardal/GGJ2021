using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableBehaviour : MonoBehaviour
{
    private bool can_attach_detach;
    [SerializeField] private ParticleSystem particleSystem;
    private ParticleSystem.MainModule ps_main;
    private bool is_playing = false;

    // Start is called before the first frame update
    private void Start()
    {
        can_attach_detach = false;
        ps_main = particleSystem.main;
    }

    public void HandleCanInteractIndicator()
    {
        if (can_attach_detach && !is_playing)
        {
            particleSystem.Play();
            is_playing = true;
        }
        else if(!can_attach_detach && is_playing)
        {
            particleSystem.Stop();
            is_playing = false;
        }
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
