using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 5f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speedOfSpin * Time.deltaTime));
    }
}
