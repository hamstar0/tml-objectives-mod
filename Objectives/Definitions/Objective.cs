using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;


namespace Objectives.Definitions {
	public abstract partial class Objective {
		protected bool HasAlerted = false;


		////////////////

		public string Title { get; protected set; }

		public string Description { get; protected set; }

		public bool IsImportant { get; protected set; }

		////

		public float? PercentComplete { get; internal set; } = null;

		public bool? IsComplete => this.PercentComplete.HasValue
			? this.PercentComplete.Value >= 1f
			: (bool?)null;



		////////////////

		[Obsolete("use other constructor", true)]
		protected Objective( string title, string description ) {
			this.Title = title;
			this.Description = description;
		}

		protected Objective( string title, string description, bool isImportant ) {
			this.Title = title;
			this.Description = description;
			this.IsImportant = isImportant;
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


		public float ComputeCompletionPercent() {
			IDictionary<string, float> status = this.ComputeCompletionStatus();
			if( status.Count == 0 ) {
				return 0f;
			}

			return status.Sum( kv => kv.Value ) / (float)status.Count;
		}


		////////////////

		internal bool Update_Internal() {
			bool isNewlyCompleted = false;

			if( !this.IsComplete.HasValue || !this.IsComplete.Value ) {
				this.PercentComplete = this.ComputeCompletionPercent();
			}

			if( this.IsComplete.Value ) {
				var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
				isNewlyCompleted = myplayer.RecordCompletedObjective( this.Title );

				if( !this.HasAlerted ) {
					Main.NewText( "Completed objective: "+this.Title, Color.Lime );
					this.HasAlerted = true;
				}
			}

			return isNewlyCompleted;
		}
	}
}
