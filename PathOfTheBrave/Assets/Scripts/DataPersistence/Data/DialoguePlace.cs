using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataPersistence.Data
{
    public class DialoguePlace
    {
        public float _xPosition { get; set; }

        public float _yPosition { get; set; }

        public List<Dialogue> _dialogues {get; set;}

        public DialoguePlace()
        {
        }

        public DialoguePlace(float xPosition, float yPosition, List<Dialogue> dialogues)
        {
            this._xPosition = xPosition;
            this._yPosition = yPosition;
            this._dialogues = dialogues;
        }
    }
}
