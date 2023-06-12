using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaterialCombiner : MonoBehaviour
{
    public Transform[] _fieldGeneratorArray;

    public void OnCombineMaterial()
    {
        CombineMeshWithMaterial();
    }

    void CombineMeshWithMaterial()
    {
        foreach(var f in _fieldGeneratorArray)
        {
            MeshFilter[] meshFilters = f.GetComponentsInChildren<MeshFilter>();
            MeshRenderer[] meshRenderers = f.GetComponentsInChildren<MeshRenderer>();

            if (meshFilters.Length != meshRenderers.Length)
            {
                return;
            }

            Dictionary<string, Material> matNameDict = new Dictionary<string, Material>();
            Dictionary<string, List<MeshFilter>> matFilterDict = new Dictionary<string, List<MeshFilter>>();
            for (int i = 0; i < meshFilters.Length; i++)
            {
                Material mat = meshRenderers[i].material;
                string matName = mat.name;

                if (!matFilterDict.ContainsKey(matName))
                {
                    List<MeshFilter> filterList = new List<MeshFilter>();
                    matFilterDict.Add(matName, filterList);
                    matNameDict.Add(matName, mat);
                }

                matFilterDict[matName].Add(meshFilters[i]);
            }

            foreach (KeyValuePair<string, List<MeshFilter>> pair in matFilterDict)
            {

                GameObject obj = CreateMeshObj(pair.Key,f);
                obj.transform.SetAsFirstSibling();

                MeshFilter combinedMeshFilter = CheckComponent<MeshFilter>(obj);
                MeshRenderer combinedMeshRenderer = CheckComponent<MeshRenderer>(obj);

                List<MeshFilter> filterList = pair.Value;
                CombineInstance[] combine = new CombineInstance[filterList.Count];

                for (int i = 0; i < filterList.Count; i++)
                {
                    combine[i].mesh = filterList[i].sharedMesh;
                    combine[i].transform = filterList[i].transform.localToWorldMatrix;
                    filterList[i].gameObject.SetActive(false);
                }

                combinedMeshFilter.mesh = new Mesh();
                combinedMeshFilter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                combinedMeshFilter.mesh.CombineMeshes(combine);

                combinedMeshRenderer.material = matNameDict[pair.Key];

                MeshCollider meshCol = CheckComponent<MeshCollider>(obj);
                meshCol.sharedMesh = combinedMeshFilter.mesh;

                f.gameObject.SetActive(true);
            }
        }

        foreach(var i in _fieldGeneratorArray)
        {
            i.transform.position = new Vector3(0,0,0);
        }
    }

    GameObject CreateMeshObj(string matName, Transform f)
    {
        GameObject go = new GameObject();
        go.name = $"CombinedMesh_{matName}";
        go.transform.SetParent(f);
        go.transform.localPosition = Vector3.zero;
        return go;
    }

    T CheckComponent<T>(GameObject obj) where T : Component
    {
        var targetComp = obj.GetComponent<T>();
        if (targetComp == null)
        {
            targetComp = obj.AddComponent<T>();
        }
        return targetComp;
    }
}