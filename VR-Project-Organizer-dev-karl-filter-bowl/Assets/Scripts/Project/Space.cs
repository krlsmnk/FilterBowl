using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAVS.ProjectOrganizer.Project
{
	/// <summary>
	/// Think of like a "Project" a space consists of all the items of information
	/// you've imported into it, as well as the placements you've put them. Contains
	/// other meta information such as tags that help you catorize data.
	/// </summary>
    public class Space
    {

		/// <summary>
		/// All items inside of our scene.
		/// </summary>
        List<Item> itemsInProject;

		/// <summary>
		/// List of tags that items can be tagged with
		/// TODO: This should probably be redone..
		/// </summary>
		HashSet<string> tags;

		/// <summary>
		/// Given an Item, where is it in our scene?
		/// </summary>
        Dictionary<Item, Vector3> itemPostitionsInSpace;

		/// <summary>
		/// Given an Item, what is it's rotation in the scene.
		/// </summary>
		Dictionary<Item, Vector3> itemRotationsInSpace;

		/// <summary>
		/// Given an item, what tags does it have?
		/// </summary>
		Dictionary<Item, HashSet<string>> itemTags;

        /// <summary>
        /// Private so only it can instantiate itself..
        /// </summary>
        private Space() 
		{ 
			this.itemsInProject = new List<Item> ();
			this.itemPostitionsInSpace = new Dictionary<Item, Vector3> ();
			this.itemRotationsInSpace = new Dictionary<Item, Vector3> ();
			this.tags = new HashSet<string>();
			this.itemTags = new Dictionary<Item, HashSet<string>>();
		}

        /// <summary>
        /// Load a project from a XML file stored on the computer
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>

        public static Space LoadFromFile(string path)
        {
            Space p = new Space();
            
            // Add example item
			p.itemsInProject.Add(new TextItem(
				"Niave Bayes Classifier",
				"Assumes the independence of features to handle real and descrete data"
			));

			p.itemsInProject.Add(new TextItem(
				"Decision Trees", 
				"Supervised learning technique for predicting the likelyhood of a outcome given x features"
			));

			p.itemsInProject.Add(new TextItem(
				"Regression", 
				"Supervised learning for making best guess predictions in continuous sets"
			));
            
			p.itemsInProject.Add (new PictureItem (
				"Regression Example",
				"https://cdn-images-1.medium.com/max/600/1*iuqVEjdtEMY8oIu3cGwC1g.png"
			));

			p.itemsInProject.Add (new PictureItem (
				"Bayes Graph",
				"http://www.cs.cornell.edu/courses/cs4780/2015fa/web/projects/03NaiveBayes/nb.png"
			));

            // Set it's position
			p.itemPostitionsInSpace.Add(p.itemsInProject[0], new Vector3(0, 5, 0));
			p.itemPostitionsInSpace.Add(p.itemsInProject[1], new Vector3(-2, 3, 0));
			p.itemPostitionsInSpace.Add(p.itemsInProject[2], new Vector3(2, 3, 0));
			p.itemPostitionsInSpace.Add(p.itemsInProject[3], new Vector3(0, 1, 0));
			p.itemPostitionsInSpace.Add(p.itemsInProject[4], new Vector3(0, 3, -1));

			// Set rotation
			p.itemRotationsInSpace.Add(p.itemsInProject[0], Vector3.zero);
			p.itemRotationsInSpace.Add(p.itemsInProject[1], Vector3.zero);
			p.itemRotationsInSpace.Add(p.itemsInProject[2], Vector3.zero);
			p.itemRotationsInSpace.Add(p.itemsInProject[3], Vector3.zero);
			p.itemRotationsInSpace.Add(p.itemsInProject[4], Vector3.zero);

			// Add some tags that a item can have in the project
			p.tags.Add("Learning");
			p.tags.Add("Continuous");
			p.tags.Add("Discrete");

            return p;
        }

        public List<Item> GetItems()
        {
            return this.itemsInProject;
        }

		/// <summary>
		/// Gets all items that contain the tag that is passed in.
		/// </summary>
		/// <returns>The all items with tag.</returns>
		/// <param name="tag">Tag.</param>
		public List<Item> GetAllItemsWithTag(string tag)
		{
			if(!tags.Contains(tag))
			{
				Debug.LogWarning ("Attempting to find nodes by a tag that doesn't exist in the project space");
				return null;
			}

			List<Item> matchingItems = new List<Item> ();
			foreach (KeyValuePair<Item, HashSet<string>> pair in itemTags) {
				if(pair.Value.Contains(tag)){
					matchingItems.Add(pair.Key);
				}
			}
			return matchingItems;
		}

        public Dictionary<Item, Vector3> GetItemPositions()
        {
            return this.itemPostitionsInSpace;
        }

    }

}