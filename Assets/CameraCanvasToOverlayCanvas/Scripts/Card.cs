using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CameraCanvasToOverlayCanvas
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private RectTransform spawnPoint;
        [SerializeField] private FlyObjectType holderType;

        private Button button;

        private void OnEnable()
        {
            button = GetComponent<Button>();
        }

        public void Init(System.Action<RectTransform, FlyObjectType> onPressed)
        {
            button.onClick.AddListener(() =>
            {
                onPressed?.Invoke(spawnPoint, holderType);
                Pump();
            });
        }

        private void Pump()
        {
            transform
                .DOScale(0.89f, 0.08f)
                .SetLoops(1, LoopType.Yoyo)
                .OnComplete(() => transform.localScale = Vector3.one);
        }
    }
}