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

		private int _UpdateTimer = 0;

		internal void Update_Internal() {
			if( this._UpdateTimer-- <= 0 ) {
				this._UpdateTimer = 60;
			} else {
				return;
			}

			foreach( Objective obj in this.Objectives.Values ) {
				obj.Update_Internal();
			}
		}
	}
}
