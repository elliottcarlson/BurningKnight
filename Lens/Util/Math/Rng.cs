﻿using System;

namespace Lens.Util.Math {
	public static class Rng {
		private static Random random = new Random();

		public static int Int() {
			return random.Next();
		}
		
		public static int Int(int min, int max) {
			return random.Next(min, max + 1);
		}

		public static float Float() {
			return (float) random.NextDouble();
		}

		public static float Float(float min, float max) {
			return (float) (random.NextDouble() * (max - min) + min);
		}

		public static double Double() {
			return random.NextDouble();
		}

		public static double Double(double min, double max) {
			return random.NextDouble() * (max - min) + min;
		}

		public static bool Bool() {
			return random.NextDouble() >= 0.5;
		}

		public static bool Chance(float chance) {
			return random.NextDouble() * 100 >= chance;
		}
	}
}