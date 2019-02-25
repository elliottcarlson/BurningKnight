using BurningKnight.core.assets;
using BurningKnight.core.util;
using BurningKnight.core.util.file;

namespace BurningKnight.core.entity.level.entities {
	public class Stone : SolidProp {
		private bool Flip;
		private string Old;

		public override Void Init() {
			if (this.Sprite == null) {
				return;
			} 

			Old = this.Sprite;
			this.Flip = Random.Chance(50);
			this.CreateSprite(Old);
		}

		private Void CreateSprite(string Sprite) {
			switch (Sprite) {
				case "prop_stone": {
					this.Sprite = "props-rock_a";
					Collider = new Rectangle(4, 6, 14 - 4 * 2, 12 - 6 - 2);
					W = 14;
					H = 12;

					break;
				}

				case "prop_high_stone": {
					this.Sprite = "props-rock_b";
					Collider = new Rectangle(4, 8, 14 - 4 * 2, 21 - 8 * 2);
					W = 14;
					H = 21;

					break;
				}

				case "prop_big_stone": {
					this.Sprite = "props-rock_c";
					Collider = new Rectangle(4, 8, 28 - 4 * 2, 23 - 8 * 2);
					W = 28;
					H = 23;

					break;
				}
			}

			base.Init();
			CreateCollider();
		}

		public override Void Save(FileWriter Writer) {
			base.Save(Writer);
			Writer.WriteString(this.Old);
			Writer.WriteBoolean(Flip);
		}

		public override Void Load(FileReader Reader) {
			base.Load(Reader);
			this.Old = Reader.ReadString();
			Flip = Reader.ReadBoolean();
			CreateSprite(Old);
		}

		public override Void Render() {
			Graphics.Render(Region, this.X + Region.GetRegionWidth() / 2, this.Y + Region.GetRegionHeight() / 2, 0, Region.GetRegionWidth() / 2, Region.GetRegionHeight() / 2, false, false, Flip ? -1 : 1, 1);
		}

		public override Void RenderShadow() {
			Graphics.Shadow(this.X, this.Y + 2, this.W, this.H);
		}
	}
}
