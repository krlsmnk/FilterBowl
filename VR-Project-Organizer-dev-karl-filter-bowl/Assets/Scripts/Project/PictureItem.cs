using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CAVS.ProjectOrganizer.Project
{

	public class PictureItem : Item
	{

		private static GameObject nodeGameobjectReference;
		protected override GameObject getGameobjectReference()
		{
			if (nodeGameobjectReference == null)
			{
				nodeGameobjectReference = Resources.Load<GameObject> ("Picture Node");
			}
			return nodeGameobjectReference;
		}

		private string url;

		public PictureItem(string title, string url) : base(title)
		{
			this.url = url;
		}

		public string GetUrl()
		{
			return this.url;
		}

		/// <summary>
		/// Builds a graphical representation of the object inside of the scene for
		/// displaying the text content.
		/// </summary>
		/// <returns>The item just built.</returns>
		protected override ItemBehaviour BuildItem (GameObject node) 
		{
			PictureItemBehavior urlBehavior = node.AddComponent<PictureItemBehavior>();
			urlBehavior.SetImage (this.GetUrl());
			return urlBehavior;
		}

	}

}