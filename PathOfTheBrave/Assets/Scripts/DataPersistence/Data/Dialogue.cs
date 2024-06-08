using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataPersistence.Data
{
    public class Dialogue
    {
        public string _characterName { get; set; }

        public string _characterText { get; set; }

        public bool _isMainCharacter { get; set; }

        public Dialogue()
        {
        }

        public Dialogue(string characterName, string characterText, bool isMainCharacter)
        {
            _characterName = characterName;
            _characterText = characterText;
            _isMainCharacter = isMainCharacter;
        }
    }
}
