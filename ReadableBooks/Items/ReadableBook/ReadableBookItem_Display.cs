using Terraria;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.UI;
using ModLibsUI.Services.UI.LayerDisable;
using ModLibsUI.Services.UI.FreeHUD;
using ReadableBooks.Items.ReadableBook.UI;


namespace ReadableBooks.Items.ReadableBook {
	/// <summary>
	/// A readable note.
	/// </summary>
	public partial class ReadableBookItem : ModItem {
		private static bool IsDisplayingNote = false;



		////////////////

		private static void DisplayNote( string titleText, string[] pages ) {
			UINote elem = FreeHUD.GetElement("ReadableBook") as UINote;

			if( elem == null ) {
				elem = new UINote( titleText, pages );
				elem.Initialize();
				FreeHUD.AddElement( "ReadableBook", elem );
			} else {
				elem.SetTitle( titleText );
				elem.SetPages( pages );
			}

			LayerDisable.Instance.DisabledLayers.Add( LayerDisable.InfoAccessoriesBar );
		}


		////

		private static void ClearDisplay() {
			FreeHUD.RemoveElement( "ReadableBook" );

			LayerDisable.Instance.DisabledLayers.Remove( LayerDisable.InfoAccessoriesBar );
		}


		////////////////

		internal static void UpdateDisplay() {
			if( !ReadableBookItem.IsDisplayingNote ) {
				return;
			}

			bool uiAvailable = UILibraries.IsUIAvailable(
				//mouseNotInUse: true,
				playerAvailable: true,
				playerNotTalkingToNPC: true,
				noFullscreenMap: true
			);

			if( Main.gameMenu || Main.playerInventory || !uiAvailable ) {
				ReadableBookItem.IsDisplayingNote = false;
				ReadableBookItem.ClearDisplay();
			}
		}



		////////////////

		private void DisplayCurrentNote() {
			Main.playerInventory = false;

			ReadableBookItem.IsDisplayingNote = true;
			ReadableBookItem.DisplayNote( this.TitleText, this.Pages );
		}
	}
}
