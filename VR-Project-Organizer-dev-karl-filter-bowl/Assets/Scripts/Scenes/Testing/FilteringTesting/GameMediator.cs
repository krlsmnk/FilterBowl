using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CAVS.ProjectOrganizer.Project;
using CAVS.ProjectOrganizer.Project.Filtering;
using CAVS.ProjectOrganizer.Project.Aggregations.Spiral;

namespace CAVS.ProjectOrganizer.Scenes.Testing.FilteringTesting
{

    public class GameMediator : MonoBehaviour
    {

        protected Item[] allItems;

        protected List<Filter> appliedFilters;

        private GameObject currentPalace;

        // Use this for initialization
        void Start()
        {
            appliedFilters
                = new List<Filter>();
            allItems = ProjectFactory.buildItemsFromCSV("CarData.csv");
            Filter[] filters = new Filter[]{
                new NumberFilter("Year",  NumberFilter.Operator.GreaterThan, 1999),  
                new StringFilter("Class", StringFilter.Operator.Equal, "Sport car"),
                new StringFilter("Cylinder", StringFilter.Operator.Equal, "V6")
            };
            // Item spirals are created here.
            new ItemSpiral(allItems, filters[0]).BuildPreview(new Vector3(2, 0, -3));  
            new ItemSpiral(allItems, filters[1]).BuildPreview(new Vector3(2, 0, -2));
            new ItemSpiral(allItems, filters[2]).BuildPreview(new Vector3(2, 0, -1));
        }


        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            SprialPreviewBehavior preview = other.gameObject.GetComponent<SprialPreviewBehavior>();
            if (preview != null)
            {
                appliedFilters.Add(preview.GetFilter());
                displayPalace();
            }
        }

        /// <summary>
        /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerExit(Collider other)
        {
            SprialPreviewBehavior preview = other.gameObject.GetComponent<SprialPreviewBehavior>();
            if (preview != null)
            {
                appliedFilters.Remove(preview.GetFilter());
                displayPalace();
            }
        }

        protected virtual void displayPalace()
        {
            if (currentPalace != null)
            {
                Destroy(currentPalace);
            }
            currentPalace = new ItemSpiral(allItems, new AggregateFilter(appliedFilters.ToArray())).BuildPalace();
        }

    }

}