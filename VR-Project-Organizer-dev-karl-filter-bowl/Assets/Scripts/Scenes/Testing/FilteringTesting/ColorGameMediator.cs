using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CAVS.ProjectOrganizer.Project;
using CAVS.ProjectOrganizer.Project.Filtering;
using CAVS.ProjectOrganizer.Project.Aggregations.Spiral;

namespace CAVS.ProjectOrganizer.Scenes.Testing.FilteringTesting
{

    /// <summary>
    /// Example class with how'd you would create your own custom node renders
    /// </summary>
    public class ColorGameMediator : GameMediator
    {

        private GameObject palace;

        private ItemBehaviour itemBuilder(Item item, Dictionary<Filter, bool> filters)
        {
            GameObject fakeNode = GameObject.CreatePrimitive(PrimitiveType.Cube);

			//turns off shadow casting for the nodes
			fakeNode.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            int currentlyFiltered = 0;

            foreach (KeyValuePair<Filter, bool> b in filters)
            {
                if (b.Value == false)
                {
                    currentlyFiltered++;
                }
            }

            //change shape if filters have "failed"?
            if (currentlyFiltered == 1) fakeNode = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            if (currentlyFiltered == 2) fakeNode = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            if (currentlyFiltered > 2) fakeNode = GameObject.CreatePrimitive(PrimitiveType.Cube);

			//turns off shadow casting for the nodes
			fakeNode.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            //resizes nodes based on how many filters they have "failed"? 
            fakeNode.transform.localScale = Vector3.one * (1f / (float)(currentlyFiltered + 1));

            //recolor nodes based on how many filters they have "failed"? 
            //fakeNode.GetComponent<Renderer> ().material.color = Color.green;
            fakeNode.GetComponent<Renderer>().material.color = new Color(ModifiedSigmoid(currentlyFiltered), .5f, .5f);

            return fakeNode.AddComponent<ItemBehaviour>();
        }

        protected override void displayPalace()
        {
            if (palace != null)
            {
                Destroy(palace);
            }
            palace = new ItemSpiral(allItems, new AggregateFilter(appliedFilters.ToArray()), itemBuilder).BuildPalace();
        }

        //produces a value between -1 and 1, then increments that to 0-2, then divides so we get the desired range of 0-1
        public float ModifiedSigmoid(int x)
        {
            return (float)((2 / (1 + System.Math.Exp(-2 * x)) - 1) + 1) / 2;
        }

    }
}