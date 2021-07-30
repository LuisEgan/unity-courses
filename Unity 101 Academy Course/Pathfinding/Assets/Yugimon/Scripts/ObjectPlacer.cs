using System;
using UnityEngine;
using UnityEngine.AI;

namespace Yugimon.Scripts
{
    public class ObjectPlacer : DefaultTrackableEventHandler
    {
        [SerializeField] private GameObject placeholderPrefab;
        [SerializeField] private Transform placeButtonTransform;
        [SerializeField] private GameObject objectPrefab;

        private GameObject _placeholderObject;
        private Camera _mainCamera;

        private void Awake()
        {
            _placeholderObject = Instantiate(placeholderPrefab);
            _placeholderObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();

            _mainCamera = Camera.main;
        }

        private void Update()
        {
            NavMeshHit hitInfo;
            bool isObjectReachable = NavMesh.SamplePosition(
                transform.position,
                out hitInfo,
                1000,
                NavMesh.AllAreas);

            if (isObjectReachable)
            {
                _placeholderObject.transform.position = hitInfo.position;

                // Place button above the target that places the object
                Vector3 pivotPoint = _placeholderObject.transform.position;
                Vector3 offset = Vector3.up * 100;
                placeButtonTransform.position = _mainCamera.WorldToScreenPoint(pivotPoint) + offset;
            }
        }

        protected override void OnTrackingFound()
        {
            _placeholderObject.SetActive(true);
        }

        protected override void OnTrackingLost()
        {
            _placeholderObject.SetActive(false);
        }

        public void PlaceObject()
        {
            Instantiate(objectPrefab, _placeholderObject.transform.position, _placeholderObject.transform.rotation);
        }
    }
}