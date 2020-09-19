using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Classes.Errors;


namespace Objectives.Definitions {
	public class ChecklistObjective : Objective {
		public delegate IDictionary<string, bool> ChecklistObjectiveCondition( ChecklistObjective currentObjective );



		////////////////

		protected ChecklistObjectiveCondition Condition = null;


		////////////////

		public IDictionary<string, bool> CheckList { get; private set; }



		////////////////

		public ChecklistObjective(
					string title, string description,
					ChecklistObjectiveCondition condition = null )
					: base( title, description ) {
			this.Condition = condition;
		}


		////////////////

		protected sealed override IDictionary<string, float> ComputeCompletionStatus() {
			this.CheckList = this.Condition?.Invoke( this ) ?? new Dictionary<string, bool>();

			return this.CheckList.ToDictionary( kv=>kv.Key, kv=>kv.Value ? 1f : 0f );
		}
	}
}
