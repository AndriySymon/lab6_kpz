
namespace ClassLibrary2.Interfaces
{
    public interface IReadableAccount
    {
        Account GetAccountByCardNumber(string cardNumber);
        Account GetAccount(string cardNumber, string pin);
    }
}
