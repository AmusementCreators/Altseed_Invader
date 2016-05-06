using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Game
{
	class Program
	{
		[System.STAThread]
		static void Main(string[] args)
		{
			asd.Engine.Initialize("Invader", 640, 640, new asd.EngineOption());

			ControlableObject player = new ControlableObject();
			asd.Engine.AddObject2D(player);

			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 6; y++)
				{
					FloatingObject enemy = new FloatingObject(new asd.Vector2DF(95 + x * 50.0f, 50 + y * 50.0f), y % 3);
					asd.Engine.AddObject2D(enemy);
				}
			}

			while (asd.Engine.DoEvents())
			{
				asd.Engine.Update();
			}
			asd.Engine.Terminate();
		}
	}

	abstract class CollidableObject : asd.TextureObject2D
	{
		protected bool IsCollide(CollidableObject obj)
		{
			if (obj == this) return false;
			if (obj == null) return false;

			return Position.X - Texture.Size.X / 2 < obj.Position.X &&
				obj.Position.X < Position.X + Texture.Size.X / 2 &&
				Position.Y - Texture.Size.Y / 2 < obj.Position.Y &&
				obj.Position.Y < Position.Y + Texture.Size.Y / 2;
		}

		protected virtual void OnCollide()
		{
		}
	}

	class ControlableObject : asd.TextureObject2D
	{
		public ControlableObject()
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/player.png");
			Position = new asd.Vector2DF(302, 600);
			CenterPosition = Texture.Size.To2DF() / 2;
		}

		protected override void OnUpdate()
		{
			asd.Vector2DF pos = Position;
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold)
			{
				pos.X -= 2.0f;
			}

			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold)
			{
				pos.X += 2.0f;
			}
			pos.X = asd.MathHelper.Clamp(pos.X, asd.Engine.WindowSize.X - Texture.Size.X, 0);
			Position = pos;

			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
			{
				Bullet bullet = new Bullet(Position);
				asd.Engine.AddObject2D(bullet);
			}
		}
	}

	class Bullet : CollidableObject
	{
		public Bullet(asd.Vector2DF firstPosition)
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/bullet.png");
			Position = firstPosition;
			CenterPosition = Texture.Size.To2DF() / 2;
		}

		protected override void OnUpdate()
		{
			Position = Position - new asd.Vector2DF(0.0f, 5.0f);
			if (Position.Y < 0)
			{
				Dispose();
			}
		}
	}

	class FloatingObject : CollidableObject
	{
		private asd.Texture2D[] Animation = new asd.Texture2D[2];
		private int Count;

		public FloatingObject(asd.Vector2DF firstPosition, int textureNo)
		{
			Animation[0] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + textureNo * 2 + ".png");
			Animation[1] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + (textureNo * 2 + 1) + ".png");
			Position = firstPosition;
			CenterPosition = Animation[0].Size.To2DF() / 2;
		}

		protected override void OnUpdate()
		{
			if (Count % 120 == 0)
			{
				Texture = Animation[0];
			}
			else if (Count % 120 == 60)
			{
				Texture = Animation[1];
			}

			asd.Vector2DF pos = Position;
			int phase = Count % 260;
			if (phase < 50)
			{
				pos.X += 1.0f;
			}
			else if (phase < 80)
			{
				pos.Y += 1.0f;
			}
			else if (phase < 180)
			{
				pos.X -= 1.0f;
			}
			else if (phase < 210)
			{
				pos.Y += 1.0f;
			}
			else
			{
				pos.X += 1.0f;
			}
			Position = pos;
			Count++;

			CheckCollide();
		}

		protected override void OnCollide()
		{
			Dispose();
		}

		private void CheckCollide()
		{
			foreach (var o in Layer.Objects)
			{
				if (IsCollide(o as CollidableObject))
				{
					OnCollide();
				}
			}
		}
	}

}
