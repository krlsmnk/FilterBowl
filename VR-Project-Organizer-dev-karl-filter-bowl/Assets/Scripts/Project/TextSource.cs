using System;

namespace CAVS.ProjectOrganizer.Project
{

	/// <summary>
	/// Meant to abstract away where the text data came from.
	/// </summary>
	public abstract class TextSource {

		/// <summary>
		/// Returns a string representation of the text to be stored in memory
		/// </summary>
		public abstract string Serialize ();

		/// <summary>
		/// Gets the content of the text source, whatever that may be.
		/// 
		/// For example, if the source is a url, it would be the text
		/// that you get from hitting that url
		/// </summary>
		/// <returns>The content.</returns>
		public abstract string GetContent();

		/// <summary>
		/// Deserialize the specified data.
		/// </summary>
		/// <param name="serializedSource">Serialized source.</param>
		public static TextSource Deserialize(string serializeData){
			// TODO: Implement desiraliziation
			throw new NotImplementedException();
		}

	}

}