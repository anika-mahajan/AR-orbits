using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;
using System;
using TMPro;

/// <summary>
/// Handles the behavior of a multiple Toios
/// </summary>
public class MultiToioManager : MonoBehaviour
{

    // /// <summary>
    // /// Visual target to move towards (specified in AR)
    // /// </summary>
    // [SerializeField]
    // private GameObject _target;

    /// <summary>
    /// Visual target to show planet movement
    /// </summary>
    private GameObject _planet;

    /// <summary>
    /// Flag to set whether to play
    /// </summary>
    [SerializeField]
    bool _shouldPlay = false;

    /// <summary>
    /// Text to display eccentricity
    /// </summary>
    [SerializeField] 
    private TMP_Text _eccentricity;

    // Toio space Pose Vector
    [HideInInspector]
    public Vector3 cubePose;

    // Toio references
    CubeManager cubeManager;

    // Cube[] cubes;
    Cube orbiterCube;
    Cube sunCube;
    Cube focusCube;

    CubeHandle orbiterHandle;
    CubeHandle sunHandle;
    CubeHandle focusHandle;

    Orbit orbit;
    

    // Async Start to allow for Toio connect await
    async void Start()
    {
        // Create a CubeManager and connect to the closest Toio
         cubeManager = new CubeManager();
         await cubeManager.MultiConnect(3);

         orbiterCube = cubeManager.cubes[0];
         sunCube = cubeManager.cubes[1];
         focusCube = cubeManager.cubes[2];

         // order is sun, focus, orbit (for connecting probably)

         orbiterHandle = cubeManager.handles[0];
         sunHandle = cubeManager.handles[1];
         focusHandle = cubeManager.handles[2];

         // Set behavior flags to default
         _shouldPlay = false;

         orbit = new Orbit();
         orbit.setPositions(orbiterCube.x, orbiterCube.y, sunCube.x, sunCube.y, focusCube.x, focusCube.y);
         orbit.calculateEllipse();
    }

    void FixedUpdate()
    {
        if (orbiterCube != null && sunCube != null && focusCube != null && _shouldPlay)
        {
            orbit.calculateVelocity();
            orbit.updateOrbiterPos();

            orbiterHandle.Update();
            // orbiterHandle.Rotate2Rad(Math.Atan2(orbit.getOrbiterPosY(), orbit.getOrbiterPosX())).Exec();
            orbiterHandle.Move2Target(orbit.getOrbiterPosX(), orbit.getOrbiterPosY()).Exec();
        }
        else if (orbiterCube != null && sunCube != null && focusCube != null)
        {
            orbit.setPositions(orbiterCube.x, orbiterCube.y, sunCube.x, sunCube.y, focusCube.x, focusCube.y);
            orbit.calculateEllipse();
            _eccentricity.text = "Eccentricity: " + orbit.getEccentricity();
        }
        else
        {
            _eccentricity.text = "Eccentricity: null";
        }
    }

    /// <summary>
    /// Method to toggle play state
    /// </summary>
    /// <param name="state"></param>
    public void TogglePlay(bool state)
    {
        _shouldPlay = state;
    }

    public Orbit getOrbit() {
        return orbit;
    }

    public Cube getOrbiterCube() {
        return orbiterCube;
    }
    
    public Cube getSunCube() {
        return sunCube;
    }

    public Cube getFocusCube() {
        return focusCube;
    }
}
