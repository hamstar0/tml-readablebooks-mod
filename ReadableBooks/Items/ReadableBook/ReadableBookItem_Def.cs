using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace ReadableBooks.Items.ReadableBook {
	/// <summary>
	/// A readable note.
	/// </summary>
	public partial class ReadableBookItem : ModItem {
		/*public static string DefaultTitle { get; private set; } = "Lorem Ipsum";
		public static string[] DefaultPages { get; private set; } = new string[] {
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit,"
				+"\nsed do eiusmod tempor incididunt ut labore et dolore"
				+"\nmagna aliqua.",
			"Ut enim ad minim veniam, quis nostrud exercitation ullamco"
				+"\nlaboris nisi ut aliquip ex ea commodo consequat.",
			"Duis aute irure dolor in reprehenderit in voluptate velit"
				+"\nesse cillum dolore eu fugiat nulla pariatur."
		};*/


		////////////////

		public static Item CreateBook( string title, string[] pages ) {
			var book = new Item();
			book.SetDefaults( ModContent.ItemType<ReadableBookItem>() );

			var mybook = book.modItem as ReadableBookItem;
			mybook.TitleText = title;
			mybook.Pages = pages;

			return book;
		}



		////////////////

		/// <summary></summary>
		public string TitleText { get; private set; }

		/// <summary></summary>
		public string[] Pages { get; private set; }


		////

		public override bool CloneNewInstances => false;

		////////////////

		/*private bool? _IsScribingEnabled = null;

		/// <summary>
		/// Reports if config enables scribing notes. Manually setting this value overrides config `NoteWritingEnabled`
		/// setting.
		/// </summary>
		public bool? IsScribingEnabled {
			get {
				if( !this._IsScribingEnabled.HasValue ) {
					return ModHelpersConfig.Instance.NoteWritingEnabled;
				}
				return this._IsScribingEnabled;
			}
			set {
				this._IsScribingEnabled = value;
			}
		}*/



		////////////////

		public override ModItem Clone( Item item ) {
			var clone = base.Clone( item ) as ReadableBookItem;
			clone.TitleText = ""+this.TitleText;
			clone.Pages = this.Pages.ToArray();
			return clone;
		}


		////////////////

		/// @private
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Readable Book" );
			this.Tooltip.SetDefault( "Right-click to read." );
		}


		/// @private
		public override void SetDefaults() {
			this.item.maxStack = 1;
			this.item.width = 24;
			this.item.height = 24;
			this.item.value = Item.buyPrice( 0, 0, 0, 75 );
			this.item.rare = ItemRarityID.Blue;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("pages") ) {
				this.TitleText = "...";
				this.Pages = new string[] { ". . ." };

				return;
			}

			this.TitleText = tag.GetString( "title" );

			int len = tag.GetInt( "pages" );
			
			this.Pages = new string[ len ];

			for( int i=0; i<len; i++ ) {
				this.Pages[i] = tag.GetString( "page_"+i );
			}
		}

		public override TagCompound Save() {
			string title = this.TitleText ?? "...";
			int len = this.Pages?.Length ?? 0;
			var tag = new TagCompound {
				{ "title", title },
				{ "pages", len }
			};

			for( int i=0; i<len; i++ ) {
				tag[ "page_"+i ] = this.Pages[i];
			}

			return tag;
		}

		////////////////

		/// @private
		public override void NetSend( BinaryWriter writer ) {
			int len = this.Pages?.Length ?? 0;

			writer.Write( this.TitleText ?? "..." );
			writer.Write( len );

			for( int i = 0; i < len; i++ ) {
				writer.Write( this.Pages[i] );
			}
		}

		/// @private
		public override void NetRecieve( BinaryReader reader ) {
			this.TitleText = reader.ReadString();

			int pages = reader.ReadInt32();
			this.Pages = new string[ pages ];

			for( int i=0; i<pages; i++ ) {
				this.Pages[i] = reader.ReadString();
			}
		}


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			if( string.IsNullOrEmpty(this.TitleText) ) {
				return;
			}

			var tip = new TooltipLine( this.mod, "ReadableBookTitle", this.TitleText );
			tooltips.Insert( 0, tip );

			if( Main.mouseRight && Main.mouseRightRelease ) {
				this.DisplayCurrentNote();
			}
		}
	}
}
