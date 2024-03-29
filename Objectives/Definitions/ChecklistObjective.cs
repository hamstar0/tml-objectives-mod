﻿using System;
using System.Collections.Generic;
using System.Linq;
using ModLibsCore.Classes.Errors;


namespace Objectives.Definitions {
	public class ChecklistObjective : Objective {
		public delegate IDictionary<string, bool> ChecklistObjectiveCondition( ChecklistObjective currentObjective );



		////////////////

		protected ChecklistObjectiveCondition Condition = null;


		////////////////

		public IDictionary<string, bool> CheckList { get; private set; }



		////////////////

		[Obsolete( "use other constructor", true )]
		public ChecklistObjective(
					string title, string description,
					ChecklistObjectiveCondition condition = null )
					: base( title, description, false ) {
			this.Condition = condition;
		}

		public ChecklistObjective(
					string title, string description,
					bool isImportant,
					ChecklistObjectiveCondition condition = null )
					: base( title, description, isImportant ) {
			this.Condition = condition;
		}


		////////////////

		protected sealed override IDictionary<string, float> ComputeCompletionStatus() {
			this.CheckList = this.Condition?.Invoke( this )
				?? new Dictionary<string, bool>();

			return this.CheckList.ToDictionary( kv=>kv.Key, kv=>kv.Value ? 1f : 0f );
		}
	}
}
