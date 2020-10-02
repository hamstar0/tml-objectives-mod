using System;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Services.UI.ControlPanel;


namespace Objectives {
	partial class ObjectivesPlayer : ModPlayer {
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (ObjectivesMod)this.mod;

			try {
				if( mymod.ControlPanelHotkey != null && mymod.ControlPanelHotkey.JustPressed ) {
					if( ControlPanelTabs.IsDialogOpen() ) {
						ControlPanelTabs.CloseDialog();
					} else {
						ControlPanelTabs.OpenTab( ObjectivesMod.ControlPanelName );
					}
				}
			} catch { }
		}
	}
}
