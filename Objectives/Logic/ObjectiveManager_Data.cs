using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		public Objective[] GetObjectives() {
			return this.ObjectiveOrder
				.Select( title => this.Objectives[title] )
				.ToArray();
		}


		////

		private bool AddObjectiveData( Objective objective, ref int order, out string result ) {
			if( this.Objectives.ContainsKey(objective.Title) ) {
				result = "Objective named "+objective.Title+" already defined.";
				return false;
			}

			if( order < 0 ) {
				order = this.ObjectiveOrder.Count;
			} else if( order > this.ObjectiveOrder.Count ) {
				result = "Objective's "+objective.Title+" order (#"+order+") is out of range.";
				return false;
			}

			this.Objectives[ objective.Title ] = objective;

			this.ObjectiveOrder.Insert( order, objective.Title );

			this.ObjectiveOrderByName[ objective.Title ] = order;

			int count = this.ObjectiveOrder.Count;
			for( int i=order+1; i<count; i++ ) {
				string title = this.ObjectiveOrder[i];
				this.ObjectiveOrderByName[ title ] += 1;
			}

			result = "Success.";
			return true;
		}

		private void RemoveObjectiveData( string title ) {
			if( !this.ObjectiveOrderByName.TryGetValue(title, out int idx) ) {
				idx = -1;
			}

			this.Objectives.Remove( title );
			this.ObjectiveOrder.Remove( title );
			this.ObjectiveOrderByName.Remove( title );

			if( idx == -1 ) {
				return;
			}

			int count = this.ObjectiveOrderByName.Count;
			for( int i=idx; i<count; i++ ) {
				string next = this.ObjectiveOrder[i];
				this.ObjectiveOrderByName[ next ] -= 1;
			}
		}
	}
}
