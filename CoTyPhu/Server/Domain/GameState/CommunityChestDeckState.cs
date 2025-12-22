using Server.Domain.GameState.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class CommunityChestDeckState
    {
        public List<Card> communityChestDeck = CommunityChestDeckLoader.LoadDefaultDeck();
    }
}
