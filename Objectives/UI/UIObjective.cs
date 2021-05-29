using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsUI.Classes.UI.Elements;
using ModLibsUI.Classes.UI.Theme;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Draw;
using Objectives.Definitions;
using Objectives.Logic;
using ReLogic.Graphics;


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
			if( !mngr.CurrentObjectives.ContainsKey(this.Objective.Title) ) {
				return;
			}

			if( mngr.CurrentObjectives[ this.Objective.Title ].PercentComplete >= 1f ) {
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
			int thisOrder = mngr.CurrentObjectiveOrderByName[ this.Objective.Title ];
			int thatOrder = mngr.CurrentObjectiveOrderByName[ otherObjectiveElem.Objective.Title ];

			if( thisOrder > thatOrder ) {
				return -1;
			} else if( thisOrder < thatOrder ) {
				return 1;
			} else {
				return 0;
			}
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			float perc = this.Objective.PercentComplete.HasValue
				? this.Objective.PercentComplete.Value
				: 0f;

			CalculatedStyle style = this.GetDimensions();
			Rectangle rect = style.ToRectangle();

			if( perc > 0f && perc < 1f ) {
				rect.X += 8;
				rect.Y += 56;
				rect.Width -= 16;
				rect.Height -= 64;

				Rectangle percRect = rect;
				percRect.Width = (int)((float)percRect.Width * perc);

				DrawLibraries.DrawBorderedRect(
					sb: sb,
					bgColor: Color.Lime,
					borderColor: null,
					rect: percRect,
					borderWidth: 2
				);
				DrawLibraries.DrawBorderedRect(
					sb: sb,
					bgColor: null,
					borderColor: Color.White,
					rect: rect,
					borderWidth: 2
				);
			}

			if( perc >= 1f ) {
				Vector2 dim = Main.fontDeathText.MeasureString( "Complete" );

				sb.DrawString(
					spriteFont: Main.fontDeathText,
					text: "Complete",
					position: new Vector2(
						rect.X + (rect.Width / 2),
						rect.Y + (rect.Height / 2)
					),
					color: Color.Lime * 0.5f,
					rotation: 0f,
					origin: dim * 0.5f,
					scale: 1.25f,
					effects: SpriteEffects.None,
					layerDepth: 0f
				);
			}
		}
	}
}
