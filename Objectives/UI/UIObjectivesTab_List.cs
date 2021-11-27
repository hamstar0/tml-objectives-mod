using System;
using Terraria;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModUtilityPanels.Internals.UtilityPanels;
using Objectives.Definitions;


namespace Objectives.UI {
	partial class UIObjectivesTab : UIUtilityPanelsTab {
		public void AddObjective( Objective objective, int order ) {
			var objectiveItem = new UIObjective( objective );

			this.ObjectiveElemsList.Insert( order, objectiveItem );

			this.ObjectivesDisplayElem?.Clear();
			this.ObjectivesDisplayElem?.AddRange( this.ObjectiveElemsList );
			this.ObjectivesDisplayElem?.UpdateOrder();

			this.Recalculate();
		}

		public bool RemoveObjective( string title ) {
			bool found = false;

			int idx;
			for( idx=0; idx<this.ObjectiveElemsList.Count; idx++ ) {
				UIObjective obj = this.ObjectiveElemsList[idx] as UIObjective;

				if( obj.Objective.Title == title ) {
					found = true;
					break;
				}
			}

			if( found ) {
				UIElement item = this.ObjectiveElemsList[ idx ];
				this.ObjectiveElemsList.RemoveAt( idx );

				this.ObjectivesDisplayElem?.Remove( item );
				this.ObjectivesDisplayElem?.UpdateOrder();

				this.Recalculate();
			}

			return found;
		}

		public void ClearObjectives() {
			for( int idx = 0; idx<this.ObjectiveElemsList.Count; idx++ ) {
				UIObjective obj = this.ObjectiveElemsList[idx] as UIObjective;

				obj?.Parent?.RemoveChild( obj );
				obj?.Remove();
			}

			this.ObjectiveElemsList.Clear();

			this.ObjectivesDisplayElem?.Clear();
			this.ObjectivesDisplayElem?.UpdateOrder();

			this.Recalculate();
		}
	}
}
