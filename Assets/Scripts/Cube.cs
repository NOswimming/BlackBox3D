using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube {

    public GameObject gameObject { get; private set; }

    public Cube(Vector3 position)
    {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.transform.position = position;

    }
}
