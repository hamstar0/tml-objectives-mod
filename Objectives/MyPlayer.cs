using System;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModUtilityPanels.Services.UI.UtilityPanels;


namespace Objectives {
	partial class ObjectivesPlayer : ModPlayer {
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (ObjectivesMod)this.mod;

			try {
				if( mymod.UtilityPanelsHotkey != null && mymod.UtilityPanelsHotkey.JustPressed ) {
					if( UtilityPanelsTabs.IsDialogOpen() ) {
						UtilityPanelsTabs.CloseDialog();
					} else {
						UtilityPanelsTabs.OpenTab( ObjectivesMod.UtilityPanelsName );
					}
				}
			} catch { }
		}
	}
}
