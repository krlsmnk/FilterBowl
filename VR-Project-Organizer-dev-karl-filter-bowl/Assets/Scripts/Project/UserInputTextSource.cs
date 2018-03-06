using System;

namespace CAVS.ProjectOrganizer.Project
{
	/// <summary>
	/// This is text that was typed out manually by the user. This can happen
	/// when creating
	/// </summary>
	public class UserInputTextSource : TextSource {

		private string content;

		public UserInputTextSource(string content)
		{
			this.content = content;
		}

		public override string GetContent ()
		{
			return this.content;
		}

		public override string Serialize ()
		{
			throw new NotImplementedException ();
		}

	}

}