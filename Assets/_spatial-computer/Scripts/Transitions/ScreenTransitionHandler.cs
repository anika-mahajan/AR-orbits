using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitionHandler : MonoBehaviour
{

    [SerializeField]
    private Image _canvas;



    private void Awake()
    {
        InitializeToScreen();
    }

    public void InitializeToScreen()
    {
        _canvas.color = Color.black;
    }

    public void Transition(bool state)
    {
        StartCoroutine(TransitionLerp(state));
    }

    IEnumerator TransitionLerp(bool state)
    {
        float time = 0;


        yield return new WaitForSeconds(0.5f);


        Color _startCol = state? Color.black : Color.clear;
        Color _endCol = state? Color.clear : Color.black;


        while (time < 2f)
        {

            _canvas.color = Color.Lerp(_startCol, _endCol, time);

            time += Time.deltaTime;
            yield return null;
        }

        _canvas.color = _endCol;

    }

}
