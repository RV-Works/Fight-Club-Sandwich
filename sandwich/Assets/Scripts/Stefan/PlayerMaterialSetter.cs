using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] _topPlayerMaterials = new Material[4];
    [SerializeField] private Material[] _bottomPlayerMaterials = new Material[4];
    [SerializeField] private List<SkinnedMeshRenderer> _topPlayerRenderers;
    [SerializeField] private List<MeshRenderer> _topPlayerMeshRenderers;
    [SerializeField] private List<SkinnedMeshRenderer> _bottomPlayerRenderers;

    public void SetMaterial(int id)
    {
        if (id < 0 || id > 4)
            return;

        foreach (SkinnedMeshRenderer renderer in _topPlayerRenderers)
        {
            renderer.material = _topPlayerMaterials[id];
        }

        foreach (MeshRenderer renderer in _topPlayerMeshRenderers)
        {
            renderer.material = _topPlayerMaterials[id];
        }

        foreach (SkinnedMeshRenderer renderer in _bottomPlayerRenderers)
        {
            renderer.material = _bottomPlayerMaterials[id];
        }

    }
}
