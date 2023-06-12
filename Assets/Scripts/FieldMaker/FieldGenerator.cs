using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [Header("シード値")] [SerializeField, Tooltip("シード値X")]
    private float _seedX;

    [SerializeField, Tooltip("シード値Z")] private float _seedZ;

    [Header("マップサイズ")] [SerializeField, Tooltip("幅")]
    private float _width = 50f;

    [SerializeField, Tooltip("深さ")] private float _depth = 50f;

    [SerializeField, Tooltip("高さの最大値")] private float _maxHeight = 10f;

    [SerializeField, Tooltip("起伏の激しさ")] private float _undulation = 15f;

    [SerializeField, Tooltip("マップの大きさ")] private float _mapSize = 1f;

    [SerializeField, Tooltip("Field用Box")] private GameObject _fieldBox;

    [SerializeField] private Material[] _materialArray;

    private GameObject _gameObjectContainer;
    private float _boxScale;
    private FieldBox[,] _fieldArray;

    private void Start()
    {
        InitializeSeeds();
        InitializeFieldArray();
        GenerateTerrain();
    }

    private void InitializeSeeds()
    {
        _seedX = Random.value * 100f;
        _seedZ = Random.value * 100f;
    }

    private void InitializeFieldArray()
    {
        _gameObjectContainer = GameObject.Find("GameObjectContainer");
        _fieldArray = new FieldBox[(int)_width, (int)_depth];
    }

    private void GenerateTerrain()
    {
        _maxHeight = Random.Range(5, 20);
        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);
        _boxScale = _fieldBox.transform.localScale.x;

        for (int x = 0; x < (int)_width; x++)
        {
            for (int z = 0; z < (int)_depth; z++)
            {
                GameObject cube = Instantiate(_fieldBox);
                cube.transform.localPosition = new Vector3(x * _boxScale, 0, z * _boxScale);
                cube.transform.SetParent(transform);
                _fieldArray[x, z] = cube.GetComponent<FieldBox>();

                // 高さ設定
                SetYPosition(cube);
            }
        }
    }

    private void SetYPosition(GameObject cube)
    {
        float y = 0;

        float xSample = (cube.transform.localPosition.x + _seedX) / _undulation;
        float zSample = (cube.transform.localPosition.z + _seedZ) / _undulation;

        float noise = Mathf.PerlinNoise(xSample, zSample);

        y = _maxHeight * noise;

        y = Mathf.Round(y);

        cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, y, cube.transform.localPosition.z);

        //ChangeCubeColor(cube, y);
    }

    private void ChangeCubeColor(GameObject cube, float y)
    {
        if (y > _maxHeight * 0.2f) // 草
        {
            cube.GetComponent<MeshRenderer>().material = _materialArray[0];
        }
        else if (y > _maxHeight * 0f) // 土
        {
            cube.GetComponent<MeshRenderer>().material = _materialArray[1];
        }
        else if (y > _maxHeight * -0.2f) // 石
        {
            cube.GetComponent<MeshRenderer>().material = _materialArray[2];
        }
        else
        {
            cube.GetComponent<MeshRenderer>().material = _materialArray[3];
        }
    }
}