using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit {
    // position variables
    double sunPosX;
    double sunPosY;
    double focusPosX;
    double focusPosY;
    double orbiterPosX;
    double orbiterPosY;

    double rotatedOrbiterPosX;
    double rotatedOrbiterPosY;

    double rotatedSunPosX;
    double rotatedSunPosY;

    double rotatedFocusPosX;
    double rotatedFocusPosY;

    double rotatedCenterX;
    double rotatedCenterY;

    // ellipse information variables
    double angle;
    double ellipseW;
    double ellipseH;

    double aUnity;
    double bUnity;

    // angle of true anamoly 
    double theta;

    double v;

    void Start()
    {
        // Initialize local position variables to zero.
        sunPosX = 0.0;
        sunPosY = 0.0;
        focusPosX = 0.0;
        focusPosY = 0.0;
        orbiterPosX = 0.0;
        orbiterPosY = 0.0;

        rotatedOrbiterPosX = 0.0;
        rotatedOrbiterPosY = 0.0;

        angle = 0.0;
        ellipseW = 0.0;
        ellipseH = 0.0;

        aUnity = 0;
        bUnity = 0;

        theta = 0.0;
        v = 0.0;
    }

    public void calculateEllipse() {
        angle = Math.Atan2((sunPosY - focusPosY), (sunPosX - focusPosX));

        rotatedFocusPosX = rotatePosX(focusPosX, focusPosY);
        rotatedFocusPosY = rotatePosY(focusPosX, focusPosY);

        rotatedSunPosX = rotatePosX(sunPosX, sunPosY);
        rotatedSunPosY = rotatePosY(sunPosX, sunPosY);

        rotatedOrbiterPosX = rotatePosX(orbiterPosX, orbiterPosY);
        rotatedOrbiterPosY = rotatePosY(orbiterPosX, orbiterPosY);

        rotatedCenterX = ((rotatedSunPosX - rotatedFocusPosX) / 2) + rotatedFocusPosX;
        rotatedCenterY = ((rotatedSunPosY - rotatedFocusPosY) / 2) + rotatedFocusPosY;
        
        double c = (Math.Sqrt(Math.Pow((rotatedSunPosX - rotatedFocusPosX), 2.0) + Math.Pow((rotatedSunPosY - rotatedFocusPosY), 2.0))) / 2;
    
        // d1 and d2 calculation (distance between orbiter and the focus points)
        double d1 = Math.Sqrt(Math.Pow((rotatedOrbiterPosX - rotatedFocusPosX), 2.0) + Math.Pow((rotatedOrbiterPosY - rotatedFocusPosY), 2.0));
        double d2 = Math.Sqrt(Math.Pow((rotatedOrbiterPosX - rotatedSunPosX), 2.0) + Math.Pow((rotatedOrbiterPosY - rotatedSunPosY), 2.0));

        double d1Unity = Math.Sqrt(Math.Pow(convertToioToUnityX(rotatedOrbiterPosX) - convertToioToUnityX(rotatedFocusPosX), 2.0) + Math.Pow(convertToioToUnityY(rotatedOrbiterPosY) - convertToioToUnityY(rotatedFocusPosY), 2.0));
        double d2Unity = Math.Sqrt(Math.Pow(convertToioToUnityX(rotatedOrbiterPosX) - convertToioToUnityX(rotatedSunPosX), 2.0) + Math.Pow(convertToioToUnityY(rotatedOrbiterPosY) - convertToioToUnityY(rotatedSunPosY), 2.0));
        double cUnity = (Math.Sqrt(Math.Pow(convertToioToUnityX(rotatedSunPosX) - convertToioToUnityX(rotatedFocusPosX), 2.0) + Math.Pow(convertToioToUnityY(rotatedSunPosY) - convertToioToUnityY(rotatedFocusPosY), 2.0))) / 2;
    
        ellipseW = d1 + d2;
        // ellipseH = 2 * (Math.Sqrt(Math.Abs((Math.Pow(ellipseW / 2, 2.0)) - (Math.Pow(c, 2.0)))));

        ellipseH = 2 * (Math.Sqrt(Math.Pow(ellipseW / 2, 2.0) - (Math.Pow(c, 2.0))));

        aUnity = (d1Unity + d2Unity) / 2;
        bUnity = (Math.Sqrt(Math.Pow(aUnity, 2.0) - (Math.Pow(cUnity, 2.0))));

        // Debug.Log("a: " + ellipseW / 2);
        // Debug.Log("b: " + ellipseH / 2);
        double insideSqrt = Math.Pow(ellipseW / 2, 2.0) - (Math.Pow(c, 2.0));
        if(insideSqrt < 0) {
            Debug.LogError("Negative value under square root: " + insideSqrt);
            return;
        }
        // Debug.Log("ellipseH: " + (Math.Pow(ellipseW / 2, 2.0)) - (Math.Pow(c, 2.0)));
    
        // double a = ellipseW / 2;
        // double b = ellipseH / 2;
    
        // double periX = centerX - a * Math.Cos(angle);
        // double periY = centerY - a * Math.Sin(angle);

        // double rotatedPeriX = rotatePosX(periX, periY);
        // double rotatedPeriY = rotatePosY(periX, periY);
    
        // theta = atan2((this.rotated.orbiterPos.y - this.rotated.peri.y), (this.rotated.orbiterPos.x - this.rotated.peri.x));
        theta = Math.Atan2((rotatedOrbiterPosY - rotatedSunPosY), (rotatedOrbiterPosX - rotatedSunPosX));
        // theta = Math.Atan2((rotatedOrbiterPosY), (rotatedOrbiterPosX));
    }

    public void calculateVelocity() {
        double a = ellipseW / 2;
        double b = ellipseH / 2;
        // e = c/a;
        double e = Math.Sqrt(1 - (Math.Pow(b, 2.0) / Math.Pow(a, 2.0)));
        // r = (a*(1-sq(e)))/(1+(e*cos(theta)));
        double r = Math.Sqrt(Math.Pow(rotatedOrbiterPosX - rotatedSunPosX, 2.0) + Math.Pow(rotatedOrbiterPosY - rotatedSunPosY, 2.0));
    
        double G = 6.67430 * Math.Pow(10, -11);
        double M = 1.989 * Math.Pow(10, 6);
        // double h = sqrt(G * M * this.a * (1 - sq(e)));

        // Debug.Log(" r and a value: r=" + r + ", a=" + a);

        // double insideSqrt = (2 / r) - (1 / a);
        // if(insideSqrt < 0) {
        //     Debug.LogError("Negative value under square root: " + insideSqrt);
        //     return;
        // }
    
        v = Math.Sqrt(G * M * ((2 / r) - (1 / a)));
    }

    public void updateOrbiterPos() {
        theta += v;

        double a = ellipseW / 2;
        double b = ellipseH / 2;

        // Calculate the new position of the asteroid based on the angle
        rotatedOrbiterPosX = rotatedCenterX + a * Math.Cos(theta);
        rotatedOrbiterPosY = rotatedCenterY + b * Math.Sin(theta);

        orbiterPosX = rotatedOrbiterPosX * Math.Cos(-1 * angle) + rotatedOrbiterPosY * Math.Sin(-1 * angle);
        orbiterPosY = -1 * rotatedOrbiterPosX * Math.Sin(-1 * angle) + rotatedOrbiterPosY * Math.Cos(-1 * angle);

        // orbiterPosX = rotatedOrbiterPosX;
        // orbiterPosY = rotatedOrbiterPosY;
        // Debug.Log("x: " + orbiterPosX);
    }

    public double rotatePosX(double positionX, double positionY) {
        return positionX * Math.Cos(angle) + positionY * Math.Sin(angle);
    } 

    public double rotatePosY(double positionX, double positionY) {
        return -1 * positionX * Math.Sin(angle) + positionY * Math.Cos(angle);
    }

    public void setOrbiterPos(double positionX, double positionY) {
        orbiterPosX = positionX;
        orbiterPosY = positionY;
    }

    public void setSunPos(double positionX, double positionY) {
        sunPosX = positionX;
        sunPosY = positionY;
    }

    public void setFocusPos(double positionX, double positionY) {
        focusPosX = positionX;
        focusPosY = positionY;
    }
    
    public void setPositions(double orbiterPositionX, double orbiterPositionY, double sunPositionX, double sunPositionY, double focusPositionX, double focusPositionY) {
        setOrbiterPos(orbiterPositionX, orbiterPositionY);
        setSunPos(sunPositionX, sunPositionY);
        setFocusPos(focusPositionX,focusPositionY);
    }

    public double getEllipseWidth() {
        return ellipseW;
    }

    public double getEllipseHeight() {
        return ellipseH;
    }

    public double getRotationAngle() {
        return angle;
    }

    public double getCenterX() {
        return ((sunPosX - focusPosX) / 2) + focusPosX;
    }

    public double getCenterY() {
        return ((sunPosY - focusPosY) / 2) + focusPosY;
    }

    public double getOrbiterPosX() {
        return orbiterPosX;
    }

    public double getOrbiterPosY() {
        return orbiterPosY;
    }

    public double getAUnity() {
        return aUnity;
    }

    public double getBUnity() {
        return bUnity;
    }

    public double convertToioToUnityX(double x) {
        return ((x - 250.0)/205.0) * 0.555 / 2.0;
    }

    public double convertToioToUnityY(double y) {
        return -1 * ((y - 250.0)/205.0) * 0.555 / 2.0;
    }

    public double convertUnityToToioX(double x) {
        return ((x / (0.555 / 2.0)) * 205.0) + 250.0;
    }

    public double convertUnityToToioY(double y) {
        return -1 * (((y / (0.555 / 2.0)) * 205.0) + 250.0);
    }
}