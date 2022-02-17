using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CameraCanvasToOverlayCanvas
{
    [RequireComponent(typeof(IObjectFactory), typeof(IUiManager))]
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private int maxNumberOfFlyObjects = 2;
        [SerializeField] private Card[] cards;
        private IObjectFactory factory;
        private IUiManager uiManager;

        private void Start()
        {
            factory = GetComponent<IObjectFactory>();
            uiManager = GetComponent<IUiManager>();
            foreach (var card in cards)
            {
                card.Init(OnStackPressed);
            }
        }

        private void OnStackPressed(RectTransform spawnPoint, FlyObjectType type)
        {
            int elementCount = Random.Range(1, maxNumberOfFlyObjects);
            GenerateFlights(spawnPoint, type, elementCount);
        }

        private void GenerateFlights(RectTransform spawnPoint, FlyObjectType type, int num)
        {
            (GameObject, Action<GameObject>)[] objs = new (GameObject, Action<GameObject>)[num];
            if (num <= 1)
            {
                var obj = factory.Create(type, spawnPoint);
                uiManager.OnFlyableGenerated(obj.Item1, type, obj.Item2);
            }
            else
            {
                GameObject[] gObjs = new GameObject[num];
                Action<GameObject>[] callBacks = new Action<GameObject>[num];
                for (int i = 0; i < num; i++)
                {
                    var obj = factory.Create(type, spawnPoint);
                    gObjs[i] = obj.Item1;
                    callBacks[i] = obj.Item2;
                }
                uiManager.OnFlyableGenerated(gObjs, type, callBacks);
            }
        }
    }
}