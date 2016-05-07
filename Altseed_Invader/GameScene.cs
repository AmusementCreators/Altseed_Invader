using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class GameScene : asd.Scene
	{
		public GameScene()
		{
			asd.Layer2D layer = new asd.Layer2D();

			ControlableObject player = new ControlableObject();
			layer.AddObject(player);

			Random random = new Random();
			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 6; y++)
				{
					FloatingObject enemy = new FloatingObject(new asd.Vector2DF(95 + x * 50.0f, 50 + y * 50.0f), y % 3, random);
					layer.AddObject(enemy);
				}
			}

			AddLayer(layer);
		}
	}
}
