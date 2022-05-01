using System;
using System.Collections.Generic;


namespace Objectives.Definitions {
	public class PercentObjective : Objective {
		public delegate float PercentObjectiveCondition( PercentObjective currentObjective );



		////////////////

		protected PercentObjectiveCondition Condition = null;

		protected int Units = -1;



		////////////////

		[Obsolete( "use other constructor", true )]
		public PercentObjective(
					string title,
					string description,
					int units = -1,
					PercentObjectiveCondition condition = null )
					: base( title, description, false ) {
			this.Units = units;
			this.Condition = condition;
		}

		public PercentObjective(
					string title,
					string description,
					bool isImportant,
					int units = -1,
					PercentObjectiveCondition condition = null )
					: base( title, description, isImportant ) {
			this.Units = units;
			this.Condition = condition;
		}


		////////////////

		protected sealed override IDictionary<string, float> ComputeCompletionStatus() {
			float percent = this.Condition?.Invoke( this ) ?? 1f;

			return new Dictionary<string, float> {
				{ "", percent }
			};
		}
	}
}
