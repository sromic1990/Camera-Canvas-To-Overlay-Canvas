using System;
using UnityEngine;

namespace CameraCanvasToOverlayCanvas
{
    public interface IUiManager
    {
        void OnFlyableGenerated(GameObject obj, FlyObjectType type, Action<GameObject> returnObject);
        void OnFlyableGenerated(GameObject[] obj, FlyObjectType type, Action<GameObject>[] returnObject);
    }
}