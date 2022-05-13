using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModUtilityPanels.Services.UI.UtilityPanels;
using HUDElementsLib;
using HUDElementsLib.Elements.Samples;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public static Color GetTextColor( bool isLit ) {
			if( !isLit ) {
				return new Color( 160, 160, 160 );
			} else {
				float pulse = (float)Main.mouseTextColor / 255f;
				return new Color( 255, 255, (byte)(pulse * pulse * pulse * pulse * 255f) );
			}
		}



		////////////////

		private void LoadWidget() {
			var dim = new Vector2( 176f, 52f );
			var posOffset = new Vector2(
				-dim.X * 0.5f,
				-dim.Y - 32f
			);

			//

			this.ObjectivesProgressHUD = new CompletionStatHUD(
				relPos: posOffset,
				percPos: new Vector2( 0.5f, 1f ),
				dim: dim,
				title: "Objectives",
				stat: () => {
					var mngr = ModContent.GetInstance<ObjectiveManager>();
					ICollection<Objective> objs = mngr.CurrentObjectives.Values;
					int complete = objs.Count( o => o.IsComplete == true );

					return (complete, objs.Count - complete);
				},
				enabler: () => Main.playerInventory
			);

			//

			this.ObjectivesProgressHUD.TitleColor = ObjectiveManager.GetTextColor( false );

			this.ObjectivesProgressHUD.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( ObjectivesMod.UtilityPanelsName );
			};

			//

			HUDElementsLibAPI.AddWidget( this.ObjectivesProgressHUD );
		}


		////////////////

		private void UpdateWidget_Local_If() {
			if( Main.dedServ || Main.netMode == NetmodeID.Server ) {
				return;
			}

			//

			CompletionStatHUD widget = this.ObjectivesProgressHUD;
			if( !widget.IsEnabled() ) {
				return;
			}

			//

			if( widget.IsMouseHovering ) {
				widget.TitleColor = ObjectiveManager.GetTextColor( true );
			} else {
				widget.TitleColor = ObjectiveManager.GetTextColor( false );
			}
		}
	}
}
