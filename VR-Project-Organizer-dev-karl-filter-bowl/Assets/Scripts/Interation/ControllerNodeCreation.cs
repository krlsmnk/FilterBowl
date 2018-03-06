using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CAVS.ProjectOrganizer.Interation
{

    /// <summary>
    /// Detects collision between controllers, and at the end of the timer, creates a node
    /// </summary>
    public class ControllerNodeCreation : MonoBehaviour
    {

        /// <summary>
        /// node to be instantiated
        /// </summary>
        [SerializeField]
        private GameObject nodeToCreate;

        [SerializeField]
        private GameObject leftController;

        [SerializeField]
        private GameObject rightController;

        /// <summary>
        /// Amount of time until node is created
        /// </summary>
        [SerializeField]
        private float timeRequiredToCreateNode;

        /// <summary>
        /// Proximity between controllers
        /// </summary>
        [SerializeField]
        private float distanceLimit;

        private float timer;

        /// <summary>
        /// How long we want to wait until we can create the next item
        /// </summary>
        [SerializeField, Range(1, 10)]
        private float coolDown;

        // OnCollision not working
        void Update()
        {
            // Judge controller distance
            if (Vector3.Distance(leftController.transform.position, rightController.transform.position) < distanceLimit)
            {
                timer += Time.deltaTime;

                if (timer >= (timer / 2))
                {
                    VRTK.VRTK_ControllerHaptics.TriggerHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(leftController), 0.5f);   //Vibrate controllers at half strength (0 < x < 1)
                    VRTK.VRTK_ControllerHaptics.TriggerHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(rightController), 0.5f);
                }
                if (timer >= timeRequiredToCreateNode)
                {
                    //------------------NODE CREATION---------------------------//
                    nodeToCreate.transform.position = (leftController.transform.position + rightController.transform.position) / 2; //midpoint between the two controllers

                    // Node created in front of controllers, in the same direction
                    nodeToCreate.transform.Translate(Vector3.forward, leftController.transform);
                    nodeToCreate.transform.eulerAngles = new Vector3(0, leftController.transform.eulerAngles.y, 0);

                    Instantiate(nodeToCreate);

                    timer = -Mathf.Abs(coolDown);
                }
            }
            else
            {
                // Reset timer when pulled apart
                timer = 0;
                VRTK.VRTK_ControllerHaptics.CancelHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(leftController));   //Cancel vibration when pulled apart
                VRTK.VRTK_ControllerHaptics.CancelHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(rightController));
            }
        }


    }

}