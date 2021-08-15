using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU
{
	public class ChunkEditor
	{
        public void Awake()
        {
            RoguePatcher patcher = new RoguePatcher(this);

            patcher.prefix(typeof(LevelEditor), nameof(LevelEditor.FixedUpdate), new Type[0] { });
            patcher.prefix(typeof(LevelEditor), nameof(LevelEditor.Update), new Type[0] { });
        }

        public static bool LevelEditor_FixedUpdate(LevelEditor __instance)
        {

            return true;
        }
        public static bool LevelEditor_Update(LevelEditor __instance)
		{

            return true;
        }
    }
}
