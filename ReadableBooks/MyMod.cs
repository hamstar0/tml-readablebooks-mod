using Terraria.ModLoader;
using ReadableBooks.Items.ReadableBook;


namespace ReadableBooks {
	public class ReadableBooksMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-readablebooks-mod";


		////////////////

		public static ReadableBooksMod Instance { get; private set; }



		////////////////

		public ReadableBooksMod() {
			ReadableBooksMod.Instance = this;
		}

		public override void Unload() {
			ReadableBooksMod.Instance = null;
		}
	}




	class ReadableBooksPlayer : ModPlayer {
		public override void PreUpdate() {
			ReadableBookItem.UpdateDisplay();
		}
	}
}