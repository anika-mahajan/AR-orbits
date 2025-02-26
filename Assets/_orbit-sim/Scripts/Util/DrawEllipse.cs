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
        pointPosition = new Vector3(rotatedX + center.x, 0.06f, rotatedY  + center.y);
		// pointPosition = pointQuaternion * pointPosition;
		
		self_lineRenderer.SetPosition(index, pointPosition);		
	}
}