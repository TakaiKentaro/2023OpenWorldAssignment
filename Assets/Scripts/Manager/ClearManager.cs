using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    [SerializeField] private GameObject _clearCanvas;
    
    private const int CLEAR = 3;
    private int _clearCount;

    public void CountUp()
    {
        _clearCount++;

        if (_clearCount <= CLEAR)
        {
            _clearCanvas.gameObject.SetActive(true);
        }
    }
}
