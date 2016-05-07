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

			asd.Engine.ChangeScene(new GameScene());

			while (asd.Engine.DoEvents())
			{
				asd.Engine.Update();
			}
			asd.Engine.Terminate();
		}
	}

	
	
	
}
