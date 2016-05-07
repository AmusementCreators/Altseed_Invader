using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class Bullet : asd.TextureObject2D
	{
		public bool OfPlayer;
		public Bullet(asd.Vector2DF firstPosition, bool ofPlayer)
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/bullet.png");
			Position = firstPosition;
			CenterPosition = Texture.Size.To2DF() / 2;
			OfPlayer = ofPlayer;
		}

		protected override void OnUpdate()
		{
			Position = Position - new asd.Vector2DF(0.0f, (OfPlayer ? 5.0f : -5.0f));
			if (Position.Y < 0)
			{
				Dispose();
			}
		}
	}
}
