using UnityEngine;

namespace CameraCanvasToOverlayCanvas
{
    public interface IObjectFactory
    {
        (GameObject, System.Action<GameObject>) Create(FlyObjectType type, Transform parent);
    }

    public enum FlyObjectType
    {
        Blue,
        Green
    }

    public class FlyingObjectFactory : MonoBehaviour, IObjectFactory
    {
        [SerializeField] private GameObject blueObjPrefab;
        [SerializeField] private GameObject greenObjPrefab;

        [SerializeField] private float maxDistance;

        public (GameObject, System.Action<GameObject>) Create(FlyObjectType type, Transform parent)
        {
            GameObject gObj = null;

            switch (type)
            {
                case FlyObjectType.Blue:
                    gObj = Instantiate(blueObjPrefab, parent);
                    break;

                case FlyObjectType.Green:
                    gObj = Instantiate(greenObjPrefab, parent);
                    break;
            }

            if (gObj != null)
            {
                gObj.transform.localPosition = Vector3.zero;
                Vector3 position = gObj.transform.position;
                Vector3 randomOffset = Random.insideUnitCircle * maxDistance;
                Vector3 newPosition = position + randomOffset;
                gObj.transform.position = newPosition;

                gObj.SetActive(false);
            }

            return (gObj, OnObjectReturned);
        }

        private void OnObjectReturned(GameObject gObj)
        {
            Destroy(gObj);
        }
    }
}