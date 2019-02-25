using BurningKnight.core.entity.level.rooms.shop;
using BurningKnight.core.entity.level.rooms.special;

namespace BurningKnight.core.entity.pool.room {
	public class ShopRoomPool : Pool<SpecialRoom>  {
		public static ShopRoomPool Instance = new ShopRoomPool();

		public ShopRoomPool() {
			Add(ClassicShopRoom.GetType(), 1);
			Add(QuadShopRoom.GetType(), 1);
			Add(GoldShopRoom.GetType(), 1);
		}
	}
}
