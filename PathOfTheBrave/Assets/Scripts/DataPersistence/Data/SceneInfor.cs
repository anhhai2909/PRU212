using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataPersistence.Data
{
    public class SceneInfor
    {
        public int sceneIndex { get;set; }

        public string sceneName { get;set; }

        public bool isCompleted { get;set; }

        public SceneInfor()
        {
        }

        public SceneInfor(int sceneIndex, string sceneName, bool isCompleted)
        {
            this.sceneIndex = sceneIndex;
            this.sceneName = sceneName;
            this.isCompleted = isCompleted;
        }
    }
}
