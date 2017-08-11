// -----------------------------------------------------------------------
// <copyright file="Constanst.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IntergalacticToRoman
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Constanst
    {
        public static class Constants
        {
            public static readonly string NewWordRegex = "^([A-Za-z]+) is ([I|V|X|L|C|D|M])$";
            public static readonly string HowMuchRegex = "^how much is (([A-Za-z\\s])+)\\?$";
            public static readonly string HowManyCreditsRegex = "^how many [c|C]redits is (([A-Za-z\\s])+)\\?$";
            public static readonly string AssignCredits = "^([A-Za-z]+)([A-Za-z\\s]*) is ([0-9]+) ([c|C]redits)$";
            public static readonly string RomanRegex = "^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
            public static readonly string HowManyCredits = "how many Credits is";
            public static readonly string HowMuchIs = "how much is";
            public static readonly string Message = "I have no idea what you are talking about";
            public static readonly string InvalidUnits = "Invalid Units";
            public static readonly string InvalidCredits = "Invalid Credits";
            public static readonly string InvalidRoamns = "Invalid Roamns";
            public static readonly string InvalidRoamnsCombination = "Invalid combination";
            public static readonly string Success = "Success";
            public static readonly string Assigned = "Added Word assigned";
            public static readonly string CreditsAssigned = "Added Credits assigned";
            public static readonly string CalculatedCredits = "Calculated Credits";
            public static readonly string IncorrectInput = "In correct input";
        }

        public enum Romans
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }
        public enum InputType
        {
            Roman,
            Invalid,
            NewWord,
            AssignCredits,
            HowMuchIs,
            HowManyCredits
        }
    }

}
