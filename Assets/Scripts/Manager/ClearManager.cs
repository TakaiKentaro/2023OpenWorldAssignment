using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    [SerializeField] private GameObject _clearCanvas;
    [SerializeField] private RawImage[] _sprites = new RawImage[3];
    private const int CLEAR = 3;
    private int _clearCount;
    private bool[] _isCheck = new bool[3];

    public void CountUp(int i)
    {
        _sprites[i].color = new Color(255, 255, 255, 1);
        _isCheck[i] = true;
        bool check = false;
        foreach (var s in _isCheck)
        {
            if (!s)
            {
                check = true;
            }
        }

        if (!check)
        {
            _clearCanvas.gameObject.SetActive(true);
        }
    }
}