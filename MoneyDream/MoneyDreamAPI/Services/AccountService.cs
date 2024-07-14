namespace WebApi.Services;

using Azure.Core;
using MoneyDreamAPI.Dto.AuthDto;
using MoneyDreamAPI.Dto.PaginationDto;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using WebApi.Authorization;


public interface IAccountService
{
    object GetAllAccount(PaginationRequest parameters); 
    Account? GetAccountByID(int AccountID);

    void BlockAccount(int id);
    void ActiveAccount(int id);


}

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    public AccountService()
    {
        _accountRepository = new AccountRepository();
    }

    public void ActiveAccount(int id)
    {
       _accountRepository.ActiveAccountByID(id);
    }

    public void BlockAccount(int id)
    {
        _accountRepository.BlockAccountByID(id);
    }

    public Account? GetAccountByID(int AccountID)
    {
        return _accountRepository.GetAllAccountByID(AccountID);
    }

    public object GetAllAccount(PaginationRequest parameters)
    {
        int pageNumber = parameters.PageNumber;
        int pageSize = parameters.PageSize;

        (IEnumerable<Account> accounts, int totalRecord)  = _accountRepository.GetAllAccount(pageNumber,pageSize);
        return new {
            paginationData = new
            {
                totalPage = (int)Math.Ceiling((double)totalRecord / pageSize),
                totalRecord = totalRecord,
                pageNumber = pageNumber,
                pageSize = pageSize,
                pageData = accounts
            }
        };
    }
}