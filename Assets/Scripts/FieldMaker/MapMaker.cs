using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private int _fieldSize = 0;
    [SerializeField] private GameObject _fieldGenerator;
    [SerializeField] private MeshMaterialCombiner _meshMaterialCombiner;

    private bool[,] _isArray;

    private void Start()
    {
        _isArray = new bool[_fieldSize, _fieldSize];

        int size = _fieldSize * _fieldSize;

        _meshMaterialCombiner._fieldGeneratorArray = new Transform[size];

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_fieldGenerator);
            go.transform.SetParent(gameObject.transform);
            go.transform.position = FieldPosSet(go);
            _meshMaterialCombiner._fieldGeneratorArray[i] = go.transform;
        }

        _meshMaterialCombiner.OnCombineMaterial();
    }

    private Vector3 FieldPosSet(GameObject go)
    {
        Vector3 pos = go.transform.position;

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int z = 0; z < _fieldSize; z++)
            {
                if (!_isArray[x, z])
                {
                    pos = new Vector3(x * 50, 0, z * 50);
                    _isArray[x, z] = true;
                    return pos;
                }
            }
        }

        return pos;
    }
}