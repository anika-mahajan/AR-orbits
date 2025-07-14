using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;
using System;

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

        //  Debug.Log(((orbiterCube.x - 250f)/205f) * 0.555f / 2f);
        //  Debug.Log(-1 * ((orbiterCube.y - 250f)/205f) * 0.555f / 2f);
        //  Debug.Log(((sunCube.x - 250f)/205f) * 0.555f / 2f);
        //  Debug.Log(-1 * ((sunCube.y - 250f)/205f) * 0.555f / 2f);
        //  Debug.Log(((focusCube.x - 250f)/205f) * 0.555f / 2f);
        //  Debug.Log(-1 * ((focusCube.y - 250f)/205f) * 0.555f / 2f);

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
        if(orbiterCube != null && sunCube != null && focusCube != null && _shouldPlay){
            orbit.calculateVelocity();
            orbit.updateOrbiterPos();

            orbiterHandle.Update();
            // orbiterHandle.Rotate2Rad(Math.Atan2(orbit.getOrbiterPosY(), orbit.getOrbiterPosX())).Exec();
            orbiterHandle.Move2Target(orbit.getOrbiterPosX(), orbit.getOrbiterPosY()).Exec();
            if (_planet != null)
            {
                _planet.transform.position = new Vector3((float)orbit.convertToioToUnityX(orbit.getOrbiterPosX()), 0, (float)orbit.convertToioToUnityY(orbit.getOrbiterPosY()));
            }
            // // Toio: center (250,250), extents (45, 455)
                //         // Toio board side dimension: 0.555f
                //         // Convert origin space coordinate into Toio space
                //         float x = ((_planet.transform.localPosition.x / (0.555f / 2f)) * 205) + 250;
                //         float y = -1f*(((_planet.transform.localPosition.z / (0.555f / 2f)) * 205 - 250));

                //         // Create seeking vector - must be Integer Vec2
                //         Vector2Int targetCoord = new Vector2Int((int)x, (int)y);                
                //         Movement mv = orbiterHandle.Move2Target(targetCoord).Exec(); // Executes seek as a movement
            }
        else if (orbiterCube != null && sunCube != null && focusCube != null)
        {
            orbit.setPositions(orbiterCube.x, orbiterCube.y, sunCube.x, sunCube.y, focusCube.x, focusCube.y);
            orbit.calculateEllipse();
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
