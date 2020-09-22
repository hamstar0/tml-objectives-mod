using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.UI.ControlPanel;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( !this.AddObjectiveData(objective, ref order, out result) ) {
				return false;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<ObjectivesPlayer>();
			string worldUid = WorldHelpers.GetUniqueIdForCurrentWorld( true );

			if( myplayer?.CompletedObjectivesPerWorld.Contains2D(worldUid, objective.Title) ?? false ) {
				objective.PercentComplete = 1f;
			}

			ObjectivesMod.Instance.ObjectivesTabUI.AddObjective( objective, order );

			if( alertPlayer ) {
				ControlPanelTabs.AddTabAlert( ObjectivesMod.ControlPanelName );
				
				Main.NewText( "New objective added: " + objective.Title, Color.Yellow );
			}

			return true;
		}


		public void RemoveObjective( string title ) {
			this.RemoveObjectiveData( title );

			ObjectivesMod.Instance.ObjectivesTabUI.RemoveObjective( title );
		}


		public void ClearObjectives() {
			this.ClearObjectivesData();

			var myplayer = Main.LocalPlayer.GetModPlayer<ObjectivesPlayer>();
			string worldUid = WorldHelpers.GetUniqueIdForCurrentWorld( true );

			myplayer?.CompletedObjectivesPerWorld[ worldUid ].Clear();

			ObjectivesMod.Instance.ObjectivesTabUI.ClearObjectives();
		}
	}
}
