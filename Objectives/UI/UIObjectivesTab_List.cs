using System;
using Terraria;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Internals.ControlPanel;
using Objectives.Definitions;


namespace Objectives.UI {
	partial class UIObjectivesTab : UIControlPanelTab {
		public void AddObjective( Objective objective, int order ) {
			var objectiveItem = new UIObjective( objective );

			this.ObjectiveElemsList.Insert( order, objectiveItem );

			this.ObjectivesDisplayElem?.Clear();
			this.ObjectivesDisplayElem?.AddRange( this.ObjectiveElemsList );
			this.ObjectivesDisplayElem?.UpdateOrder();

			this.Recalculate();
		}

		public void RemoveObjective( string title ) {
			int idx;
			for( idx=0; idx<this.ObjectiveElemsList.Count; idx++ ) {
				UIObjective obj = this.ObjectiveElemsList[idx] as UIObjective;

				if( obj.Objective.Title == title ) {
					break;
				}
			}

			UIElement item = this.ObjectiveElemsList[ idx ];
			this.ObjectiveElemsList.RemoveAt( idx );

			this.ObjectivesDisplayElem?.Remove( item );
			this.ObjectivesDisplayElem?.UpdateOrder();

			this.Recalculate();
		}

		public void ClearObjectives() {
			for( int idx = 0; idx<this.ObjectiveElemsList.Count; idx++ ) {
				UIObjective obj = this.ObjectiveElemsList[idx] as UIObjective;

				obj.Parent.RemoveChild( obj );
				obj.Remove();
			}

			this.ObjectiveElemsList.Clear();

			this.ObjectivesDisplayElem?.Clear();
			this.ObjectivesDisplayElem?.UpdateOrder();

			this.Recalculate();
		}
	}
}
