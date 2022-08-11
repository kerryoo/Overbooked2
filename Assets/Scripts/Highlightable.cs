using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Highlightable : MonoBehaviour
{
    //we assign all the renderers here through the inspector
    private List<MeshRenderer> _meshes = new List<MeshRenderer>();

    [SerializeField]
    private Color color = Color.yellow;

    //helper list to cache all the materials ofd this object
    private List<Material> _materials;

    public virtual void Awake()
    {
        _materials = new List<Material>();
        CacheMeshRenderers();

        foreach (var renderer in _meshes)
        {
            //A single child-object might have mutliple materials on it
            //that is why we need to all materials with "s"
            _materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    private void CacheMeshRenderers()
    {
        var baseMesh = transform.GetComponent<MeshRenderer>();
        if (baseMesh != null) _meshes.Add(baseMesh);

        CacheMeshRenderersRecursively(transform);
    }

    private void CacheMeshRenderersRecursively(Transform root)
    {
        foreach (Transform child in root)
        {
            var meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                _meshes.Add(meshRenderer);
            }

            CacheMeshRenderersRecursively(child);
        }
    }


    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in _materials)
            {
                //We need to enable the EMISSION
                material.EnableKeyword("_EMISSION");
                //before we can set the color
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in _materials)
            {
                //we can just disable the EMISSION
                //if we don't use emission color anywhere else
                material.DisableKeyword("_EMISSION");
            }
        }

    }

}
