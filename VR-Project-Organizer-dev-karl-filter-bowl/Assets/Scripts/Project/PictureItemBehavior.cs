using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CAVS.ProjectOrganizer.Images;

namespace CAVS.ProjectOrganizer.Project
{

	public class PictureItemBehavior : ItemBehaviour, ImageLoaderSubscriber{

		string uriLoadedFrom;

		void Awake()
		{
			uriLoadedFrom = "";
		}

		public void SetImage(string uri)
		{
			uriLoadedFrom = uri;
			ImageLoader.LoadImage (uri, this);
		}

		public void OnImageLoad (string requestURI, Texture2D image)
		{
			if (uriLoadedFrom != requestURI) {
				return;
			}
			GetComponent<Renderer>().material.mainTexture = image;
		}

	}

}