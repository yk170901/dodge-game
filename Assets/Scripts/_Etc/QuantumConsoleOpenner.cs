using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumConsoleOpenner : MonoBehaviour
{
    [SerializeField] private Canvas _quantumConsoleCanvas;

    private void Awake()
    {
        _quantumConsoleCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            _quantumConsoleCanvas.enabled = !_quantumConsoleCanvas.enabled;
        }
    }
}
