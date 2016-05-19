using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class ExplosionEffect : asd.TextureObject2D
	{
		private int Count;

		public ExplosionEffect(asd.Vector2DF firstPosition)
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/explosion.png");
			Position = firstPosition;
			CenterPosition = new asd.Vector2DF(16, 16);
		}

		protected override void OnUpdate()
		{

			if (Count == 0) Src = new asd.RectF(0, 0, 32, 32);
			else if (Count == 5) Src = new asd.RectF(32, 0, 32, 32);
			else if (Count == 10) Src = new asd.RectF(64, 0, 32, 32);
			else if (Count == 15) Src = new asd.RectF(96, 0, 32, 32);
			else if (Count == 20) Dispose();
			Count++;
		}
	}
}
