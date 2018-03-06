using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CAVS.ProjectOrganizer.Images
{

	/// <summary>
	/// In charge of making asyncronous requests to load an image as a texture
	/// to be used later.
	/// </summary>
	public static class ImageLoader 
	{

		/// <summary>
		/// Attempts to load an image from either the filesystem or cache, or
		/// will make an http request to fetch the image for asyncronously over
		/// http
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="sub">Sub.</param>
		public static void LoadImage(string url, ImageLoaderSubscriber sub) {

			if (!Directory.Exists("cache"))
			{
				Directory.CreateDirectory("cache");
			}

			// Get Md5 hash...
			string hashName = "something hashed";

			// Try Loading from memory...
			string[] chunks = url.Split('.');
			string extension = chunks[chunks.Length-1];

			string filePath = Path.Combine( Path.Combine(Directory.GetCurrentDirectory(), "cache"), hashName + "." + extension);

			GetRequestObject ().LoadImage (url, sub, filePath);
		}

		private static ImageLoaderBehavior requestGameObjectInstance = null;

		private static ImageLoaderBehavior GetRequestObject()
		{

			if (requestGameObjectInstance == null)
			{
				GameObject obj = (GameObject)GameObject.Instantiate(new GameObject("Image Loader Object"));
				requestGameObjectInstance = obj.AddComponent<ImageLoaderBehavior>();
				Object.DontDestroyOnLoad(obj);
			}

			return requestGameObjectInstance;

		}


	}

}