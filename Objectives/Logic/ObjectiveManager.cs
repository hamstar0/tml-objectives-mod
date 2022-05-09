using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using HUDElementsLib.Elements.Samples;
using Objectives.Definitions;


namespace Objectives.Logic {
	partial class ObjectiveManager : ILoadable {
		private int _UpdateTimer = 0;


		////////////////

		public ConcurrentDictionary<string, Objective> CurrentObjectives { get; }
			= new ConcurrentDictionary<string, Objective>();

		public IList<string> CurrentObjectiveOrder { get; }
			= new List<string>();

		public ConcurrentDictionary<string, int> CurrentObjectiveOrderByName { get; }
			= new ConcurrentDictionary<string, int>();

		public ConcurrentDictionary<string, ObjectivesAPI.SubscriptionEvent> Subscribers { get; }
			= new ConcurrentDictionary<string, ObjectivesAPI.SubscriptionEvent>();


		////////////////

		private CompletionStatHUD ObjectivesProgressHUD;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.LoadWidget();
			}
		}

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void Update() {
			if( this._UpdateTimer-- <= 0 ) {
				this._UpdateTimer = 60;

				this.Update_Intervals();
			}

			//

			this.UpdateWidget_Local_If();
		}

		internal void Update_Intervals() {
			foreach( Objective obj in this.CurrentObjectives.Values ) {
				if( obj.Update_Internal() ) {
					this.NotifySubscribers( obj, false );
				}
			}
		}


		////////////////

		public void PostDrawInterface( SpriteBatch spriteBatch ) {
			if( this.ObjectivesProgressHUD.IsMouseHovering ) {
				Main.LocalPlayer.mouseInterface = true;
			}
		}
	}
}
