using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ModLibsCore.Classes.Loadable;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		private int _UpdateTimer = 0;


		////////////////

		public ConcurrentDictionary<string, Objective> CurrentObjectives { get; } = new ConcurrentDictionary<string, Objective>();

		public IList<string> CurrentObjectiveOrder { get; } = new List<string>();

		public ConcurrentDictionary<string, int> CurrentObjectiveOrderByName { get; } = new ConcurrentDictionary<string, int>();

		public ConcurrentDictionary<string, ObjectivesAPI.SubscriptionEvent> Subscribers { get; } = new ConcurrentDictionary<string, ObjectivesAPI.SubscriptionEvent>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() {
			this.LoadWidget();
		}

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void Update_Internal() {
			if( this._UpdateTimer-- <= 0 ) {
				this._UpdateTimer = 60;
			} else {
				return;
			}

			foreach( Objective obj in this.CurrentObjectives.Values ) {
				if( obj.Update_Internal() ) {
					this.NotifySubscribers( obj, false );
				}
			}
		}
	}
}
