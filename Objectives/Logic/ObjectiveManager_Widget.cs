using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.Loadable;
using HUDElementsLib;
using HUDElementsLib.Elements.Samples;
using Terraria.ModLoader;
using Objectives.Definitions;

namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		private void LoadWidget() {
			var dim = new Vector2( 176f, 52f );
			var pos = new Vector2(
				((float)Main.screenWidth - dim.X) * 0.5f,
				(float)Main.screenHeight - dim.Y //- 32f
			);

			ObjectivesMod.Instance.ObjectivesProgressHUD = new CompletionStatHUD(
				pos: pos,
				dim: dim,
				title: "Objectives:",
				stat: () => {
					var mngr = ModContent.GetInstance<ObjectiveManager>();
					ICollection<Objective> objs = mngr.CurrentObjectives.Values;
					int complete = objs.Count( o => o.IsComplete == true );

					return (complete, objs.Count - complete);
				},
				enabler: () => Main.playerInventory
			);

			HUDElementsLibAPI.AddWidget( ObjectivesMod.Instance.ObjectivesProgressHUD );
		}
	}
}
