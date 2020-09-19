using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.UI.ControlPanel;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( !this.AddObjectiveData(objective, ref order, out result) ) {
				return false;
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
	}
}
