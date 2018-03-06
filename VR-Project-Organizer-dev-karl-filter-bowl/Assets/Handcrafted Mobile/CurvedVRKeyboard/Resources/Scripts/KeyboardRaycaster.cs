using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CurvedVRKeyboard {

    public class KeyboardRaycaster: KeyboardComponent {

        public GameObject controller; //Should be whatever controller is set as the RayCaster.

        //------Raycasting----
        [SerializeField, HideInInspector]
        private Transform raycastingSource;

        [SerializeField, HideInInspector]
        private GameObject target;

        private float rayLength;
        private Ray ray;
        private RaycastHit hit;
        private LayerMask layer;
        private float minRaylengthMultipler = 1.5f;
        //---interactedKeys---
        private KeyboardStatus keyboardStatus;
        private KeyboardItem keyItemCurrent;

        [SerializeField, HideInInspector]
        private string clickInputName;


        private bool debounce; //The keyboard was firing off several clicks for one click, using this to counteract (see below)


        void Start () {
            keyboardStatus = gameObject.GetComponent<KeyboardStatus>();
            int layerNumber = gameObject.layer;
            layer = 1 << layerNumber;
            debounce = false;
        }

        void Update () {
            // * sum of all scales so keys are never to far
            rayLength = Vector3.Distance(raycastingSource.position, target.transform.position) * (minRaylengthMultipler + 
                 (Mathf.Abs(target.transform.lossyScale.x) + Mathf.Abs(target.transform.lossyScale.y) + Mathf.Abs(target.transform.lossyScale.z)));
            RayCastKeyboard();
        }

        /// <summary>
        /// Check if camera is pointing at any key. 
        /// If it does changes state of key
        /// </summary>
        private void RayCastKeyboard () {
            
            ray = new Ray(raycastingSource.position, raycastingSource.forward);
            if(Physics.Raycast(ray, out hit, rayLength, layer)) { // If any key was hit
                KeyboardItem focusedKeyItem = hit.transform.gameObject.GetComponent<KeyboardItem>();
                if(focusedKeyItem != null) { // Hit may occur on item without script
                    ChangeCurrentKeyItem(focusedKeyItem);
                    keyItemCurrent.Hovering();

#if !UNITY_HAS_GOOGLEVR
                    //former: if(Input.GetButtonDown(clickInputName))                 
                    
                    if (controller.GetComponent<VRTK.VRTK_InteractGrab>().IsGrabButtonPressed())
                    { // If key clicked 
                       

#else
                    if(GvrController.TouchDown) {
#endif
                        //If the user has clicked,

                        //debounce is false, it is not a duplicate click as a result of the update cycle
                        if (debounce == false)
                        {
                            keyItemCurrent.Click();
                            keyboardStatus.HandleClick(keyItemCurrent);
                        }
                        debounce = true;    //set true, because this click has already been used.
                    }
                    else
                    {
                        debounce = false;   //user ended previous click
                    }
                    
                }
            } else if(keyItemCurrent != null) {// If no target hit and lost focus on item
                ChangeCurrentKeyItem(null);
            }
        }

        private void ChangeCurrentKeyItem ( KeyboardItem key ) {
            if(keyItemCurrent != null) {
                keyItemCurrent.StopHovering();
            }
            keyItemCurrent = key;
        }

        //---Setters---
        public void SetRayLength ( float rayLength ) {
            this.rayLength = rayLength;
        }

        public void SetRaycastingTransform ( Transform raycastingSource ) {
            this.raycastingSource = raycastingSource;
        }

        public void SetClickButton ( string clickInputName ) {
            this.clickInputName = clickInputName;
        }

        public void SetTarget ( GameObject target ) {
            this.target = target;
        }
    }
}