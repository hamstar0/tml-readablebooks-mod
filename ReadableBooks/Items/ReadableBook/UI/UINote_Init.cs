﻿using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Classes.Errors;
using ModLibsUI.Classes.UI.Theme;
using ModLibsUI.Classes.UI.Elements;


namespace ReadableBooks.Items.ReadableBook.UI {
	/// <summary></summary>
	public partial class UINote : UIElement {
		/// @private
		public override void OnInitialize() {
			CalculatedStyle dim;

			this.TitleElem = new UIThemedText( UITheme.Vanilla, false, this.TitleText, true, 1f, true );
			this.TitleElem.Left.Set( 0f, 0.5f );
			this.TitleElem.Top.Set( UINote.PixelsFromTop, 0f );
			this.Append( this.TitleElem );

			this.BodyElem = new UIThemedText( UITheme.Vanilla, false, this.BodyText, true, 1f, false );
			this.BodyElem.Left.Set( 0f, 0.5f );
			this.BodyElem.Top.Set( UINote.PixelsFromTop + 64f, 0f );
			this.Append( this.BodyElem );
			
			//

			this.PrevPageElem = new UIText( "Prev Page", 1f, false );
			this.PrevPageElem.Recalculate();
			dim = this.PrevPageElem.GetDimensions();
			this.PrevPageElem.Left.Set( -128f - (dim.Width * 0.5f), 0.5f );
			this.PrevPageElem.Top.Set( UINote.PixelsFromTop + 40f, 0f );
			this.Append( this.PrevPageElem );

			this.NextPageElem = new UIText( "Next Page", 1f, false );
			this.NextPageElem.Recalculate();
			dim = this.NextPageElem.GetDimensions();
			this.NextPageElem.Left.Set( 128f - (dim.Width * 0.5f), 0.5f );
			this.NextPageElem.Top.Set( UINote.PixelsFromTop + 40f, 0f );
			this.Append( this.NextPageElem );

			this.PageNumElem = new UIText( this.CurrentPage+" / "+(this.Pages.Length-1), 1f, false );
			this.PageNumElem.Recalculate();
			dim = this.NextPageElem.GetDimensions();
			this.PageNumElem.Left.Set( dim.Width * -0.5f, 0.5f );
			this.PageNumElem.Top.Set( UINote.PixelsFromTop + 40f, 0f );
			this.Append( this.PageNumElem );

			//

			this.PrevPageElem.OnClick += ( _, __ ) => {
				Main.LocalPlayer.mouseInterface = true;
				this.GoToPage( this.CurrentPage - 1 );
			};
			this.NextPageElem.OnClick += ( _, __ ) => {
				Main.LocalPlayer.mouseInterface = true;
				this.GoToPage( this.CurrentPage + 1 );
			};

			//

			this.SetTitle( this.TitleText );

			this.GoToPage( this.CurrentPage );
		}
	}
}
