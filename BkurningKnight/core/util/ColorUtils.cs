namespace BurningKnight.core.util {
	public class ColorUtils {
		public const Vector3 WHITE = new Vector3(1, 1, 1);

		public static Color HSV_to_RGB(float H, float S, float V) {
			int R;
			int G;
			int B;
			int I;
			float F;
			float P;
			float Q;
			float T;
			H = (float) Math.Max(0.0, Math.Min(360.0, H));
			S = (float) Math.Max(0.0, Math.Min(100.0, S));
			V = (float) Math.Max(0.0, Math.Min(100.0, V));
			S /= 100;
			V /= 100;
			H /= 60;
			I = (int) Math.Floor(H);
			F = H - I;
			P = V * (1 - S);
			Q = V * (1 - S * F);
			T = V * (1 - S * (1 - F));

			switch (I) {
				case 0: {
					R = Math.Round(255 * V);
					G = Math.Round(255 * T);
					B = Math.Round(255 * P);

					break;
				}

				case 1: {
					R = Math.Round(255 * Q);
					G = Math.Round(255 * V);
					B = Math.Round(255 * P);

					break;
				}

				case 2: {
					R = Math.Round(255 * P);
					G = Math.Round(255 * V);
					B = Math.Round(255 * T);

					break;
				}

				case 3: {
					R = Math.Round(255 * P);
					G = Math.Round(255 * Q);
					B = Math.Round(255 * V);

					break;
				}

				case 4: {
					R = Math.Round(255 * T);
					G = Math.Round(255 * P);
					B = Math.Round(255 * V);

					break;
				}

				default:{
					R = Math.Round(255 * V);
					G = Math.Round(255 * P);
					B = Math.Round(255 * Q);
				}
			}

			return new Color(R / 255.0f, G / 255.0f, B / 255.0f, 1);
		}
	}
}
