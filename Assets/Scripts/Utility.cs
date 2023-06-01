using UnityEngine;

namespace ProyectM2.Assets.Scripts
{
    public static class Utility
    {
        public static GameObject GetClosestObjectWithTag(Vector3 position, string tag)
        {
            var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            GameObject closestObject = null;
            var closestDistance = Mathf.Infinity;
            foreach (var obj in objectsWithTag)
            {
                var distance = (obj.transform.position - position).magnitude;
                if (distance < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = distance;
                }
            }

            return closestObject;
        }
        
        public static bool CheckNierObjects(Transform origin, float distance, LayerMask layerMask, GameObject objectExcluded)
        {
            var ray = new Ray(origin.position, origin.forward);
            RaycastHit _hitInfo;
            if (Physics.Raycast(ray, out _hitInfo, distance, layerMask))
            {
                return _hitInfo.transform.gameObject != objectExcluded;
            }

            return false;
        }
    }
}