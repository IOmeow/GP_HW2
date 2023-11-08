using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Utility
{
    public static void AddColliderAroundChildren(GameObject assetModel)
    {
        var pos = assetModel.transform.localPosition;
        var rot = assetModel.transform.localRotation;
        var scale = assetModel.transform.localScale;

        // need to clear out transforms while encapsulating bounds
        assetModel.transform.localPosition = Vector3.zero;
        assetModel.transform.localRotation = Quaternion.identity;
        assetModel.transform.localScale = Vector3.one;

        // start with root object's bounds
        var bounds = new Bounds(Vector3.zero, Vector3.zero);
        if (assetModel.transform.TryGetComponent<Renderer>(out var mainRenderer))
        {
            // as mentioned here https://forum.unity.com/threads/what-are-bounds.480975/
            // new Bounds() will include 0,0,0 which you may not want to Encapsulate
            // because the vertices of the mesh may be way off the model's origin
            // so instead start with the first renderer bounds and Encapsulate from there
            bounds = mainRenderer.bounds;
        }

        var descendants = assetModel.GetComponentsInChildren<Transform>();
        foreach (Transform desc in descendants)
        {
            if (desc.TryGetComponent<Renderer>(out var childRenderer))
            {
                // use this trick to see if initialized to renderer bounds yet
                // https://answers.unity.com/questions/724635/how-does-boundsencapsulate-work.html
                if (bounds.extents == Vector3.zero)
                    bounds = childRenderer.bounds;
                bounds.Encapsulate(childRenderer.bounds);
            }
        }

        var boxCol = assetModel.AddComponent<BoxCollider>();
        boxCol.center = bounds.center - assetModel.transform.position;
        boxCol.size = bounds.size;
        // restore transforms
        assetModel.transform.localPosition = pos;
        assetModel.transform.localRotation = rot;
        assetModel.transform.localScale = scale;
    }
}

