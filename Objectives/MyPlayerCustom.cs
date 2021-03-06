﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.World;


namespace Objectives {
	partial class ObjectivesCustomPlayer : CustomPlayerData {
		private Dictionary<string, HashSet<string>> CompletedObjectivesPerWorld = new Dictionary<string, HashSet<string>>();



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( !isCurrentPlayer ) {
				return;
			}

			if( data != null ) {
//LogLibraries.Log( "ENTER "+string.Join(", ", this.CompletedObjectivesPerWorld.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
				this.CompletedObjectivesPerWorld = ((JObject)data)
					.ToObject<Dictionary<string, string[]>>()
					.ToDictionary( kv=>kv.Key, kv=>new HashSet<string>( kv.Value ) );
			}
//else { LogLibraries.Log( "ENTER!" ); }
		}

		protected override object OnExit() {
			var data = new Dictionary<string, HashSet<string>>( this.CompletedObjectivesPerWorld );

			if( Main.netMode != NetmodeID.Server ) {
				ObjectivesAPI.ClearObjectives( false );
			}

			//LogLibraries.Log( "EXIT "+string.Join(", ", data.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
			return data;
		}


		////////////////

		public bool IsObjectiveByNameComplete( string objectiveTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.CompletedObjectivesPerWorld.ContainsKey(worldUid) ) {
				return false;
			}
			return this.CompletedObjectivesPerWorld[ worldUid ].Contains( objectiveTitle );
		}


		public bool RecordCompletedObjective( string objectiveTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.CompletedObjectivesPerWorld.ContainsKey( worldUid ) ) {
				this.CompletedObjectivesPerWorld[ worldUid ] = new HashSet<string>();
			}

			return this.CompletedObjectivesPerWorld[worldUid].Add( objectiveTitle );
//LogLibraries.Log( "RECORD "+worldUid+", "+objectiveTitle+", this: "+this.GetHashCode());
//LogLibraries.Log( "RECORD "+string.Join(", ", this.CompletedObjectivesPerWorld.Select(kv=>kv.Key+":"+string.Join(",",kv.Value))) );
		}

		
		public bool ForgetCompletedObjective( string objectiveTitle ) {
			string worldUid = WorldIdentityLibraries.GetUniqueIdForCurrentWorld( true );

			if( !this.CompletedObjectivesPerWorld.ContainsKey( worldUid ) ) {
				return false;
			}

			return this.CompletedObjectivesPerWorld[ worldUid ].Remove( objectiveTitle );
		}


		public void ClearCompletedObjectives() {
//LogLibraries.Log( "CLEAR" );
			this.CompletedObjectivesPerWorld.Clear();
		}
	}
}
