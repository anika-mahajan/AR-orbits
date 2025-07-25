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
    /// <summary>
    /// Flag to set whether to play
    /// </summary>
    [SerializeField]
    bool _shouldPlay = false;

    bool _originLocked = true;

    bool connectingSun = false;

    bool connectingOrbiter = false;

    bool connectingFocus = false;

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

    ButtonHandler PlayButton;
    // ButtonHandler OriginLock;

    [SerializeField] 
    private GameObject playBackground;
    
    [SerializeField] 
    private GameObject playCheckmark;

    ButtonHandler OriginLockButton;

    [SerializeField] 
    private GameObject lockBackground;
    
    [SerializeField] 
    private GameObject lockCheckmark;


    ButtonHandler SunButton;

    [SerializeField] 
    private GameObject sunBackground;
    
    [SerializeField] 
    private GameObject sunCheckmark;

    ButtonHandler OrbiterButton;

    [SerializeField] 
    private GameObject orbiterBackground;
    
    [SerializeField] 
    private GameObject orbiterCheckmark;

    ButtonHandler FocusButton;
    // ButtonHandler OriginLock;

    [SerializeField] 
    private GameObject focusBackground;
    
    [SerializeField] 
    private GameObject focusCheckmark;

    [SerializeField] 
    private GameObject debugCube;
    
    Cube cube;

    int index;


    // Async Start to allow for Toio connect await
    void Start()
    {
        index = 0;
        // Create a CubeManager and connect to the closest Toio
        cubeManager = new CubeManager();
        // await cubeManager.MultiConnect(3);

        // orbiterCube = cubeManager.cubes[0];
        // sunCube = cubeManager.cubes[1];
        // focusCube = cubeManager.cubes[2];

        // cube = await cubeManager.SingleConnect();
        // orbiterCube = cube;
        // cube = await cubeManager.SingleConnect();
        // sunCube = cube;
        // cube = await cubeManager.SingleConnect();
        // focusCube = cube;

        // order is sun, focus, orbit (for connecting probably)

        // orbiterHandle = cubeManager.handles[0];
        // sunHandle = cubeManager.handles[1];
        // focusHandle = cubeManager.handles[2];

        // Set behavior flags to default
        _shouldPlay = false;

        orbit = new Orbit();
        // orbit.setPositions(orbiterCube.x, orbiterCube.y, sunCube.x, sunCube.y, focusCube.x, focusCube.y);
        // orbit.calculateEllipse();

        PlayButton = new ButtonHandler();
        PlayButton.setProperties(playBackground, playCheckmark, false);

        OriginLockButton = new ButtonHandler();
        OriginLockButton.setProperties(lockBackground, lockCheckmark, true);

        SunButton = new ButtonHandler();
        SunButton.setProperties(sunBackground, sunCheckmark, false);

        OrbiterButton = new ButtonHandler();
        OrbiterButton.setProperties(orbiterBackground, orbiterCheckmark, false);

        FocusButton = new ButtonHandler();
        FocusButton.setProperties(focusBackground, focusCheckmark, false);
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
        PlayButton.changeObj(_shouldPlay);
    }

    /// <summary>
    /// Method to toggle play state
    /// </summary>
    /// <param name="state"></param>
    public async void ToggleConnectingSun(bool state)
    {
        connectingSun = state;
        cube = await cubeManager.SingleConnect();
        sunCube = cube;
        sunHandle = cubeManager.handles[index];
        index++;
        SunButton.changeObj(sunCube!=null);
    }

    /// <summary>
    /// Method to toggle play state
    /// </summary>
    /// <param name="state"></param>
    public async void ToggleConnectingOrbiter(bool state)
    {
        connectingOrbiter = state;
        cube = await cubeManager.SingleConnect();
        orbiterCube = cube;
        orbiterHandle = cubeManager.handles[index];
        index++;
        OrbiterButton.changeObj(orbiterCube!=null);
    }

    /// <summary>
    /// Method to toggle play state
    /// </summary>
    /// <param name="state"></param>
    public async void ToggleConnectingFocus(bool state)
    {
        connectingFocus = state;
        cube = await cubeManager.SingleConnect();
        focusCube = cube;
        focusHandle = cubeManager.handles[index];
        index++;
        FocusButton.changeObj(focusCube!=null);
    }

    /// <summary>
    /// Method to toggle play state
    /// </summary>
    /// <param name="state"></param>
    public void ToggleOriginLock(bool state)
    {
        _originLocked = state;
        OriginLockButton.changeObj(_originLocked);
        debugCube.SetActive(_originLocked);
    }

    public Orbit getOrbit()
    {
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
