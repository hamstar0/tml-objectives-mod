using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Theme;
using ModUtilityPanels.Classes.UI;


namespace Objectives.UI {
	partial class UIObjectivesTab : UIUtilityPanelsTab {
		private IList<UIElement> ObjectiveElemsList = new List<UIElement>();

		////

		private UIList ObjectivesDisplayElem;
		private UIHideableScrollbar Scrollbar;



		////////////////

		public UIObjectivesTab( UITheme theme ) : base( theme ) {
			this.Theme = theme;

			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );
		}


		////////////////

		public override void OnInitializeMe() {
			var label = new UIText( "Objectives:" );
			label.Top.Set( 0f, 0f );
			label.Left.Set( 0f, 0f );
			this.Append( (UIElement)label );

			var objectivesPanel = new UIPanel();
			objectivesPanel.Top.Set( 24f, 0f );
			objectivesPanel.Width.Set( 0f, 1f );
			objectivesPanel.Height.Set( 472f, 0f );
			objectivesPanel.HAlign = 0f;
			objectivesPanel.SetPadding( 4f );
			//modListPanel.PaddingTop = 0.0f;
			objectivesPanel.BackgroundColor = this.Theme.ListBgColor;
			objectivesPanel.BorderColor = this.Theme.ListEdgeColor;
			this.Append( (UIElement)objectivesPanel );

			this.ObjectivesDisplayElem = new UIList();
			this.ObjectivesDisplayElem.Left.Set( 0f, 0f );
			this.ObjectivesDisplayElem.Width.Set( -25f, 1f );
			this.ObjectivesDisplayElem.Height.Set( 0f, 1f );
			this.ObjectivesDisplayElem.HAlign = 0f;
			this.ObjectivesDisplayElem.ListPadding = 4f;
			this.ObjectivesDisplayElem.SetPadding( 0f );
			objectivesPanel.Append( (UIElement)this.ObjectivesDisplayElem );

			this.Scrollbar = new UIHideableScrollbar( true );
			this.Scrollbar.Top.Set( 8f, 0f );
			this.Scrollbar.Left.Set( -24f, 1f );
			this.Scrollbar.Height.Set( -16f, 1f );
			this.Scrollbar.SetView( 100f, 1000f );
			this.Scrollbar.HAlign = 0f;
			objectivesPanel.Append( (UIElement)this.Scrollbar );

			this.ObjectivesDisplayElem.SetScrollbar( this.Scrollbar );

			//

			this.ObjectivesDisplayElem.AddRange( this.ObjectiveElemsList );
			this.ObjectivesDisplayElem.UpdateOrder();
		}


		////////////////
		
		public void UpdateAll() {

		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			bool listChanged;

			try {
				this.Scrollbar.IsHidden = UIHideableScrollbar.IsScrollbarHidden(
					(int)this.ObjectivesDisplayElem.Height.Pixels,
					this.ObjectivesDisplayElem.Parent
				);

				if( this.Scrollbar.IsHidden ) {
					listChanged = this.ObjectivesDisplayElem.Width.Pixels != 0f;
					this.ObjectivesDisplayElem.Width.Pixels = 0f;
				} else {
					listChanged = this.ObjectivesDisplayElem.Width.Pixels != -25f;
					this.ObjectivesDisplayElem.Width.Pixels = -25f;
				}

				if( listChanged ) {
					this.Recalculate();
					this.ObjectivesDisplayElem.Recalculate();
				}
			} catch { }

			base.Draw( spriteBatch );
		}
	}
}
