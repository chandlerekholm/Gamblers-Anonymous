using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
    public class Token
    {

        public string tokenType { get; set; } 
        public string tokenOwner {  get; set; }

        public Token(string type)
        {
            tokenType = type;
            tokenOwner = "unassigned";
        }

        public void RotateToken(Player player)
        {
            this.tokenOwner = player.playerName;
            Console.WriteLine(player.playerName + " is the " + this.tokenType);
            // TODO: Implement this method
        }

    }
}
