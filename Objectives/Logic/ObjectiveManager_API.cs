using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Messages.Inbox;
using HamstarHelpers.Services.UI.ControlPanel;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( !this.AddObjectiveData( objective, ref order, out result ) ) {
				return false;
			}

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );

			// Load data
			bool isComplete = myplayer?.IsObjectiveComplete( objective.Title ) ?? false;
			objective.Initialize( isComplete );

			ObjectivesMod.Instance.ObjectivesTabUI.AddObjective( objective, order );

			if( !objective.IsComplete && alertPlayer ) {
				InboxMessages.SetMessage(
					which: "ObjectivesAlert",
					msg: "New objective(s) added!",
					forceUnread: true,
					onRun: ( isUnread ) => {
						if( isUnread ) {
							if( !ControlPanelTabs.IsDialogOpen() ) {
								ControlPanelTabs.OpenTab( ObjectivesMod.ControlPanelName );
							}
						}
					}
				);

				ControlPanelTabs.AddTabAlert( ObjectivesMod.ControlPanelName );

				Main.NewText( "New objective added: " + objective.Title, Color.Yellow );
			}

			this.NotifySubscribers( objective, true );

			return true;
		}


		public void RemoveObjective( string title ) {
			this.RemoveObjectiveData( title );

			ObjectivesMod.Instance.ObjectivesTabUI.RemoveObjective( title );
		}


		public void ClearObjectives() {
			this.ClearObjectivesData();

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
			myplayer?.ClearCompletedObjectives();

			ObjectivesMod.Instance.ObjectivesTabUI.ClearObjectives();
		}


		////

		public void NotifySubscribers( Objective objective, bool isNew ) {
			foreach( ObjectivesAPI.SubscriptionEvent evt in this.Subscribers.Values ) {
				evt.Invoke( objective.Title, isNew, objective.IsComplete );
			}
		}
	}
}
