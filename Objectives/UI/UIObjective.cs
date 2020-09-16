using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.UI.Theme;
using Objectives.Definitions;
using Objectives.Logic;


namespace Objectives.UI {
	partial class UIObjective : UIThemedPanel {
		private UIThemedText TitleElem;
		private UIThemedText DescriptionElem;

		public Objective Objective { get; private set; }



		////////////////

		public UIObjective( Objective objective ) : base( UITheme.Vanilla, false ) {
			this.Objective = objective;

			this.Width.Set( 0f, 1f );
			this.Height.Set( 80f, 0f );

			//

			this.TitleElem = new UIThemedText( this.Theme, false, this.Objective.Title, 1.1f );
			this.TitleElem.TextColor = Color.Yellow;
			this.TitleElem.Width.Set( 0f, 1f );
			this.Append( this.TitleElem );

			this.DescriptionElem = new UIThemedText( this.Theme, false, this.Objective.Description, 0.8f );
			this.DescriptionElem.TextColor = Color.White;
			this.DescriptionElem.Top.Set( 24f, 0f );
			this.DescriptionElem.Width.Set( 0f, 1f );
			this.Append( this.DescriptionElem );
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			var mngr = ModContent.GetInstance<ObjectiveManager>();
			if( !mngr.Objectives.ContainsKey(this.Objective.Title) ) {
				return;
			}

			if( mngr.Objectives[ this.Objective.Title ].IsComplete() ) {
				this.TitleElem.TextColor = Color.Gray;
				this.DescriptionElem.TextColor = Color.Gray;
			} else {
				this.TitleElem.TextColor = Color.Yellow;
				this.DescriptionElem.TextColor = Color.White;
			}
		}


		////////////////

		public override int CompareTo( object obj ) {
			var otherObjectiveElem = obj as UIObjective;
			if( otherObjectiveElem == null ) {
				return -1;
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();
			int thisOrder = mngr.ObjectiveOrderByName[ this.Objective.Title ];
			int thatOrder = mngr.ObjectiveOrderByName[ otherObjectiveElem.Objective.Title ];

			if( thisOrder > thatOrder ) {
				return -1;
			} else if( thisOrder < thatOrder ) {
				return 1;
			} else {
				return 0;
			}
		}
	}
}
