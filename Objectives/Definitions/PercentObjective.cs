using System;
using System.Collections.Generic;


namespace Objectives.Definitions {
	public class PercentObjective : Objective {
		public delegate float PercentObjectiveCondition( PercentObjective currentObjective );



		////////////////

		protected PercentObjectiveCondition Condition = null;

		protected int Units = -1;



		////////////////

		public PercentObjective(
					string title,
					string description,
					int units = -1,
					PercentObjectiveCondition condition = null )
					: base( title, description ) {
			this.Units = units;
			this.Condition = condition;
		}


		////////////////

		protected sealed override IDictionary<string, float> ComputeCompletionStatus() {
			float percent = this.Condition?.Invoke( this ) ?? 1f;

			return new Dictionary<string, float> { { "", percent } };
		}
	}
}
