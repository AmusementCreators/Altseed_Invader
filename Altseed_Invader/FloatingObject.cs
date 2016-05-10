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
		private IEnumerator<asd.Vector2DF> Behavior;

		public FloatingObject(asd.Vector2DF firstPosition, int textureNo, Random random)
		{
			Animation[0] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + textureNo * 2 + ".png");
			Animation[1] = asd.Engine.Graphics.CreateTexture2D("Resources/enemy" + (textureNo * 2 + 1) + ".png");
			Position = firstPosition;
			CenterPosition = Animation[0].Size.To2DF() / 2;
			Rand = random;
			Behavior = CreateMovementIterator();
		}

		protected IEnumerator<asd.Vector2DF> CreateMovementIterator()
		{
			while (true)
			{
				for (int i = 0; i < 50; i++)
				{
					yield return new asd.Vector2DF(1.0f, 0.0f);
				}
				for (int i = 0; i < 30; i++)
				{
					yield return new asd.Vector2DF(0.0f, 1.0f);
				}
				for (int i = 0; i < 100; i++)
				{
					yield return new asd.Vector2DF(-1.0f, 0.0f);
				}
				for (int i = 0; i < 30; i++)
				{
					yield return new asd.Vector2DF(0.0f, 1.0f);
				}
				for (int i = 0; i < 50; i++)
				{
					yield return new asd.Vector2DF(1.0f, 0.0f);
				}
			}
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

			Position = Position + Behavior.Current;
			Behavior.MoveNext();
			CheckCollide();

			int range = Layer.Objects.Count(x => x is FloatingObject) * 20;

			if (Rand.Next(range) == 0)
			{
				Bullet bullet = new Bullet(Position, false);
				Layer.AddObject(bullet);
			}

			Count++;
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
