using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCValidator
{
    public class CCValidator
    {
        #region Members
        
        #endregion

        #region Methods
        protected CCValidationResult InternalValidateCard(string cardNumber)
        {
            CCValidationResult result = new CCValidationResult();
            result.CardNumber = cardNumber;

            // strip all the non-numerics out of the supplied number.
            cardNumber = stripNonNumerics(cardNumber);

            // Now we use the Luhn Formula to see if the supplied CC number passes it.
            result.InValid = !internalLuhnCheck(cardNumber);
            result.CardType = internalGetCardType(cardNumber);
            return result;

        }
        protected CardType internalGetCardType(string cardNumber)
        {
            CardType result = CardType.Unknown; // Assume it's unknown, and then we'll set out to disprove...
            
            // Declare a quick anonymous method to get the first n digits from the card number.
            Func< int, int> firstNDigitsAsInt = new Func<int, int>(n => {
                if (n > cardNumber.Length) n = cardNumber.Length;
                int val = 0;
                int.TryParse(cardNumber.Substring(0, n), out val);
                return val;
            });
            
            // Let's start testing with 6 digits.
            int firstN = firstNDigitsAsInt(6);
            if (firstN == 417500)
            {
                result = CardType.Visa_Electron;
            }
            else if (622126 <= firstN && firstN <= 622925)
            {
                result = CardType.Discover;
            }
            
            // If we need to, let's drop to testing with 4 digits.
            firstN = firstNDigitsAsInt(4);
            if (result == CardType.Unknown)
            {
                if (3528 <= firstN && firstN <= 3589)
                {
                    result = CardType.JCB;
                }
                else if((new int[] {6304,6706,6771,6709}).Contains(firstN))
                {
                    result = CardType.Laser;
                }
                else if ((new int[] { 5018, 5020, 5038, 5893, 6304, 6759, 6761,6762, 6763}).Contains(firstN))
                {
                    result = CardType.Maestro;
                }
                else if ((new int[] {4026, 4508, 4844, 4913, 4917}).Contains(firstN))
                {
                    result = CardType.Visa_Electron;
                }
            }

            // If we need to, start testing with three digits.
            firstN = firstNDigitsAsInt(3);
            if (result == CardType.Unknown)
            {
                if (300 <= firstN && firstN <= 305)
                {
                    result = CardType.Diners_Club_Carte_Blanche;
                }
                else if (644 <= firstN && firstN <= 649)
                {
                    result = CardType.Discover;
                }
                else if (637 <= firstN && firstN <= 639)
                {
                    result = CardType.InstaPayment;
                }
            }

            // If we need to, start testing with two digits.
            firstN = firstNDigitsAsInt(2);
            if (result == CardType.Unknown)
            {
                if ((new int[] {34, 37}).Contains(firstN))
                {
                    result = CardType.American_Express;
                }
                else if (firstN == 36)
                {
                    result = CardType.Diners_Club_International;
                }
                else if (firstN == 54)
                {
                    result = CardType.Diners_Club_USA_Canada;
                }
                else if (51 <= firstN && firstN <= 55)
                {
                    result = CardType.MasterCard;
                }
            }

            // Finally, if we need to, test on a single digit...
            if (result == CardType.Unknown)
            {
                firstN = firstNDigitsAsInt(1);
                if (firstN == 4)
                {
                    result = CardType.Visa;
                }
            }

            return result;
        }
        protected string stripNonNumerics(string cardNumber)
        {
            return System.Text.RegularExpressions.Regex.Replace(cardNumber, @"\D", "");
        }
        protected bool internalLuhnCheck(string cardNumber)
        {
            bool result = false;
            List<int> digits = new List<int>();
            for (int index = 0; index < cardNumber.Length - 1; index++)
            {
                string character = cardNumber.Substring(index, 1);
                digits.Add(Convert.ToInt32(character));
            }

            digits.Reverse();

            if (cardNumber.Length < 2) return false; // Unable to perform a Luhn check if this were to be the case...

            int total = 0;
            for (int index = 0; index < digits.Count; index++)
            {
                int val = digits[index];
                if (index % 2 == 0)
                {
                    val *= 2;
                    if (val > 9) val -= 9;
                    total += val;
                }
                else
                {
                    total += val;
                }
            }
            int calculatedCheck = (int)(Math.Ceiling((decimal)total / 10.0M) * 10.0M) - total;
            int lastValue = Convert.ToInt32(cardNumber.Substring(cardNumber.Length - 1, 1));
            result = calculatedCheck == lastValue;
            return result;
        }
        public CCValidationResult ValidateCard(string cardNumber)
        {
            return InternalValidateCard(cardNumber);
        }
        public bool LuhnCheck(string cardNumber)
        {
            return internalLuhnCheck(cardNumber);
        }
        public CardType GetCardType(string cardNumber)
        {
            return internalGetCardType(cardNumber);
        }
        #endregion

        #region Bulk Validation Methods
        public List<CCValidationResult> BulkValidate(IEnumerable<string> cardNumbers)
        {
            List<CCValidationResult> results = new List<CCValidationResult>();
            internalBulkValidate(cardNumbers, results);
            return results;
        }
        public void BulkValidateByAsync(IEnumerable<string> cardNumbers, List<CCValidationResult> results, Action <List<CCValidationResult>> userCallback)
        {
            Action<IEnumerable<string>, List<CCValidationResult>> launchDelegate = PerformAsyncBulkValidate;
            AsyncCallback callbackDelegate = new AsyncCallback(internalCallback);
            if (results == null) results = new List<CCValidationResult>(); else results.Clear();

            IAsyncResult ar = launchDelegate.BeginInvoke(cardNumbers,results,callbackDelegate , new AsyncStateObject(userCallback, results));
        }

        private void PerformAsyncBulkValidate(IEnumerable<string> cardNumbers, List<CCValidationResult> results)
        {
            internalBulkValidate(cardNumbers, results);
        }

        private void internalCallback(IAsyncResult ar)
        {
            AsyncStateObject state = (AsyncStateObject)ar.AsyncState;
            state.UserCallback.Invoke(state.Results);
        }

        private void internalBulkValidate(IEnumerable<string> cardNumbers, List<CCValidationResult> results)
        {
            
            foreach (string cardNumber in cardNumbers)
            {
                CCValidationResult item = InternalValidateCard(cardNumber);
                results.Add(item);
            }

            return ;
        }

       
        #endregion
    }
}
