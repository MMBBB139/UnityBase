using System;
using Sisus.Init;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Sample.Scripts
{
    [Service(typeof(MouseTrackingService))]
    public class MouseTrackingService : MonoBehaviour
    {
        [SerializeField] LayerMask layer;

        private Vector2 _mousePosition;
        
        public bool TryGetMouseWallHit(out Vector3 hitPoint)
        {
            _mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.crimson);
            
            bool isHit = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
            
            if (isHit)
            {
                hitPoint = hit.point;
                return true;
            }

            hitPoint = Vector3.zero;
            return false;
        }
    }
}
