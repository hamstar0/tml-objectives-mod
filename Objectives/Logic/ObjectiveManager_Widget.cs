using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
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
			var pos = new Vector2(
				((float)Main.screenWidth - dim.X) * 0.5f,
				(float)Main.screenHeight - dim.Y - 32f
			);

			//

			var mymod = ObjectivesMod.Instance;

			mymod.ObjectivesProgressHUD = new CompletionStatHUD(
				pos: pos,
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

			mymod.ObjectivesProgressHUD.TitleColor = ObjectiveManager.GetTextColor( false );

			mymod.ObjectivesProgressHUD.OnMouseOver += (_, __) => {
				mymod.ObjectivesProgressHUD.TitleColor = ObjectiveManager.GetTextColor( true );
			};
			mymod.ObjectivesProgressHUD.OnMouseOut += (_, __) => {
				mymod.ObjectivesProgressHUD.TitleColor = ObjectiveManager.GetTextColor( false );
			};

			mymod.ObjectivesProgressHUD.OnClick += (_, __) => {
				UtilityPanelsTabs.OpenTab( ObjectivesMod.UtilityPanelsName );
			};

			//

			HUDElementsLibAPI.AddWidget( mymod.ObjectivesProgressHUD );
		}
	}
}
