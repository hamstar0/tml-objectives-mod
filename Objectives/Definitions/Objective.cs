using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;


namespace Objectives.Definitions {
	public abstract class Objective {
		protected bool HasAlerted = false;


		////////////////

		public string Title { get; protected set; }

		public string Description { get; protected set; }



		////////////////
		
		protected Objective( string title, string description ) {
			this.Title = title;
			this.Description = description;
		}


		////////////////

		public abstract IDictionary<string, float> GetCompletionStatus();


		////////////////
		
		public bool IsComplete() {
			return !this.GetCompletionStatus().Any( kv => kv.Value < 1f );
		}

		public float PercentComplete() {
			IDictionary<string, float> status = this.GetCompletionStatus();
			return status.Sum( kv => kv.Value ) / (float)status.Count;
		}


		////////////////

		internal void Update_Internal() {
			if( !this.HasAlerted ) {
				if( this.IsComplete() ) {
					Main.NewText( "Completed objective: "+this.Title, Color.Lime );
					this.HasAlerted = true;
				}
			}
		}
	}
}
