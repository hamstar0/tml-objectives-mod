using System;
using System.Linq;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public Objective[] GetObjectives() {
			return this.CurrentObjectiveOrder
				.Select( title => this.CurrentObjectives[title] )
				.ToArray();
		}


		////

		private bool AddObjectiveData( Objective objective, ref int order, out string result ) {
			if( this.CurrentObjectives.ContainsKey(objective.Title) ) {
				result = "Objective named "+objective.Title+" already defined.";
				return false;
			}

			if( order < 0 ) {
				order = this.CurrentObjectiveOrder.Count;
			} else if( order > this.CurrentObjectiveOrder.Count ) {
				result = "Objective's "+objective.Title+" order (#"+order+") is out of range.";
				return false;
			}

			this.CurrentObjectives[ objective.Title ] = objective;

			this.CurrentObjectiveOrder.Insert( order, objective.Title );

			this.CurrentObjectiveOrderByName[ objective.Title ] = order;

			int count = this.CurrentObjectiveOrder.Count;
			for( int i=order+1; i<count; i++ ) {
				string title = this.CurrentObjectiveOrder[i];
				this.CurrentObjectiveOrderByName[ title ] += 1;
			}

			result = "Success.";
			return true;
		}

		private void RemoveObjectiveData( string title ) {
			if( !this.CurrentObjectiveOrderByName.TryGetValue(title, out int idx) ) {
				idx = -1;
			}

			this.CurrentObjectives.TryRemove( title, out Objective _ );
			this.CurrentObjectiveOrder.Remove( title );
			this.CurrentObjectiveOrderByName.TryRemove( title, out int __ );

			if( idx == -1 ) {
				return;
			}

			int count = this.CurrentObjectiveOrderByName.Count;
			for( int i=idx; i<count; i++ ) {
				string next = this.CurrentObjectiveOrder[i];
				this.CurrentObjectiveOrderByName[ next ] -= 1;
			}
		}

		private void ClearObjectivesData() {
			this.CurrentObjectives.Clear();
			this.CurrentObjectiveOrder.Clear();
			this.CurrentObjectiveOrderByName.Clear();
		}
	}
}
