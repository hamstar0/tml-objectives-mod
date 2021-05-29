using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;


namespace Objectives.UI {
	class UIHideableScrollbar : UIScrollbar {
		public static bool IsScrollbarHidden( int height, UIElement container ) {
			/*int listHeight = (int)container.Height.Pixels - 8;

			return height < listHeight;*/
			return false;
		}



		////////////////

		public bool IsHidden;



		////////////////

		public UIHideableScrollbar( bool isHidden ) {
			this.IsHidden = isHidden;
		}

		////////////////

		public override void Draw( SpriteBatch sb ) {
			if( !this.IsHidden ) {
				base.Draw( sb );
			}
		}
	}
}
