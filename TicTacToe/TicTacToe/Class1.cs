using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    // Värdet på vardera box i spelet

    public enum MarkType
    {
        // Boxen har inte blivit i klickad än
        Free,

        // Boxen är en O
        Nought,

        // Boxen är ett X
        Cross
    }
}
