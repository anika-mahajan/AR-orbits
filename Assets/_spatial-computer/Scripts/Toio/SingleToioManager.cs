using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;
using System;

/// <summary>
/// Handles the behavior of a single Toio
/// </summary>
public class SingleToioManager : MonoBehaviour
{

    /// <summary>
    /// Visual target to move towards (specified in AR)
    /// </summary>
    [SerializeField]
    private GameObject _target;

    /// <summary>
    /// Flag to set whether to seek
    /// </summary>
    [SerializeField]
    bool _shouldSeek = false;

    /// <summary>
    /// Flag to set whether to spin
    /// </summary>
    [SerializeField]
    bool _shouldSpin = false;

    // Toio space Pose Vector
    [HideInInspector]
    public Vector3 cubePose;

    // Toio references
    CubeManager cubeManager;
    Cube cube;
    

    // Async Start to allow for Toio connect await
    async void Start()
    {
        // Create a CubeManager and connect to the closest Toio
         cubeManager = new CubeManager();
         cube = await cubeManager.SingleConnect();

         // Set behavior flags to default
         _shouldSeek = false;
         _shouldSpin = false;
    }

    void Update()
    {
        if (cubeManager.IsControllable(cube))
            {
                // Spin if the right flags are set
                if(_shouldSpin && !_shouldSeek)
                {
                    cube.Move(10, -10, 200); // First two values control rate of spin
                }

                // Construct pose vector: includes X, Y, and Angle
                cubePose = new Vector3(cube.x, cube.y, cube.angle);
            }   

            // Handles contain the seek method
            foreach (var handle in cubeManager.syncHandles)
            {
                if(_shouldSeek)
                {
                    // Toio: center (250,250), extents (45, 455)
                    // Toio board side dimension: 0.555f
                    // Convert origin space coordinate into Toio space
                    float x = ((_target.transform.localPosition.x / (0.555f / 2f)) * 205) + 250;
                    float y = -1f*(((_target.transform.localPosition.z / (0.555f / 2f)) * 205 - 250));
                    
                    // Create seeking vector - must be Integer Vec2
                    Vector2Int targetCoord = new Vector2Int((int)x, (int)y);                
                    Movement mv = handle.Move2Target(targetCoord).Exec(); // Executes seek as a movement
                }

            }
    }


    /// <summary>
    /// Method to toggle Seek state
    /// </summary>
    /// <param name="state"></param>
    public void ToggleSeek(bool state)
    {
        _shouldSeek = state;
    }

    /// <summary>
    /// Method to toggle spin state
    /// </summary>
    /// <param name="state"></param>
    public void ToggleSpin(bool state)
    {
        _shouldSpin = state;
    }
}
