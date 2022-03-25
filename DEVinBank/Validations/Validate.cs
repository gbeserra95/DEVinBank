using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEVinBank.Validations
{
    public class Validate
    {
        public static bool ValidateCPF(string? cpf)
        {
            #region Check if input is null
            if (cpf == null)
                return false;
            #endregion

            #region Check input size
            if (cpf.Length != 11)
                return false;
            #endregion

            #region First CPF Validation
            int firstValidationSum = 0;
            int a = 10;
            int firstRemainder;

            for (int i = 0; i < 9; i++)
            {
                firstValidationSum += (int)Char.GetNumericValue(cpf[i]) * a;
                a--;
            }

            firstRemainder = (firstValidationSum * 10) % 11;

            if (firstRemainder == 10)
                firstRemainder = 0;

            if (firstRemainder != (int)Char.GetNumericValue(cpf[9]))
                return false;
            #endregion

            #region Second CPF Validation
            int secondValidationSum = 0;
            int b = 11;
            int secondRemainder;

            for (int i = 0; i < 10; i++)
            {
                secondValidationSum += (int)Char.GetNumericValue(cpf[i]) * b;
                b--;
            }

            secondRemainder = (secondValidationSum * 10) % 11;

            if (secondRemainder == 10)
                secondRemainder = 0;

            if (secondRemainder != (int)Char.GetNumericValue(cpf[10]))
                return false;
            #endregion

            #region Check if all digits are the same
            if (cpf[0] == cpf[1] && cpf[1] == cpf[2] && cpf[2] == cpf[3] && cpf[3] == cpf[4] && cpf[4] == cpf[5] && cpf[5] == cpf[6] && cpf[6] == cpf[7] && cpf[7] == cpf[8] && cpf[8] == cpf[9] && cpf[9] == cpf[10])
                return false;
            #endregion

            return true;
        }
    }
}
