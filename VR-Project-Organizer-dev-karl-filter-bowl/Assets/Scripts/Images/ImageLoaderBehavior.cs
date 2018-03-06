using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CAVS.ProjectOrganizer.Images
{
	/// <summary>
	/// In charge of making a request
	/// </summary>
	public class ImageLoaderBehavior : MonoBehaviour {

		public void LoadImage(string url, ImageLoaderSubscriber sub, string nameToUseInCache)
		{
			StartCoroutine (LoadImageAsync (url, sub, nameToUseInCache));
		}


		private IEnumerator LoadImageAsync(string url, ImageLoaderSubscriber sub, string nameToUseInCache)
		{
			// Make a request then to download the source
			WWW www = new WWW(url);
			yield return www;

			// Cache the image on the machine.
			File.WriteAllBytes(nameToUseInCache, www.bytes);
			sub.OnImageLoad (url, www.texture);
		}

	}

}