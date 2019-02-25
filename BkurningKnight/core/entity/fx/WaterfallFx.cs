using BurningKnight.core.assets;
using BurningKnight.core.util;

namespace BurningKnight.core.entity.fx {
	public class WaterfallFx : Entity {
		protected void _Init() {
			{
				AlwaysActive = true;
			}
		}

		private float Size;
		private float Al;
		private float Vel;
		private float An;
		private float AnVel;
		private float Tar;
		private bool Grow = true;
		private float R;
		private float G;
		private float B;
		public bool Lava;

		public override Void Init() {
			base.Init();
			Size = 1;
			Al = Random.NewFloat(0.8f, 1f);
			Tar = Random.NewFloat(3, 6) * 1.2f;
			An = Random.NewFloat(360f);
			AnVel = Random.NewFloat(-1, 1) * 120;
			Vel = 0;

			if (Lava) {
				G = Random.NewFloat(0.4f, 0.6f);
				R = Random.NewFloat(0.8f, 1.2f);
			} else {
				R = 1;
				G = Random.NewFloat(1.4f, 1.8f);
				B = Random.NewFloat(G, 2f);
			}

		}

		public override Void Update(float Dt) {
			base.Update(Dt);

			if (Grow) {
				Size += Dt * 40;

				if (Size >= Tar) {
					Size = Tar;
					Grow = false;
				} 
			} else {
				Size -= Dt * 3;
			}


			Al -= Dt * 0.5f;
			Vel -= Dt * 20;
			Y += Dt * Vel;
			An += AnVel * Dt;

			if (B > 0.5f) {
				if (Lava) {
					R -= Dt * 1;
				} else {
					R -= Dt * 1.5f;
					B -= Dt * 1f;
				}


				G -= Dt * 1f;
			} 

			if (Al < 0 || Size < 0) {
				Done = true;
			} 
		}

		public override Void Render() {
			Graphics.StartAlphaShape();
			Graphics.Shape.SetColor(R, G, B, Al * 0.5f);
			Graphics.Shape.Rect(X - Size / 2, Y - Size / 2, Size / 2, Size / 2, Size, Size, 1, 1, An);
			Graphics.Shape.SetColor(R, G, B, Al);
			Graphics.Shape.Rect(X - Size / 2, Y - Size / 2, Size / 2, Size / 2, Size, Size, 0.5f, 0.5f, An);
			Graphics.EndAlphaShape();
		}

		public WaterfallFx() {
			_Init();
		}
	}
}
