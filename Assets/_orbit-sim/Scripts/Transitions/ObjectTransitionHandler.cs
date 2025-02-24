using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTransitionHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject _arCamera;

    [SerializeField]
    private GameObject _origin;

    [Header("Transition Object")]
    public GameObject TransitionObject;


    [Header("Transforms")]
    public Transform ScreenTransform;
    public Transform WorldTransform;

    private void Awake()
    {
        InitializeToScreen();
    }

    public void InitializeToScreen()
    {
        TransitionObject.transform.parent = _arCamera.transform;
        TransitionObject.transform.localPosition = ScreenTransform.localPosition;
        TransitionObject.transform.localEulerAngles = ScreenTransform.localEulerAngles;
        TransitionObject.transform.localScale = ScreenTransform.localScale;
    }

    public void Transition(bool state)
    {
        StartCoroutine(TransitionLerp(state));
    }

    IEnumerator TransitionLerp(bool state)
    {
        float time = 0;

        TransitionObject.transform.parent = state? _origin.transform : _arCamera.transform;

        yield return new WaitForSeconds(0.5f);

        Vector3 _startPos = TransitionObject.transform.localPosition;
        Vector3 _startRot = TransitionObject.transform.localEulerAngles;
        Vector3 _startScale = TransitionObject.transform.localScale;

        Vector3 _endPos, _endRot, _endScale;

        if(state)
        {
            _endPos = WorldTransform.transform.localPosition;
            _endRot = WorldTransform.transform.localEulerAngles;
            _endScale = WorldTransform.transform.localScale;
        }
        else
        {
            _endPos = ScreenTransform.transform.localPosition;
            _endRot = ScreenTransform.transform.localEulerAngles;
            _endScale = ScreenTransform.transform.localScale;
        }


        while (time < 2f)
        {
            TransitionObject.transform.localPosition = Vector3.Lerp(_startPos, _endPos, time);
            TransitionObject.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(_startRot), Quaternion.Euler(_endRot), time);
            TransitionObject.transform.localScale = Vector3.Lerp(_startScale, _endScale, time);

            time += Time.deltaTime;
            yield return null;
        }


        TransitionObject.transform.localPosition = _endPos;
        TransitionObject.transform.localRotation = Quaternion.Euler(_endRot);
        TransitionObject.transform.localScale = _endScale;

    }

}
