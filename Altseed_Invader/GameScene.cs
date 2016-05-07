using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class GameScene : asd.Scene
	{
		asd.Layer2D Layer = new asd.Layer2D();

		public GameScene()
		{


			ControlableObject player = new ControlableObject();
			Layer.AddObject(player);

			Random random = new Random();
			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 6; y++)
				{
					FloatingObject enemy = new FloatingObject(new asd.Vector2DF(95 + x * 50.0f, 50 + y * 50.0f), y % 3, random);
					Layer.AddObject(enemy);
				}
			}

			AddLayer(Layer);
		}

		protected override void OnUpdating()
		{
			if (!Layer.Objects.Any(x => x is FloatingObject))
			{
				asd.Engine.ChangeSceneWithTransition(new GameOverScene("CONGRATULATIONS"),new asd.TransitionFade(1.0f,1.0f));
			}
		}
	}
}
