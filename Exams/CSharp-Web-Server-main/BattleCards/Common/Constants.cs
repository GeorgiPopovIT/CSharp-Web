using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCards.Common
{
   public class Constants
    {
        public const int MinLengthUsername = 5;
        public const int MaxLengthUsername = 20;
        public const int MinLengthPassword = 6;
        public const int MaxLengthPassword = 20;
        public const string UserEmailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int MinLengthCardName = 5;
        public const int MaxLengthCardName = 15;
        public const int MaxLengthCardDescription = 200;
    }
}
