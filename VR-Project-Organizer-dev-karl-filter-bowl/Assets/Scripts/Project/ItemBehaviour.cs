using UnityEngine;
using CAVS.ProjectOrganizer.Project;


namespace CAVS.ProjectOrganizer.Project
{

    public class ItemBehaviour : MonoBehaviour
    {

        public Item ToItem()
        {
            return null;
        }

		protected virtual void OnExamineStart()
		{
			Debug.Log ("Examine Started");
		}

		protected virtual void OnExamineStop()
		{
			Debug.Log ("Examine Stopped");
		}

    }

}