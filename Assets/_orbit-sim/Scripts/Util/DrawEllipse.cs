// based on Sonoshee's ellipse drawing code from 
// https://discussions.unity.com/t/draw-an-ellipse-in-unity-3d/94777/3

using UnityEngine;
using System.Collections;

// [ExecuteInEditMode]
[RequireComponent (typeof(LineRenderer))]
public class DrawEllipse : MonoBehaviour 
{

    /// <summary>
    /// Reference to the in-scene Toio Manager
    /// </summary>
    [SerializeField]
    MultiToioManager _toioManager;

	private Vector2 radius = new Vector2(0.1f, 0.2f);
    private Vector2 center = new Vector2(0.1f, 0.2f);
	private float width = 0.01f;
	private float rotationAngle = 0;
	public int resolution = 500;

	private Vector3[] positions;
	private LineRenderer self_lineRenderer;
    
	
	
	void Start()
	{
		// UpdateEllipse();
	}

    public void Update() {
        if(_toioManager.getOrbiterCube() != null && _toioManager.getSunCube() != null && _toioManager.getFocusCube() != null) {
            UpdateEllipseComponents();
            UpdateEllipse();
        }
    }

    public void UpdateEllipseComponents() {

        radius = new Vector2((float) _toioManager.getOrbit().getAUnity(), (float) _toioManager.getOrbit().getBUnity());
        // // center = 
        float centerX = (float) _toioManager.getOrbit().convertToioToUnityX(_toioManager.getOrbit().getCenterX());
        float centerY = (float) _toioManager.getOrbit().convertToioToUnityY(_toioManager.getOrbit().getCenterY());
        // float centerX = (float) _toioManager.getOrbit().convertToioToUnityX(_toioManager.getSunCube().x);
        // float centerY = (float) _toioManager.getOrbit().convertToioToUnityY(_toioManager.getSunCube().y);
        center = new Vector2(centerX, centerY);
        rotationAngle = (float) _toioManager.getOrbit().getRotationAngle();
    }
	
	public void UpdateEllipse()
	{
		if ( self_lineRenderer == null)
			self_lineRenderer = GetComponent<LineRenderer>();
			
		// self_lineRenderer.SetVertexCount (resolution+3);
        self_lineRenderer.positionCount = resolution + 3;
		
		// self_lineRenderer.SetWidth(width, width);
        self_lineRenderer.startWidth = width;
        self_lineRenderer.endWidth = width;
		
		
		AddPointToLineRenderer(0f, 0);
		for (int i = 1; i <= resolution + 1; i++) 
		{
			AddPointToLineRenderer((float)i / (float)(resolution) * 2.0f * Mathf.PI, i);
		}
		AddPointToLineRenderer(0f, resolution + 2);
	}
	
	void AddPointToLineRenderer(float angle, int index)
	{
		// Quaternion pointQuaternion = Quaternion.AngleAxis (rotationAngle, Vector3.up);
		Vector3 pointPosition;
		
		// pointPosition = new Vector3(radius.x * Mathf.Cos (angle), radius.y * Mathf.Sin (angle), 0.0f);
        // float x = radius.x * Mathf.Cos (angle) + center.x;
        // float y = radius.y * Mathf.Sin (angle) + center.y;
        float x = radius.x * Mathf.Cos (angle);
        float y = radius.y * Mathf.Sin (angle);

        // Rotate point by rotationAngle
        float rotatedX = x * Mathf.Cos(rotationAngle) + y * Mathf.Sin(rotationAngle);
        float rotatedY = -1 * x * Mathf.Sin(rotationAngle) + y * Mathf.Cos(rotationAngle);

        // pointPosition = new Vector3(radius.x * Mathf.Cos (angle) + center.x, 0.0f, radius.y * Mathf.Sin (angle) + center.y);
        pointPosition = new Vector3(rotatedX + center.x, 0.135f, rotatedY  + center.y);
		// pointPosition = pointQuaternion * pointPosition;
		
		self_lineRenderer.SetPosition(index, pointPosition);		
	}
}


// using UnityEngine;
// using System;
// using System.Threading.Tasks;

// public class DrawEllipse : MonoBehaviour
// {
//     /// <summary>
//     /// Reference to the in-scene Toio Manager
//     /// </summary>
//     [SerializeField]
//     MultiToioManager _toioManager;

//     public int segments = 100;
//     double a = 0.0f; // Semi-major axis
//     double b = 0.0f; // Semi-minor axis
//     Vector3 center = Vector3.zero; // Center of the ellipse
//     double rotationAngle = 0f; // Rotation in radians
//     public LineRenderer lineRenderer;

//     void Start()
//     {
//         if (lineRenderer == null) {
//             lineRenderer = GetComponent<LineRenderer>();
//         }

//         lineRenderer.positionCount = segments + 1;
//     }

//     public void Draw()
//     {
//         a = _toioManager.getOrbit().getEllipseWidth() / 2;
//         b = _toioManager.getOrbit().getEllipseHeight() / 2;

//         center = new Vector3((float) _toioManager.getOrbit().getCenterX(), 0.1f, (float) _toioManager.getOrbit().getCenterY());

//         rotationAngle = _toioManager.getOrbit().getRotationAngle();

//         double angle = 0.0;
//         for (int i = 0; i <= segments; i++)
//         {
//             // Calculate base ellipse point
//             double x = a * Math.Cos(angle);
//             double y = b * Math.Sin(angle);

//             // Rotate point by rotationAngle
//             double rotatedX = x * Math.Cos(rotationAngle) - y * Math.Sin(rotationAngle);
//             double rotatedY = x * Math.Sin(rotationAngle) + y * Math.Cos(rotationAngle);

//             // Set the point position with center offset
//             lineRenderer.SetPosition(i, new Vector3((float)(center.x + rotatedX), center.y, (float)(center.y + rotatedY)));
//             angle += 2 * Math.PI / segments;
//         }
//     }

//     void Update() {
//         if (_toioManager.getOrbit() != null) {
//             Draw();
//         }
//     }

//     public void SetCenter(Vector3 newCenter)
//     {
//         center = newCenter;
//         Draw();
//     }

//     public void SetRotation(float newRotation)
//     {
//         rotationAngle = newRotation;
//         Draw();
//     }
// }
