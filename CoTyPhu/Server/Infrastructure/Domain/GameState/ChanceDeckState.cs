using Server.Domain.GameState.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class ChanceDeckState
    {
        public List<Card> chanceDeck = ChanceDeckLoader.LoadDefaultDeck();
    }
}
