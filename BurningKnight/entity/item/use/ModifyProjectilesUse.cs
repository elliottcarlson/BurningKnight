using System;
using BurningKnight.entity.events;
using ImGuiNET;
using Lens.entity;
using Lens.lightJson;

namespace BurningKnight.entity.item.use {
	public class ModifyProjectilesUse : ItemUse {
		public float Scale;
		public float Damage;

		public override void Use(Entity entity, Item item) {
			
		}

		public override bool HandleEvent(Event e) {
			if (e is ProjectileCreatedEvent pce) {
				pce.Projectile.Scale *= Scale;
				pce.Projectile.Damage = (int) Math.Round(pce.Projectile.Damage * Damage);
			}
			
			return base.HandleEvent(e);
		}

		public override void Setup(JsonValue settings) {
			base.Setup(settings);
			Scale = settings["amount"].Number(1);
			Damage = settings["damage"].Number(1);
		}
		
		public static void RenderDebug(JsonValue root) {
			var val = root["amount"].Number(1);

			if (ImGui.InputFloat("Scale Modifier", ref val)) {
				root["amount"] = val;
			}
			
			val = root["damage"].Number(1);

			if (ImGui.InputFloat("Damage Modifier", ref val)) {
				root["damage"] = val;
			}
		}
	}
}