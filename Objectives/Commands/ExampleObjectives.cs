using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.NPCs;
using Objectives.Definitions;


namespace Objectives.Commands {
	public class ExampleObjectivesCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "obj-example";
		/// @private
		public override string Usage => "/" + this.Command;
		/// @private
		public override string Description => "Creates a few example objectives.";


		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			string _;

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					//title: "Kill The Guide",
					//description: "He's let in one too many zombies",
					title: "Kill A Blue Slime",
					description: "What even is a slime?",
					condition: ( obj ) => NPCHelpers.CurrentPlayerKillsOfBannerNpc(NPCID.BlueSlime) > 0
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Collect 50 Rings",
					description: "Wrong game.",
					units: 50,
					condition: ( obj ) => (float)PlayerItemFinderHelpers.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.GoldRing },
						false
					) / 50f
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Order Pizza",
					description: "Can't be done."
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Collect A Blueberry",
					description: "Don't ask.",
					condition: ( obj ) => (float)PlayerItemFinderHelpers.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.BlueBerries },
						false
					) > 0
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Craft A Molotov",
					description: "Viva la revolution!",
					condition: ( obj ) => (float)PlayerItemFinderHelpers.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.MolotovCocktail },
						false
					) > 0
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Kill A Squid",
					description: "Thanks twerking Squidward. Some random squids must be punished now.",
					units: 10,
					condition: ( obj ) => (float)NPCHelpers.CurrentPlayerKillsOfBannerNpc( NPCID.Squid ) / 10f
				),
				order: -1,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Collect 99 Dirt Blocks",
					description: "Mission impossible?",
					units: 99,
					condition: ( obj ) => (float)PlayerItemFinderHelpers.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.DirtBlock },
						false
					) / 99f
				),
				order: -1,
				result: out _
			);
		}
	}
}
