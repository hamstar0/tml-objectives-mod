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

		public ChecklistObjective(
					string title, string description,
					ChecklistObjectiveCondition condition = null )
					: base( title, description ) {
			this.Condition = condition;
		}


		////////////////

		public sealed override IDictionary<string, float> GetCompletionStatus() {
			IDictionary<string, bool> checklist = this.GetCompletionChecklistStatus();
			return checklist.ToDictionary( kv=>kv.Key, kv=>kv.Value ? 1f : 0f );
		}

		public virtual IDictionary<string, bool> GetCompletionChecklistStatus() {
			IDictionary<string, bool> checklist = this.Condition?.Invoke( this ) ?? new Dictionary<string, bool>();
			return checklist;
		}
	}
}
