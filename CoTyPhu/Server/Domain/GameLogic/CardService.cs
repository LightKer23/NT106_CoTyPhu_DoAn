using Server.Domain.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameLogic
{
    public class CardService
    {
        private GameFlowService flowService;
        public void SellGetOutOfJailCard(PlayerState player)
        {
            bool hasGetOutOfJailCard = player.hasGetOutOfJailCard;
            if (hasGetOutOfJailCard)
            {
                player.hasGetOutOfJailCard = false;
                flowService.AddMoney(player, 200);
            }  
        }

    }
}
