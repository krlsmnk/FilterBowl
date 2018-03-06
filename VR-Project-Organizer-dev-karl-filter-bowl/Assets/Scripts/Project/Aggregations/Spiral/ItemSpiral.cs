using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CAVS.ProjectOrganizer.Project.Filtering;

namespace CAVS.ProjectOrganizer.Project.Aggregations.Spiral
{

    public class ItemSpiral
    {

        private AggregateFilter filter;

        private Item[] itemsToDisplay;

        private Func<Item, Dictionary<Filter, bool>, ItemBehaviour> itemBuilder;

        public ItemSpiral(Item[] itemsToDisplay, Filter filter)
        {
            this.itemsToDisplay = itemsToDisplay;
            this.filter = new AggregateFilter(new Filter[] { filter });
            this.itemBuilder = null;
        }

        public ItemSpiral(Item[] itemsToDisplay, AggregateFilter filter)
        {
            this.itemsToDisplay = itemsToDisplay;
            this.filter = filter;
            this.itemBuilder = null;
        }

        public ItemSpiral(Item[] itemsToDisplay, AggregateFilter filter, Func<Item, Dictionary<Filter, bool>, ItemBehaviour> itemBuilder)
        {
            this.itemsToDisplay = itemsToDisplay;
            this.filter = filter;
            this.itemBuilder = itemBuilder;
        }

        private GameObject getSpiralContainerReference()
        {
            return Resources.Load<GameObject>("Cube Container");
        }

        public GameObject BuildPreview(Vector3 positionForPreview)
        {
            GameObject palace = GameObject.Instantiate(getSpiralContainerReference(), Vector3.zero, Quaternion.identity);
            int i = 0;
            Item[] filteredItems = filter.FilterItems(itemsToDisplay);
            foreach (Item item in filteredItems)
            {
                GameObject node = GameObject.CreatePrimitive(PrimitiveType.Sphere);

				//attempt to destroy sphere collider
				node.GetComponent<Collider>().enabled = false;
				//turn off shadow casting for the spheres
				node.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                

				node.transform.parent = palace.transform;
                node.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                node.transform.position = new Vector3(Mathf.Sin(i) * .2f, -.35f + ((float)i / 400f), Mathf.Cos(i) * .2f);
                i++;
            }
            palace.GetComponent<SprialPreviewBehavior>().SetFilter(this.filter);
            palace.transform.position = positionForPreview;
            return palace;
        }


        /// <summary>
        /// Creates a large construction that some one can walk through.
        /// </summary>
        /// <returns>The palace that was constructed</returns>
        public GameObject BuildPalace()
        {
            GameObject palace = new GameObject("Palace");
            int itemsCreated = 0;
            int itemOffset = 0;
            Dictionary<Filter, bool>[] filtersPassedForItems = filter.FiltersPassed(itemsToDisplay);
            foreach (Item item in itemsToDisplay)
            {
                ItemBehaviour itemBehavior = null;
                Vector3 position = new Vector3(Mathf.Sin(itemsCreated) * 20, itemsCreated / 20, Mathf.Cos(itemsCreated) * 20);
                Dictionary<Filter, bool> appliedFiltersToItem = filtersPassedForItems[itemOffset + itemsCreated];
                if (itemBuilder != null)
                {
                    itemBehavior = itemBuilder(item, appliedFiltersToItem);
                    if (itemBehavior == null)
                    {
                        itemOffset++;
                        continue;
                    }
                    itemBehavior.transform.position = position;
                }
                else
                {
                    bool anyPassed = false;
                    foreach (KeyValuePair<Filter, bool> b in appliedFiltersToItem)
                    {
                        if (b.Value == true)
                        {
                            anyPassed = true;
                        }
                    }
                    if (anyPassed)
                    {
                        itemBehavior = item.Build(position, Vector3.zero);
                    }
                    else
                    {
                        itemOffset++;
                        continue;
                    }
                }

                itemBehavior.transform.parent = palace.transform;
                itemBehavior.transform.LookAt(Vector3.zero);
                itemsCreated++;
            }
            return palace;
        }

    }

}