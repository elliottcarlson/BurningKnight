using System;
using BurningKnight.assets;
using BurningKnight.entity.component;
using BurningKnight.entity.creature;
using BurningKnight.physics;
using BurningKnight.util;
using ImGuiNET;
using Lens.entity;
using Lens.util.file;
using Lens.util.tween;
using Microsoft.Xna.Framework;

namespace BurningKnight.level.entities.chest {
	public class Chest : Prop, CollisionFilterEntity {
		protected bool open;
		protected internal float Scale = 1;
		private bool transitioning;

		public bool CanOpen = true;
		public bool Empty;
		
		protected virtual Rectangle GetCollider() {
			return new Rectangle(0, (int) (5 * Scale), (int) (Math.Max(1, 17 * Scale)), (int) (Math.Max(1, 8 * Scale)));
		}
		
		protected virtual BodyComponent CreateBody() {
			var collider = GetCollider();
			return new RectBodyComponent(collider.X, collider.Y, collider.Width, collider.Height);
		}

		protected virtual float GetWidth() {
			return 16;
		}

		protected virtual float GetHeight() {
			return 13;
		}

		public override void AddComponents() {
			base.AddComponents();

			AlwaysActive = true;
			Width = GetWidth() * Scale;
			Height = GetHeight() * Scale;

			AddGraphics();
			AddComponent(new SensorBodyComponent(-2, -2, Width + 4, Height + 4));
			AddComponent(new DropsComponent());
			AddComponent(new ShadowComponent());
			AddComponent(new RoomComponent());
			AddComponent(new AudioEmitterComponent());
			
			AddComponent(new InteractableComponent(Interact) {
				CanInteract = e => CanOpen && ShouldOpen()
			});
			
			AddTag(Tags.Chest);
			AddTag(Tags.Item);
			
			DefineDrops();
		}

		protected virtual bool ShouldOpen() {
			return !open;
		}

		protected virtual void AddGraphics() {
			AddComponent(new InteractableSliceComponent("props", GetSprite()));
		}

		protected virtual void DefineDrops() {
			var p = GetPool();

			if (p != null) {
				GetComponent<DropsComponent>().Add(p);
			}
		}

		public override void PostInit() {
			base.PostInit();
			AddComponent(CreateBody());

			if (open) {
				UpdateSprite();
			}

			var body = GetComponent<RectBodyComponent>().Body;

			body.LinearDamping = 100;
			body.Mass = 1000000;

			GetComponent<RectBodyComponent>().KnockbackModifier = 0.1f;
			Animate();
		}
		
		protected virtual void Animate() {
			var a = GetComponent<InteractableSliceComponent>();

			a.Scale.X = 0.6f * Scale;
			a.Scale.Y = 1.7f * Scale;
					
			Tween.To(1.8f * Scale, a.Scale.X, x => a.Scale.X = x, 0.15f);
			Tween.To(0.2f * Scale, a.Scale.Y, x => a.Scale.Y = x, 0.15f).OnEnd = () => {
				Tween.To(Scale, a.Scale.X, x => a.Scale.X = x, 0.2f);
				Tween.To(Scale, a.Scale.Y, x => a.Scale.Y = x, 0.2f);
			};
		}

		public virtual string GetSprite() {
			return "chest";
		}

		public virtual string GetPool() {
			return null;
		}

		protected virtual void UpdateSprite(bool open = true) {
			GetComponent<InteractableSliceComponent>().Sprite = CommonAse.Props.GetSlice($"{GetSprite()}{(open ? "_open" : "")}");
		}

		public void Open(Entity who) {
			if (open || transitioning) {
				return;
			}

			open = true;
			transitioning = true;
			
			Animate(() => {
				transitioning = false;
				
				UpdateSprite();
				SpawnDrops();
				GetComponent<AudioEmitterComponent>().EmitRandomized("level_chest_open");
			});

			HandleEvent(new OpenedEvent {
				Chest = this,
				Who = who
			});
		}

		public void Close() {
			if (!open) {
				return;
			}

			transitioning = true;
			
			Animate(() => {
				open = false;
				transitioning = false;
				UpdateSprite(false);

				// GetComponent<AudioEmitterComponent>().EmitRandomized("level_chest_open");
			});
		}

		protected virtual void Animate(Action callback) {
			var a = GetComponent<InteractableSliceComponent>();
					
			Tween.To(1.8f * Scale, a.Scale.X, x => a.Scale.X = x, 0.2f);
			Tween.To(0.2f * Scale, a.Scale.Y, x => a.Scale.Y = x, 0.2f).OnEnd = () => {
				callback();
				
				Tween.To(0.6f * Scale, a.Scale.X, x => a.Scale.X = x, 0.1f);
				Tween.To(1.7f * Scale, a.Scale.Y, x => a.Scale.Y = x, 0.1f).OnEnd = () => {
					Tween.To(Scale, a.Scale.X, x => a.Scale.X = x, 0.2f);
					Tween.To(Scale, a.Scale.Y, x => a.Scale.Y = x, 0.2f);
				};
			};
		}
		
		protected virtual void SpawnDrops() {
			if (!Empty) {
				Empty = true;
				GetComponent<DropsComponent>().SpawnDrops();
			}
		}

		protected virtual bool Interact(Entity entity) {
			if (open || !CanOpen) {
				return true;
			}

			if (TryOpen(entity)) {
				Open(entity);
				return true;
			} else {
				AnimationUtil.ActionFailed();
			}
			
			return false;
		}

		protected virtual bool TryOpen(Entity entity) {
			return true;
		}

		public override void Save(FileWriter stream) {
			base.Save(stream);
			
			stream.WriteBoolean(open);
			stream.WriteFloat(Scale);
			stream.WriteBoolean(Empty);
		}

		public override void Load(FileReader stream) {
			base.Load(stream);
			
			open = stream.ReadBoolean();
			Scale = stream.ReadFloat();
			Empty = stream.ReadBoolean();
		}

		public override void RenderImDebug() {
			base.RenderImDebug();

			ImGui.Checkbox("Empty", ref Empty);
			ImGui.Checkbox("Can open", ref CanOpen);
		}

		public class OpenedEvent : Event {
			public Chest Chest;
			public Entity Who;
		}

		public virtual bool ShouldCollide(Entity entity) {
			return !(entity is Creature c && c.InAir());
		}
	}
}