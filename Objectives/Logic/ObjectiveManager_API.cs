using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModControlPanel.Services.UI.ControlPanel;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( !this.AddObjectiveData(objective, ref order, out result) ) {
				return false;
			}

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );

			// Load data
			bool isPrevComplete = myplayer?.IsObjectiveByNameComplete( objective.Title ) ?? false;
			objective.Initialize( isPrevComplete );

			ObjectivesMod.Instance
				.ObjectivesTabUI
				.AddObjective( objective, order );	// Initializes objective

			if( !objective.IsComplete.Value && alertPlayer ) {
				ControlPanelTabs.AddTabAlert( ObjectivesMod.ControlPanelName, true );

				Main.NewText( "New objective added: " + objective.Title, Color.Yellow );

				Main.PlaySound( SoundID.Chat, Main.LocalPlayer.MountedCenter );
			}

			this.NotifySubscribers( objective, true );

			return true;
		}


		public void RemoveObjective( string title, bool forceIncomplete ) {
			this.RemoveObjectiveData( title );

			if( forceIncomplete ) {
				var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
				myplayer.ForgetCompletedObjective( title );
			}

			ObjectivesMod.Instance
				.ObjectivesTabUI
				.RemoveObjective( title );
		}


		public void ClearObjectives( bool forceIncomplete ) {
			this.ClearObjectivesData();

			if( forceIncomplete ) {
				var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
				myplayer?.ClearCompletedObjectives();
			}

			ObjectivesMod.Instance
				.ObjectivesTabUI
				.ClearObjectives();
		}


		////

		public void NotifySubscribers( Objective objective, bool isNew ) {
			foreach( ObjectivesAPI.SubscriptionEvent evt in this.Subscribers.Values ) {
				bool isComplete = objective.IsComplete.HasValue
					? objective.IsComplete.Value
					: false;
				evt.Invoke( objective.Title, isNew, isComplete );
			}
		}
	}
}
