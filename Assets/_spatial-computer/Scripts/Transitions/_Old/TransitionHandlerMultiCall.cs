using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHandlerMultiCall : MonoBehaviour
{

    public GameObject Camera;
    public GameObject Origin;
    public Image Canvas;

    [Header("Objects")]
    public GameObject Motor;
    public GameObject Video;

    public GameObject Vidio;

    [Header("Motor")]
    public Vector3 MotorPos1; // 0, 0, 0
    public Vector3 MotorRot1; // 0, 0, 0
    public Vector3 MotorScale1; // 1, 1, 1

    public Vector3 MotorPos2;
    public Vector3 MotorRot2;
    public Vector3 MotorScale2;

    [Header("Video")]
    public Vector3 VidPos1; // 
    public Vector3 VidRot1; //
    public Vector3 VidScale1; //

    public Vector3 VidPos2;
    public Vector3 VidRot2;
    public Vector3 VidScale2;

    [Header("Vidio")]
    public Vector3 VidiPos1; // 
    public Vector3 VidiRot1; //
    public Vector3 VidiScale1; //

    public Vector3 VidiPos2;
    public Vector3 VidiRot2;
    public Vector3 VidiScale2;


    private void Awake()
    {
        Motor.transform.parent = Camera.transform;
        Motor.transform.localPosition = MotorPos1;
        Motor.transform.localEulerAngles = MotorRot1;
        Motor.transform.localScale = MotorScale1;

        Video.transform.parent = Camera.transform;
        Video.transform.localPosition = VidPos1;
        Video.transform.localEulerAngles = VidRot1;
        Video.transform.localScale = VidScale1;

        Vidio.transform.parent = Camera.transform;
        Vidio.transform.localPosition = VidiPos1;
        Vidio.transform.localEulerAngles = VidiRot1;
        Vidio.transform.localScale = VidiScale1;

        Canvas.color = Color.black;

    }

    public void Transition(bool state)
    {
        if(state)
        {
            StartCoroutine(LerpOut());
        }
        else
        {
            StartCoroutine(LerpIn());
        }
    }

    IEnumerator LerpOut()
    {
        float time = 0;


        //Motor.transform.parent = null;
        //Video.transform.parent = null;

        //yield return new WaitForSeconds(0.5f);

        Motor.transform.parent = Origin.transform;
        Video.transform.parent = Origin.transform;
        Vidio.transform.parent = Origin.transform;


        yield return new WaitForSeconds(0.5f);

        
        Vector3 motorStartPos = Motor.transform.localPosition;
        Vector3 motorStartRot = Motor.transform.localEulerAngles;
        Vector3 motorStartScale = Motor.transform.localScale;


        Vector3 vidStartPos = Video.transform.localPosition;
        Vector3 vidStartRot = Video.transform.localEulerAngles;
        Vector3 vidStartScale = Video.transform.localScale;

        Vector3 vidiStartPos = Vidio.transform.localPosition;
        Vector3 vidiStartRot = Vidio.transform.localEulerAngles;
        Vector3 vidiStartScale = Vidio.transform.localScale;



        while (time < 2f)
        {
            Canvas.color = Color.Lerp(Color.black, Color.clear, time / 2f);

            Motor.transform.localPosition = Vector3.Lerp(motorStartPos, MotorPos2, time / 2f);
            Motor.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(motorStartRot), Quaternion.Euler(MotorRot2), time / 2f);

            //Motor.transform.localEulerAngles = Vector3.Lerp(motorStartRot, MotorRot2, time / 2f);
            Motor.transform.localScale = Vector3.Lerp(motorStartScale, MotorScale2, time / 2f);

            Video.transform.localPosition = Vector3.Lerp(vidStartPos, VidPos2, time);
            Video.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(vidStartRot), Quaternion.Euler(VidRot2), time);

            //Video.transform.localEulerAngles = Vector3.Lerp(vidStartRot, VidRot2, time / 2f);
            Video.transform.localScale = Vector3.Lerp(vidStartScale, VidScale2, time);

            Vidio.transform.localPosition = Vector3.Lerp(vidiStartPos, VidiPos2, time);
            Vidio.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(vidiStartRot), Quaternion.Euler(VidiRot2), time);

            //Video.transform.localEulerAngles = Vector3.Lerp(vidStartRot, VidRot2, time / 2f);
            Vidio.transform.localScale = Vector3.Lerp(vidiStartScale, VidiScale2, time);

            time += Time.deltaTime;
            yield return null;
        }

        Canvas.color = Color.clear;

        Motor.transform.localPosition = MotorPos2;
        Motor.transform.localEulerAngles = MotorRot2;
        Motor.transform.localScale = MotorScale2;

        Video.transform.localPosition = VidPos2;
        Video.transform.localEulerAngles = VidRot2;
        Video.transform.localScale = VidScale2;

        Vidio.transform.localPosition = VidiPos2;
        Vidio.transform.localEulerAngles = VidiRot2;
        Vidio.transform.localScale = VidiScale2;



    }

    IEnumerator LerpIn()
    {
        float time = 0;

        //Motor.transform.parent = null;
        //Video.transform.parent = null;

        //yield return new WaitForSeconds(0.5f);

        Motor.transform.parent = Camera.transform;
        Video.transform.parent = Camera.transform;
        Vidio.transform.parent = Camera.transform;


        yield return new WaitForSeconds(0.5f);

        Vector3 motorStartPos = Motor.transform.localPosition;
        Vector3 motorStartRot = Motor.transform.localEulerAngles;
        Vector3 motorStartScale = Motor.transform.localScale;

        Vector3 vidStartPos = Video.transform.localPosition;
        Vector3 vidStartRot = Video.transform.localEulerAngles;
        Vector3 vidStartScale = Video.transform.localScale;

        Vector3 vidiStartPos = Vidio.transform.localPosition;
        Vector3 vidiStartRot = Vidio.transform.localEulerAngles;
        Vector3 vidiStartScale = Vidio.transform.localScale;

        while (time < 2f)
        {
            Canvas.color = Color.Lerp(Color.clear, Color.black, time / 2f);

            Motor.transform.localPosition = Vector3.Lerp(motorStartPos, MotorPos1, time);
            Motor.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(motorStartRot), Quaternion.Euler(MotorRot1), time);
            //Motor.transform.localEulerAngles = Vector3.Lerp(motorStartRot, MotorRot1, time / 2f);
            Motor.transform.localScale = Vector3.Lerp(motorStartScale, MotorScale1, time);

            Video.transform.localPosition = Vector3.Lerp(vidStartPos, VidPos1, time / 2f);
            Video.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(vidStartRot), Quaternion.Euler(VidRot1), time / 2f);

            //Video.transform.localEulerAngles = Vector3.Lerp(vidStartRot, VidRot1, time / 2f);
            Video.transform.localScale = Vector3.Lerp(vidStartScale, VidScale1, time / 2f);

            Vidio.transform.localPosition = Vector3.Lerp(vidiStartPos, VidiPos1, time / 2f);
            Vidio.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(vidiStartRot), Quaternion.Euler(VidiRot1), time / 2f);

            //Video.transform.localEulerAngles = Vector3.Lerp(vidStartRot, VidRot1, time / 2f);
            Vidio.transform.localScale = Vector3.Lerp(vidiStartScale, VidiScale1, time / 2f);

            time += Time.deltaTime;
            yield return null;
        }

        Canvas.color = Color.black;

        Motor.transform.localPosition = MotorPos1;
        Motor.transform.localEulerAngles = MotorRot1;
        Motor.transform.localScale = MotorScale1;

        Video.transform.localPosition = VidPos1;
        Video.transform.localEulerAngles = VidRot1;
        Video.transform.localScale = VidScale1;
    }

}
