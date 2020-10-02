using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		private int _UpdateTimer = 0;


		////////////////

		public ConcurrentDictionary<string, Objective> CurrentObjectives { get; } = new ConcurrentDictionary<string, Objective>();

		public IList<string> CurrentObjectiveOrder { get; } = new List<string>();

		public ConcurrentDictionary<string, int> CurrentObjectiveOrderByName { get; } = new ConcurrentDictionary<string, int>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void Update_Internal() {
			if( this._UpdateTimer-- <= 0 ) {
				this._UpdateTimer = 60;
			} else {
				return;
			}

			foreach( Objective obj in this.CurrentObjectives.Values ) {
				obj.Update_Internal();
			}
		}
	}
}
