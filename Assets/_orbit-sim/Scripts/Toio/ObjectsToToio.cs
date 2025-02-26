using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moon by Poly by Google [CC-BY] (https://creativecommons.org/licenses/by/3.0/) via Poly Pizza (https://poly.pizza/m/9OPocAqXM0u)
// Sun with beams by Andrew McHugh [CC-BY] (https://creativecommons.org/licenses/by/3.0/) via Poly Pizza (https://poly.pizza/m/6fIXQtUSXy4)

/// <summary>
/// Moves the tracked object based on the current position of the Toio.
/// </summary>
public class ObjectsToToio : MonoBehaviour
{

    /// <summary>
    /// Reference to the in-scene Toio Manager
    /// </summary>
    [SerializeField]
    MultiToioManager _toioManager;

    /// <summary>
    /// Visual target to show planet movement
    /// </summary>
    [SerializeField]
    private GameObject _orbiter;

    /// <summary>
    /// Visual target to show planet movement
    /// </summary>
    [SerializeField]
    private GameObject _sun;

    /// <summary>
    /// Visual target to show planet movement
    /// </summary>
    [SerializeField]
    private GameObject _focus;

    /// <summary>
    /// Rate of interpolation
    /// </summary>
    [SerializeField]
    float _smoothTime = 0.2f;

    // Local transform holders
    float x, z, angle;

    // Target Vector and Quaternion
    Vector3 _targetPosition;
    Quaternion _targetRotation;

    Vector3 _targetOrbiterPosition;
    Quaternion _targetOrbiterRotation;

    Vector3 _targetSunPosition;
    Quaternion _targetSunRotation;

    Vector3 _targetFocusPosition;
    Quaternion _targetFocusRotation;


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
        // GetTrackedPosition();
        // GetTrackedOrbiterPosition();
        // GetTrackedSunPosition();
        // GetTrackedFocusPosition();

        // Apply to current object by interpolation.
        // Modify _smoothTime for different rates of change.
        // transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPosition, ref _posVelocity, _smoothTime);
        // transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, _smoothTime);

        if (_toioManager.getOrbiterCube() != null && _toioManager.getSunCube() != null && _toioManager.getFocusCube() != null) {
            GetTrackedOrbiterPosition();
            GetTrackedSunPosition();
            GetTrackedFocusPosition();

            _orbiter.transform.position = Vector3.SmoothDamp(_orbiter.transform.position, _targetOrbiterPosition, ref _posVelocity, _smoothTime);
            _orbiter.transform.rotation = Quaternion.Slerp(_orbiter.transform.rotation, _targetOrbiterRotation, _smoothTime);

            // _sun.transform.position = Vector3.SmoothDamp(_sun.transform.position, _targetSunPosition, ref _posVelocity, _smoothTime);
            // _sun.transform.rotation = Quaternion.Slerp(_sun.transform.rotation, _targetSunRotation, _smoothTime);

            _sun.transform.position =  _targetSunPosition;
            _sun.transform.rotation = _targetSunRotation;

            // _focus.transform.localPosition = Vector3.SmoothDamp(_focus.transform.localPosition, _targetFocusPosition, ref _posVelocity, _smoothTime);
            // _focus.transform.localRotation = Quaternion.Slerp(_focus.transform.localRotation, _targetFocusRotation, _smoothTime);

            _focus.transform.position =  _targetFocusPosition;
            _focus.transform.rotation = _targetFocusRotation;
        }
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

    /// <summary>
    /// Method to convert position and rotation from Toio Coordinates to local coordinates relative to the origin.
    /// </summary>
    public void GetTrackedOrbiterPosition()
    {
        // Toio: center (250,250), extents (45, 455)
        // Toio board side dimension: 0.555f
        x = ((_toioManager.getOrbiterCube().x - 250f)/205f) * 0.555f / 2f;
        z = -1f * ((_toioManager.getOrbiterCube().y - 250f)/205f) * 0.555f / 2f;
        
        angle = _toioManager.getOrbiterCube().angle;
        
        _targetOrbiterPosition = new Vector3(x, 0.1096f, z);
        
        Vector3 _targetEulers = new Vector3(0,angle,0);        
        _targetOrbiterRotation = Quaternion.Euler(_targetEulers); // Convert to Quaternion to avoid gimbal lock while spinning.
    }

    /// <summary>
    /// Method to convert position and rotation from Toio Coordinates to local coordinates relative to the origin.
    /// </summary>
    public void GetTrackedSunPosition()
    {
        // Toio: center (250,250), extents (45, 455)
        // Toio board side dimension: 0.555f
        x = ((_toioManager.getSunCube().x - 250f)/205f) * 0.555f / 2f;
        z = -1f * ((_toioManager.getSunCube().y - 250f)/205f) * 0.555f / 2f;
        Debug.Log("x: " + x + " z: " + z);
        Debug.Log("target: " + _targetSunPosition);
        Debug.Log("current: " + _sun.transform.position);
        
        angle = _toioManager.getSunCube().angle;
        
        _targetSunPosition = new Vector3(x, 0.1353f, z);
        
        Vector3 _targetEulers = new Vector3(0,angle,0);        
        _targetSunRotation = Quaternion.Euler(_targetEulers); // Convert to Quaternion to avoid gimbal lock while spinning.
    }

    /// <summary>
    /// Method to convert position and rotation from Toio Coordinates to local coordinates relative to the origin.
    /// </summary>
    public void GetTrackedFocusPosition()
    {
        // Toio: center (250,250), extents (45, 455)
        // Toio board side dimension: 0.555f
        x = ((_toioManager.getFocusCube().x - 250f)/205f) * 0.555f / 2f;
        z = -1f * ((_toioManager.getFocusCube().y - 250f)/205f) * 0.555f / 2f;
        
        angle = _toioManager.getFocusCube().angle;
        
        _targetFocusPosition = new Vector3(x, 0.1345f, z);
        
        Vector3 _targetEulers = new Vector3(0,angle,0);        
        _targetFocusRotation = Quaternion.Euler(_targetEulers); // Convert to Quaternion to avoid gimbal lock while spinning.
    }


}
