using System.Collections;
using System.Collections.Generic;

namespace CAVS.ProjectOrganizer.Project.Filtering
{

    /// <summary>
    /// An aggregation of multiple filters, an easy way to apply multiple filters
    /// to a list of items.
    /// </summary>
    public class AggregateFilter : Filter
    {

        private Filter[] filters;

        public AggregateFilter(Filter[] filters)
        {
            this.filters = filters;
        }

        
        public override bool FilterItem(Item item)
        {
            bool passed = true;
            for (int filterIndex = 0; filterIndex < filters.Length; filterIndex++)
            {
                passed = filters[filterIndex].FilterItem(item);
                if (!passed)
                {
                    return false;
                }
            }
            return true;
        }

        public Dictionary<Filter, bool>[] FiltersPassed(Item[] unfilteredItems)
        {
            if (unfilteredItems == null)
            {
                return null;
            }

            Dictionary<Filter, bool>[] applied = new Dictionary<Filter, bool>[unfilteredItems.Length];
            for (int itemCount = 0; itemCount < unfilteredItems.Length; itemCount++)
            {
                applied[itemCount] = new Dictionary<Filter, bool>();
                for (int filterIndex = 0; filterIndex < filters.Length; filterIndex++)
                {
                    applied[itemCount].Add(filters[filterIndex], filters[filterIndex].FilterItem(unfilteredItems[itemCount]));
                }
            }
            return applied;
        }

    }

}