using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the tracked object based on the current position of the Toio.
/// </summary>
public class ToioTrackedObject : MonoBehaviour
{

    /// <summary>
    /// Reference to the in-scene Toio Manager
    /// </summary>
    [SerializeField]
    SingleToioManager _toioManager;

    /// <summary>
    /// Rate of interpolation
    /// </summary>
    [SerializeField]
    float _smoothTime = 0.5f;

    // Local transform holders
    float x, z, angle;

    // Target Vector and Quaternion
    Vector3 _targetPosition;
    Quaternion _targetRotation;


    // Velocity value for SmoothDamp
    private Vector3 _posVelocity = Vector3.zero;
    
    void Start()
    {
        // Initialize local position variables to zero.
        x = 0f;
        z = 0f;
        angle = 0f;
    }

    void Update()
    {
        // Get target position of the tracked object (where the Toio is currently)
        GetTrackedPosition();

        // Apply to current object by interpolation.
        // Modify _smoothTime for different rates of change.
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref _posVelocity, _smoothTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, _smoothTime);
    }

    /// <summary>
    /// Method to convert position and rotation from Toio Coordinates to local coordinates relative to the origin.
    /// </summary>
    public void GetTrackedPosition()
    {
        // Toio: center (250,250), extents (45, 455)
        // Toio board side dimension: 0.555f
        x = ((_toioManager.cubePose.x - 250f)/205f) * 0.555f / 2f;
        z = -1f * ((_toioManager.cubePose.y - 250f)/205f) * 0.555f / 2f;
        
        angle = _toioManager.cubePose.z;
        
        _targetPosition = new Vector3(x, 0, z);
        
        Vector3 _targetEulers = new Vector3(0,angle,0);        
        _targetRotation = Quaternion.Euler(_targetEulers); // Convert to Quaternion to avoid gimbal lock while spinning.
    }


}
