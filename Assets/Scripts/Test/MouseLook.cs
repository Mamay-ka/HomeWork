using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
   public enum RotationAxis
    {
        MouseXandY,
        MouseX,
        MouseY
    }

    public RotationAxis axis = RotationAxis.MouseXandY;
}
