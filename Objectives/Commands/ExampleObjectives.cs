using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.NPCs;
using ModLibsGeneral.Libraries.Players;
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
					isImportant: false,
					condition: ( obj ) => NPCLibraries.CurrentPlayerKillsOfBannerNpc(NPCID.BlueSlime) > 0
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Collect 50 Rings",
					description: "Wrong game.",
					isImportant: false,
					units: 50,
					condition: ( obj ) => (float)PlayerItemFinderLibraries.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.GoldRing },
						false
					) / 50f
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Order Pizza",
					description: "Can't be done.",
					isImportant: true
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Collect A Blueberry",
					description: "Don't ask.",
					isImportant: true,
					condition: ( obj ) => (float)PlayerItemFinderLibraries.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.BlueBerries },
						false
					) > 0
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new FlatObjective(
					title: "Craft A Molotov",
					description: "Viva la revolution!",
					isImportant: false,
					condition: ( obj ) => (float)PlayerItemFinderLibraries.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.MolotovCocktail },
						false
					) > 0
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Kill 10 Squids",
					description: "Thanks twerking Squidward. Some random squids must be punished now.",
					isImportant: true,
					units: 10,
					condition: ( obj ) => (float)NPCLibraries.CurrentPlayerKillsOfBannerNpc( NPCID.Squid ) / 10f
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);

			ObjectivesAPI.AddObjective(
				objective: new PercentObjective(
					title: "Collect 99 Dirt Blocks",
					description: "Mission impossible?",
					isImportant: false,
					units: 99,
					condition: ( obj ) => (float)PlayerItemFinderLibraries.CountTotalOfEach(
						Main.LocalPlayer,
						new HashSet<int> { ItemID.DirtBlock },
						false
					) / 99f
				),
				order: -1,
				alertPlayer: false,
				result: out _
			);
		}
	}
}
