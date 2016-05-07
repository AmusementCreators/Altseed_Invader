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
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/bomb.png");
			Position = firstPosition;
			CenterPosition = Texture.Size.To2DF() / 2;
		}

		protected override void OnUpdate()
		{
			Count++;
			if (Count == 10)
			{
				Dispose();
			}
		}
	}
}
