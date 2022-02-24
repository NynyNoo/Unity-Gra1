using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class RallyPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public void Enable()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers) renderer.enabled = true;
    }

    public void Disable()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers) renderer.enabled = false;
    }
}
