using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deactivates the Toio Simulator objects outside the Editor.
/// </summary>
public class ToioSimHandler : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.SetActive(Application.isEditor);
    }
}
