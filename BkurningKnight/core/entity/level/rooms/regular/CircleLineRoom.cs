using BurningKnight.core.entity.level.features;
using BurningKnight.core.entity.level.painters;
using BurningKnight.core.util;
using BurningKnight.core.util.geometry;

namespace BurningKnight.core.entity.level.rooms.regular {
	public class CircleLineRoom : RegularRoom {
		public override Void Paint(Level Level) {
			foreach (LDoor Door in this.Connected.Values()) {
				Door.SetType(LDoor.Type.REGULAR);
			}

			byte Floor = Terrain.RandomFloor();
			byte Fl = Random.Chance(30) ? Terrain.WALL : (Random.Chance(50) ? Terrain.CHASM : Terrain.LAVA);
			Painter.Fill(Level, this, Terrain.WALL);

			if (Fl == Terrain.LAVA) {
				Floor = Random.Chance(40) ? Terrain.WATER : Terrain.DIRT;
			} 

			Painter.Fill(Level, this, 1, Terrain.RandomFloor());
			Painter.Fill(Level, this, 1, Floor);
			Painter.FillEllipse(Level, this, 2, Fl);
			Painter.FillEllipse(Level, this, 3, Floor);
			byte F = Floor;
			bool S = false;

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(this.GetWidth() / 2 + this.Left, this.Top + 2), F);
				S = true;
			} 

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(this.GetWidth() / 2 + this.Left, this.Bottom - 2), F);
				S = true;
			} 

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(this.Left + 2, this.GetHeight() / 2 + this.Top), F);
				S = true;
			} 

			if (Random.Chance(50) || !S) {
				Painter.Set(Level, new Point(this.Right - 2, this.GetHeight() / 2 + this.Top), F);
			} 

			if (Random.Chance(50)) {
				Painter.FillEllipse(Level, this, 5, Fl);
			} else if (Random.Chance(50)) {
				F = Terrain.RandomFloor();

				if (Fl == Terrain.LAVA) {
					F = Terrain.DIRT;
				} 

				Painter.FillEllipse(Level, this, 5, F);
			} 

			if (Random.Chance(50)) {
				byte Flr = Terrain.RandomFloor();

				if (Random.Chance(50)) {
					Painter.Fill(Level, new Rect(this.Left + 1, this.Top + 1, this.Left + 4, this.Top + 4), Flr);
					Painter.Fill(Level, new Rect(this.Right - 3, this.Top + 1, this.Right, this.Top + 4), Flr);
					Painter.Fill(Level, new Rect(this.Left + 1, this.Bottom - 3, this.Left + 4, this.Bottom), Flr);
					Painter.Fill(Level, new Rect(this.Right - 3, this.Bottom - 3, this.Right, this.Bottom), Flr);
				} else {
					Painter.FillEllipse(Level, new Rect(this.Left + 1, this.Top + 1, this.Left + 4, this.Top + 4), Flr);
					Painter.FillEllipse(Level, new Rect(this.Right - 3, this.Top + 1, this.Right, this.Top + 4), Flr);
					Painter.FillEllipse(Level, new Rect(this.Left + 1, this.Bottom - 3, this.Left + 4, this.Bottom), Flr);
					Painter.FillEllipse(Level, new Rect(this.Right - 3, this.Bottom - 3, this.Right, this.Bottom), Flr);
				}

			} 
		}

		public override int GetMinWidth() {
			return 8;
		}

		public override int GetMinHeight() {
			return 8;
		}
	}
}
