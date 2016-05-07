using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class FloatingObject : CollidableObject
	{
		private asd.Texture2D[] Animation = new asd.Texture2D[2];
		private int Count;
		private Random Rand;

		public FloatingObject(asd.Vector2DF firstPosition, int textureNo, Random random)
		{
			Animation[0] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + textureNo * 2 + ".png");
			Animation[1] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + (textureNo * 2 + 1) + ".png");
			Position = firstPosition;
			CenterPosition = Animation[0].Size.To2DF() / 2;
			Rand = random;
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

			int range = Layer.Objects.Count(x => x is FloatingObject) * 20;

			if (Rand.Next(range) == 0)
			{
				Bullet bullet = new Bullet(Position, false);
				Layer.AddObject(bullet);
			}
		}

		protected override void OnCollide(Bullet obj)
		{
			if (obj.OfPlayer)
			{
				ExplosionEffect bomb = new ExplosionEffect(Position);
				Layer.AddObject(bomb);
				Dispose();
				obj.Dispose();
			}
		}
	}
}
