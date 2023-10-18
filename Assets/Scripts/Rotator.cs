using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;

    private void Update() => transform.Rotate(0, _speed * Time.deltaTime, 0);
}
