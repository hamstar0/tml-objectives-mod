using System;
using System.Collections.Generic;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public IDictionary<string, Objective> Objectives { get; } = new Dictionary<string, Objective>();

		public IList<string> ObjectiveOrder { get; } = new List<string>();

		public IDictionary<string, int> ObjectiveOrderByName { get; } = new Dictionary<string, int>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }


		////////////////
		
		public bool AddObjective( Objective objective, int order, out string result ) {
			if( !this.AddObjectiveData(objective, ref order, out result) ) {
				return false;
			}

			ObjectivesMod.Instance.ObjectivesTabUI.AddObjective( objective, order );
			return true;
		}

		public void RemoveObjective( string title ) {
			this.RemoveObjective( title );

			ObjectivesMod.Instance.ObjectivesTabUI.RemoveObjective( title );
		}


		////////////////

		internal void Update_Internal() {
			foreach( Objective obj in this.Objectives.Values ) {
				obj.Update_Internal();
			}
		}
	}
}
