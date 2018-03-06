using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAVS.ProjectOrganizer.Images
{
		
	/// <summary>
	/// Some one who wants to be passed in an image that was requested to load
	/// given some uri.
	/// </summary>
	public interface ImageLoaderSubscriber 
	{
		void OnImageLoad (string requestURI, Texture2D image);
	}

}