using System;
using System.Collections.Generic;


namespace Objectives.Definitions {
	public class FlatObjective : Objective {
		public delegate bool FlatObjectiveCondition( FlatObjective currentObjective );



		////////////////

		protected FlatObjectiveCondition Condition = null;



		////////////////

		public FlatObjective( string title, string description, FlatObjectiveCondition condition =null )
					: base( title, description ) {
			this.Condition = condition;
		}


		////////////////

		public sealed override IDictionary<string, float> GetCompletionStatus() {
			bool flag = this.GetCompletionFlagStatus();
			return new Dictionary<string, float> { { "", flag ? 1f : 0f } };
		}

		public virtual bool GetCompletionFlagStatus() {
			bool flag = this.Condition?.Invoke(this) ?? true;
			return flag;
		}
	}
}
