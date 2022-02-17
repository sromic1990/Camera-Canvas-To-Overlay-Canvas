using DG.Tweening;
using UnityEngine;

namespace CameraCanvasToOverlayCanvas
{
    public class FlyUiManager : MonoBehaviour, IUiManager
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform flyPositionBlue;
        [SerializeField] private Transform flyPositionGreen;
        [SerializeField] private Ease ease;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private float additionalDelay;

        public void OnFlyableGenerated(GameObject obj, FlyObjectType type, System.Action<GameObject> onObjectReturned)
        {
            FlyObject(obj, type, onObjectReturned, delay);
        }

        public void OnFlyableGenerated(GameObject[] obj, FlyObjectType type, System.Action<GameObject>[] returnObject)
        {
            float startDelay = delay;
            for (int i = 0; i < obj.Length; i++)
            {
                FlyObject(obj[i], type, returnObject[i], startDelay);
                startDelay += additionalDelay;
            }
        }

        private void FlyObject(GameObject obj, FlyObjectType type, System.Action<GameObject> onObjectReturned, float startDelay)
        {
            PrepareObject(obj);
            var position = Vector3.zero;
            switch (type)
            {
                case FlyObjectType.Blue:
                    position = flyPositionBlue.position;
                    break;

                case FlyObjectType.Green:
                    position = flyPositionGreen.position;
                    break;
            }

            obj.transform.DOMove(position, duration).SetEase(ease).SetDelay(startDelay).OnComplete(() => onObjectReturned?.Invoke(obj));
        }

        private void PrepareObject(GameObject obj)
        {
            var pos = GetWorldCanvasPosition(obj.transform);

            obj.transform.SetParent(container);
            obj.transform.localPosition = pos;
            obj.transform.localScale = Vector3.one;
            obj.SetActive(true);
        }

        private Vector3 GetWorldCanvasPosition(Transform t)
        {
            var parent = t.parent;
            Vector3 localPos = t.localPosition;
            Vector3 localScale = t.localScale;

            t.SetParent(null);
            Vector3 pos = t.position;
            t.SetParent(parent);
            t.localPosition = localPos;
            t.localScale = localScale;

            pos.z = 0;

            return pos;
        }
    }
}