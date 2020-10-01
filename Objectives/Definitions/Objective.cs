using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.World;


namespace Objectives.Definitions {
	public abstract partial class Objective {
		protected bool HasAlerted = false;


		////////////////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		////

		public float PercentComplete { get; internal set; } = 0f;

		public bool IsComplete => this.PercentComplete >= 1f;



		////////////////

		protected Objective( string title, string description ) {
			this.Title = title;
			this.Description = description;
		}

		////

		internal void Initialize( bool isAlreadyComplete ) {
			this.PercentComplete = isAlreadyComplete ? 1f : this.ComputeCompletionPercent();

			if( this.PercentComplete >= 1f ) {
				this.HasAlerted = true;
			}
		}


		////////////////

		protected abstract IDictionary<string, float> ComputeCompletionStatus();


		private float ComputeCompletionPercent() {
			IDictionary<string, float> status = this.ComputeCompletionStatus();
			if( status.Count == 0 ) {
				return 0f;
			}

			return status.Sum( kv => kv.Value ) / (float)status.Count;
		}


		////////////////

		internal void Update_Internal() {
			if( !this.IsComplete ) {
				this.PercentComplete = this.ComputeCompletionPercent();

				var myplayer = Main.LocalPlayer.GetModPlayer<ObjectivesPlayer>();
				myplayer?.CompletedObjectivesPerWorld.Set2D(
					WorldHelpers.GetUniqueIdForCurrentWorld(true),
					this.Title
				);
			}

			if( this.IsComplete ) {
				if( !this.HasAlerted ) {
					Main.NewText( "Completed objective: "+this.Title, Color.Lime );
					this.HasAlerted = true;
				}
			}
		}
	}
}
