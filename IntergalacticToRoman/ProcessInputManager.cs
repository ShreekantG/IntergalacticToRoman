// -----------------------------------------------------------------------
// <copyright file="ProcessInputManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IntergalacticToRoman
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProcessInputManager
    {
        #region Private variables

        private UpdateValues<string, Constanst.Romans> _romanUnits = new UpdateValues<string, Constanst.Romans>();
        private UpdateValues<string, float> _itemCredits = new UpdateValues<string, float>();

        #endregion

        # region Private Methods

        private int CalculateSum(char currentRoman, char nextRoman)
        {
            var currentValue = (int)Enum.Parse(typeof(Constanst.Romans), currentRoman.ToString(CultureInfo.InvariantCulture));

            if (nextRoman == 'N') return currentValue;

            var nextVlaue = (int)Enum.Parse(typeof(Constanst.Romans), nextRoman.ToString(CultureInfo.InvariantCulture));
            if (currentValue < nextVlaue)
                return currentValue * -1;

            return currentValue;
        }
        private bool ValidateInput(string romanSting)
        {
            var regex = new Regex(Constanst.Constants.RomanRegex);
            if (!regex.IsMatch(romanSting))
                return false;

            if (romanSting.Contains("MMMM"))
                return false;

            return true;
        }

        #endregion

        #region Public Methods

        public string ProcessInput(string inputString)
        {
            switch (findTypeOfString(inputString))
            {
                case Constanst.InputType.AssignCredits:
                    return AssignCredit(inputString);
                case Constanst.InputType.NewWord:
                    return AddWord(inputString);
                case Constanst.InputType.HowMuchIs:
                    return GetHowMuchIs(inputString);
                case Constanst.InputType.Roman:
                    return GetConvertedValue(inputString);
                case Constanst.InputType.Invalid:
                    return Constanst.Constants.Message;
                case Constanst.InputType.HowManyCredits:
                    return GetHowManyCredits(inputString);
            }

            return IntergalacticToRoman.Constanst.Constants.IncorrectInput;
        }

        public string GetConvertedValue(string romanSting)
        {
            if (!ValidateInput(romanSting))
                throw new Exception(IntergalacticToRoman.Constanst.Constants.InvalidRoamnsCombination);

            var sum = 0;
            var romanArray = romanSting.ToCharArray();
            for (var index = 0; index < romanArray.Length; index++)
            {
                var nextRoman = (index + 1) >= romanArray.Length ? 'N' : romanArray[index + 1];
                sum = sum + CalculateSum(romanArray[index], nextRoman);
            }

            return sum.ToString(CultureInfo.InvariantCulture);
        }

        public string GetHowMuchIs(string inputString)
        {
            inputString =
                inputString.Replace("?", string.Empty).Trim().Substring(IntergalacticToRoman.Constanst.Constants.HowMuchIs.Length).TrimStart();
            var words = inputString.Split(' ');

            var romanWord = string.Empty;
            foreach (var word in words)
            {
                if (_romanUnits.Keys.Contains(word))
                {
                    romanWord += _romanUnits[word];
                }
                else
                {
                    return IntergalacticToRoman.Constanst.Constants.InvalidUnits;
                }
            }

            return string.Format("{0} is {1}", inputString, GetConvertedValue(romanWord));
        }

        public Constanst.InputType findTypeOfString(string inputString)
        {

            var regex = new Regex(Constanst.Constants.NewWordRegex);
            if (regex.IsMatch(inputString))
            {
                return Constanst.InputType.NewWord;
            }
            regex = new Regex(Constanst.Constants.AssignCredits);
            if (regex.IsMatch(inputString))
            {
                return Constanst.InputType.AssignCredits;
            }

            regex = new Regex(Constanst.Constants.HowMuchRegex);
            if (regex.IsMatch(inputString))
            {
                return Constanst.InputType.HowMuchIs;
            }

            regex = new Regex(Constanst.Constants.HowManyCreditsRegex);
            if (regex.IsMatch(inputString))
            {
                return Constanst.InputType.HowManyCredits;
            }

            regex = new Regex(Constanst.Constants.RomanRegex);
            return regex.IsMatch(inputString) ? Constanst.InputType.Roman : Constanst.InputType.Invalid;
        }

        public string GetHowManyCredits(string inputString)
        {
            inputString = inputString.Replace("?", string.Empty).Trim().Substring(IntergalacticToRoman.Constanst.Constants.HowManyCredits.Length).TrimStart();
            var words = inputString.Split(' ');
            var romanWord = string.Empty;
            foreach (var word in words)
            {
                if (_romanUnits.ContainsKey(word))
                {
                    romanWord += _romanUnits[word];
                }
                else if (_itemCredits.ContainsKey(word))
                {
                    return string.Format("{0} is {1} Credits", inputString,
                        (int.Parse(GetConvertedValue(romanWord)) * _itemCredits[word]).ToString(
                            CultureInfo.InvariantCulture));
                }
                else
                {
                    break;
                }
            }

            return IntergalacticToRoman.Constanst.Constants.InvalidCredits;
        }

        public string AssignCredit(string inputString)
        {
            var words = inputString.Split(' ');
            var romanWord = string.Empty;
            for (var index = 0; index < words.Length; index++)
            {
                var word = words[index].Trim();

                if (string.IsNullOrEmpty(word))
                {
                    return IntergalacticToRoman.Constanst.Constants.Message;
                }

                if (_romanUnits.Keys.Contains(word))
                {
                    romanWord += _romanUnits[word];
                }
                else
                {
                    if (!words[index + 1].Equals("is"))
                        return IntergalacticToRoman.Constanst.Constants.InvalidUnits;

                    var itemGrossValue = int.Parse(words[index + 2]);
                    var unitValue = (itemGrossValue / float.Parse(GetConvertedValue(romanWord)));
                    _itemCredits.AddUpdateKey(word, unitValue);
                    return IntergalacticToRoman.Constanst.Constants.CreditsAssigned;
                }
            }

            return IntergalacticToRoman.Constanst.Constants.IncorrectInput;
        }

        public string AddWord(string inputString)
        {
            var words = inputString.Split(' ');
            var roman = (IntergalacticToRoman.Constanst.Romans)Enum.Parse(typeof(IntergalacticToRoman.Constanst.Romans), words[2]);
            _romanUnits.AddUpdateKey(words[0], roman);
            return IntergalacticToRoman.Constanst.Constants.Assigned;
        }
        #endregion
    }

}
