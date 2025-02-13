﻿using UnityEngine;
using System.Collections;

public class RotationTransformation : Transformation {

    public Vector3 rotation;

    public override Vector3 Apply(Vector3 point)
    {
        float radZ = rotation.z * Mathf.Deg2Rad;
        float sinZ = Mathf.Sin(radZ);
        float cosZ = Mathf.Cos(radZ);

        //point.x *= sinZ;
       // point.y *= cosZ;

        return new Vector3(
            point.x * cosZ - point.y * sinZ,
            point.x * sinZ + point.y * cosZ,
            point.z
        );
        
    }
}
