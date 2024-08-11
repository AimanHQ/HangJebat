using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform camerapivotTransform;
        private Transform myTransform;
        private Vector3 cameratransformpoition;
        private LayerMask ignoreLayers;

        public static CameraHandler singleton;
        public float lookspeed = 0.1f;
        public float followspeed = 0.1f;
        public float pivotspeed = 0.03f;

        private float defaultpos;
        private float lookangle;
        private float pivotangle;
        public float minimumpivot = -35;
        public float maximumpivot = 35;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultpos = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }
        
        public void followtarget(float delta)
        {
            Vector3 targetpos = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followspeed);
            myTransform.position = targetpos;
        }

        public void handlecamerarotation(float delta,float mouseX,float mouseY)
        {
            lookangle += (mouseX * lookspeed) / delta;
            pivotangle -= (mouseY * pivotspeed) / delta;
            pivotangle = Mathf.Clamp(pivotangle, minimumpivot, maximumpivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookangle;
            Quaternion targetrotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetrotation;

            rotation = Vector3.zero;
            rotation.x = pivotangle;

            targetrotation = Quaternion.Euler(rotation);
            camerapivotTransform.localRotation = targetrotation;
        }
    }
}