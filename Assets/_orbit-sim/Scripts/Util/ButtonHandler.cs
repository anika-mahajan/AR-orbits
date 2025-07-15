using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonHandler
{
    GameObject _background;
    GameObject _checkmark;

    bool isActive;
    public void setProperties(GameObject background, GameObject checkmark, bool isActive)
    {
        this._background = background;
        this._checkmark = checkmark;
        this.isActive = isActive;
    }

    public void changeObj(bool state)
    {
        this.isActive = state;
        if (this.isActive)
        {
            this._background.SetActive(false);
            this._checkmark.SetActive(true);
        }
        else
        {
            this._background.SetActive(true);
            this._checkmark.SetActive(false);
        }
    }

}
