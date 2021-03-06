using BurningKnight.entity.item;
using BurningKnight.save;
using Lens.lightJson;

namespace BurningKnight.assets.items {
	public class ItemData {
		public JsonValue Root;
		public JsonValue Uses;
		public JsonValue Renderer;

		public bool AutoPickup;
		public bool Automatic;
		public bool SingleUse;
		public string Animation;
		public string Id;
		public float UseTime;
		public ItemType Type;
		public ItemQuality Quality;
		public Chance Chance;
		public int Pools;
		public bool Single = true;
		public bool Lockable;
		public bool Scourged;
		public int UnlockPrice = 1;

		public WeaponType WeaponType;

		public bool Unlocked => !Lockable || GlobalSave.IsTrue(Id);
	}
}