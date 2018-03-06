using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CAVS.ProjectOrganizer.Project.Filtering;

namespace CAVS.ProjectOrganizer.Project.Aggregations.Spiral
{

    public class SprialPreviewBehavior : MonoBehaviour
    {

        private Filter filter;

        public void SetFilter(Filter filter)
        {
			this.filter = filter;
        }

        public Filter GetFilter()
        {
            return filter;
        }

    }

}