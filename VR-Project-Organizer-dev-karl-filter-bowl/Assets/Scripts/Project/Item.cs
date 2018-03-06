using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CAVS.ProjectOrganizer.Project
{

    public abstract class Item
    {

        protected abstract GameObject getGameobjectReference();

        private readonly string title;

        Dictionary<string, string> values;

        public Item(string title)
        {
            this.values = new Dictionary<string, string>();
            this.title = title;
        }

        public Item(string title, Dictionary<string, string> values)
        {
            this.values = values;
            this.title = title;
        }

        /// <summary>
        /// Gets the title, something to represent the node
        /// </summary>
        /// <returns>The title.</returns>
        public string GetTitle()
        {
            return title;
        }


        /// <summary>
        /// Attempts to retrieve value of the field passed in.
        /// </summary>
        /// <param name="field">field associated with the value (key in the dict)</param>
        /// <returns>value if found, else null</returns>
        public string GetValue(string field)
        {
            if (values.ContainsKey(field))
            {
                return values[field];
            }
            return null;
        }

        /// <summary>
        /// Builds a graphical representation of the object inside of the scene
        /// </summary>
        /// <returns>The item.</returns>
        public ItemBehaviour Build()
        {
            return this.Build(Vector3.zero, Vector3.zero);
        }

        /// <summary>
        /// Builds a graphical representation of the object inside of the scene
        /// </summary>
        /// <returns>The item.</returns>
        public ItemBehaviour Build(Vector3 position, Vector3 rotation)
        {
            GameObject node = GameObject.Instantiate(getGameobjectReference());
            node.transform.name = this.GetTitle();
            node.transform.position = position;
            node.transform.rotation = Quaternion.Euler(rotation);

            // The canvas could actually be added on at this step, instead of finding a reference..
            node.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = this.GetTitle();

            return BuildItem(node);
        }

        protected abstract ItemBehaviour BuildItem(GameObject node);

    }

}