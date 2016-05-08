using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	abstract class CollidableObject : asd.TextureObject2D
	{
		protected bool IsCollide(Bullet obj)
		{
			return Position.X - Texture.Size.X / 2 < obj.Position.X &&
				obj.Position.X < Position.X + Texture.Size.X / 2 &&
				Position.Y - Texture.Size.Y / 2 < obj.Position.Y &&
				obj.Position.Y < Position.Y + Texture.Size.Y / 2;
		}

		protected abstract void OnCollide(Bullet obj);

		protected void CheckCollide()
		{
			foreach (var o in Layer.Objects)
			{
				Bullet bullet = o as Bullet;
				if (bullet != null && IsCollide(bullet))
				{
					OnCollide(bullet);
				}
			}
		}

	}
}
