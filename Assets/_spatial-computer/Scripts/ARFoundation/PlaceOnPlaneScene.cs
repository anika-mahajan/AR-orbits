using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedObject"/> is placed
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlaneScene : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Places this object on a plane at the touch location.")]
        GameObject m_PlacedObject;

        [SerializeField]
        [Tooltip("Reference to the AR Camera object.")]
        GameObject ARCamera;

        /// <summary>
        /// Flag to know if the PlacedObject can be placed or moved.
        /// </summary>
        bool isPlacementActive;

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            isPlacementActive = false;
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;


            // Flag to know if the touch is over a UI element.
            bool isOverUI = IsPointOverUIObject(touchPosition);

            // Executes code if touch is over a plane & placement mode is active.
            if (isPlacementActive && !isOverUI && m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                // Set object position to touch position
                m_PlacedObject.transform.position = hitPose.position;

                // Set object rotation to face the camera
                Vector3 lookVector = hitPose.position - ARCamera.transform.position;
                Vector3 turnVector = new Vector3(lookVector.x, 0, lookVector.z);
                m_PlacedObject.transform.rotation = Quaternion.LookRotation(turnVector, Vector3.up);
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;


    /// <summary>
    /// Basic method to check if a touch point is over a UI element.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsPointOverUIObject(Vector2 pos)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = pos;
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }

    /// <summary>
    /// Method to enable or disable placement of the object in AR.
    /// </summary>
    /// <param name="state"></param>
    public void TogglePlacement(bool state)
    {
        isPlacementActive = state;
    }

    }
}
