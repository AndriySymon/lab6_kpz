using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AmountValidator
    {
        private readonly double _minAmount;
        private readonly double _maxAmount;

        public AmountValidator(double minAmount = 0.01, double maxAmount = 10000.00)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }

        public bool IsValid(double amount, double balance, out string errorMessage)
        {
            if (amount < _minAmount)
            {
                errorMessage = $"Сума повинна бути більшою за {_minAmount}.";
                return false;
            }

            if (amount > _maxAmount)
            {
                errorMessage = $"Сума не може перевищувати {_maxAmount}.";
                return false;
            }

            if (amount > balance)
            {
                errorMessage = "Недостатньо коштів на рахунку.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }

}
