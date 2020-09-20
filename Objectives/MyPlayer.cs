using System;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Services.UI.ControlPanel;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace Objectives {
	partial class ObjectivesPlayer : ModPlayer {
		internal IDictionary<string, ISet<string>> CompletedObjectivesPerWorld = new Dictionary<string, ISet<string>>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.CompletedObjectivesPerWorld.Clear();

			if( !tag.ContainsKey("world_count") ) {
				return;
			}

			int worldCount = tag.GetInt( "world_count" );

			for( int i=0; i<worldCount; i++ ) {
				string worldUid = tag.GetString( "world_uid_"+i );
				int objectiveCount = tag.GetInt( "world_obj_count_"+i );

				this.CompletedObjectivesPerWorld[ worldUid ] = new HashSet<string>();

				for( int j=0; j<objectiveCount; j++ ) {
					string objective = tag.GetString( "world_"+i+"_obj_"+j );

					this.CompletedObjectivesPerWorld[ worldUid ].Add( objective );
				}
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "would_count", this.CompletedObjectivesPerWorld.Count }
			};

			int i = 0;
			foreach( (string worldUid, ISet<string> objectives) in this.CompletedObjectivesPerWorld ) {
				tag[ "world_uid_"+i ] = worldUid;
				tag[ "world_obj_count_"+i ] = worldUid;

				int j=0;
				foreach( string objective in objectives ) {
					tag[ "world_"+i+"_obj_"+j ] = objective;
					j++;
				}

				i++;
			}

			return tag;
		}


		////////////////

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
