using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Objectives.Definitions {
	public abstract partial class Objective {
		protected bool HasAlerted = false;


		////////////////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		////

		public float PercentComplete { get; private set; }



		////////////////

		protected Objective( string title, string description ) {
			this.Title = title;
			this.Description = description;

			this.PercentComplete = this.ComputeCompletionPercent();

			if( this.PercentComplete >= 1f ) {
				this.HasAlerted = true;
			}
		}


		////////////////

		protected abstract IDictionary<string, float> ComputeCompletionStatus();


		private float ComputeCompletionPercent() {
			IDictionary<string, float> status = this.ComputeCompletionStatus();

			return status.Sum( kv => kv.Value ) / (float)status.Count;
		}


		////////////////

		internal void Update_Internal() {
			this.PercentComplete = this.ComputeCompletionPercent();

			if( !this.HasAlerted ) {
				if( this.PercentComplete >= 1f ) {
					Main.NewText( "Completed objective: "+this.Title, Color.Lime );
					this.HasAlerted = true;
				}
			}
		}
	}
}
