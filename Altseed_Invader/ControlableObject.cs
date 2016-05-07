using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class ControlableObject : CollidableObject
	{
		private int Count;
		private int LastShoot;
		public ControlableObject()
		{
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/player.png");
			Position = new asd.Vector2DF(302, 600);
			CenterPosition = Texture.Size.To2DF() / 2;
		}

		protected override void OnUpdate()
		{
			Count++;
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

			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push && Count >= LastShoot + 10)
			{
				Bullet bullet = new Bullet(Position, true);
				Layer.AddObject(bullet);
				LastShoot = Count;
			}
			CheckCollide();
		}

		protected override void OnCollide(Bullet obj)
		{
			if (!obj.OfPlayer)
			{
				ExplosionEffect bomb = new ExplosionEffect(Position);
				Layer.AddObject(bomb);
				Dispose();
				asd.Engine.ChangeSceneWithTransition(new GameOverScene("GAMEOVER"),new asd.TransitionFade(1.0f,1.0f));
			}
		}
	}
}
