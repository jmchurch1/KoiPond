using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnIn : MonoBehaviour
{

    private Vector3 _endLocation;
    private float _fallAmount = .5f;

    private void FixedUpdate()
    {
        gameObject.transform.position -= new Vector3(0f, _fallAmount, 0f);
    }

    public void SetEndLocation(Vector3 endLocation)
    {
        _endLocation = endLocation;
    }
}
