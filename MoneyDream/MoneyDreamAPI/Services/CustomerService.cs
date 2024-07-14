using Microsoft.Identity.Client;
using MimeKit.Tnef;
using MoneyDreamAPI.Dto.AccountDto;
using MoneyDreamAPI.Dto.AddressDto;
using MoneyDreamAPI.Dto.CartDto;
using MoneyDreamAPI.Dto.PaymentDto;
using MoneyDreamAPI.Dto.ReviewDto;
using MoneyDreamAPI.Dto.WishlistDto;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;

namespace MoneyDreamAPI.Services
{
    public interface ICustomerService
    {

        public Product GetWishListItem(int id, int accountID);

        public IEnumerable<Product> GetWishList(int AccountID);
        public void AddWishListItem(AddToWishListRequest request);
        public void RemoveWishListItem(int id);

        public void UpdateProfile(int accountID, UpdateProfileRequest data);
        public object GetProfile(int accountID);

        public void ReviewProduct(ReviewProductRequest request);
        public IEnumerable<object> GetReviewProduct(int productID);

        public void AddProductToCart(AddToCartRequest request);
        public void RemoveFromCart(RemoveFromCartRequest request);
        public void EditQuantityProductFromCart(RemoveFromCartRequest request);
        public IEnumerable<object> GetAllProductInCart(int accountID);

        public IEnumerable<AccountAddress> GetAllAddress(int accountID);
        public AccountAddress GetAddress(int accountID);

        public void CreateAddress(AddressRequest request);
        public void UpdateAddress(int addressId, AddressRequest request);
        public void DeleteAddress (int addressId);

        public int CreatePayment(CreatePaymentRequest request);

    }
    public class CustomerService : ICustomerService 
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IAccountAddressRepository _accountAddressRepository;
        private readonly IPaymentRepository _paymentRepository;
        public CustomerService() { 
            _wishListRepository = new WishListRepository();
            _accountRepository = new AccountRepository();
            _reviewRepository = new ReviewRepository();
            _cartRepository = new CartRepository();
            _accountAddressRepository = new AccountAddressRepository();
            _paymentRepository = new PaymentRepository();
        }

        public void AddWishListItem(AddToWishListRequest request)
        {
            var newItem = new Wishlist();
            newItem.AccountId = request.AcccountID;
            newItem.ProductId = request.ProductID;
            newItem.IsActive = true;


            _wishListRepository.AddItem(newItem);
        }

        

        public IEnumerable<Product> GetWishList(int AccountID)
        {
           return  _wishListRepository.GetAllWishListItem(AccountID);
        }

        public Product GetWishListItem(int id, int accountID)
        {
            return _wishListRepository.GetWishListItem(id, accountID);
        }

        public void RemoveWishListItem(int id)
        {
            _wishListRepository.RemoveItem(id);
        }
        public object GetProfile(int accountID)
        {
            Account currentAccount = _accountRepository.GetAllAccountByID(accountID);
            if (currentAccount != null)
            {
                return new
                {
                    currentAccount.AccountId,
                    currentAccount.FullName,
                    currentAccount.UserName,
                    currentAccount.Age,
                    currentAccount.Gender,
                    currentAccount.DateofBirth,
                    currentAccount.Email,
                    currentAccount.PhoneNumber,
                    currentAccount.Picture,
                };
            }
            return null;
        }

        public void UpdateProfile(int accountID, UpdateProfileRequest data)
        {
            Account currentAccount = _accountRepository.GetAllAccountByID(accountID);

            if (currentAccount != null)
            {
                currentAccount.FullName = data.FullName;
                currentAccount.Age = data.Age;
                currentAccount.Gender = data.Gender;
                currentAccount.DateofBirth = data.Dob;
                currentAccount.PhoneNumber = data.PhoneNumber;
                currentAccount.Picture = data.Avatar;

                _accountRepository.UpdateAccount(currentAccount);
            }
        }

        public void ReviewProduct(ReviewProductRequest request)
        {
            Review review =  new Review();

            review.ProductId = request.ProductID;
            review.AccountId = request.AcccountID;
            review.Rate = request.ratePoint;
            review.ReviewContent = request.content;
            review.CreateAt = DateTime.Now.ToString();

            _reviewRepository.CreateReview(review);
        }

        public IEnumerable<object> GetReviewProduct(int productID)
        {
            return _reviewRepository.GetProductReview(productID);
        }

        public void AddProductToCart(AddToCartRequest request)
        {
            Cart item = new Cart();
            item.ProductId = request.ProductID;
            item.AccountId = request.AcccountID;
            item.Quantity = request.Quantity;

            _cartRepository.AddToCart(item);
        }

        public void RemoveFromCart(RemoveFromCartRequest request)
        {
            _cartRepository.DeleteFromCart(request.AcccountID, request.ProductID, request.Quantity);
        }


        public void EditQuantityProductFromCart(RemoveFromCartRequest request)
        {
            _cartRepository.EditQuantityProductFromCart(request.AcccountID, request.ProductID, request.Quantity);
        }

        public IEnumerable<object> GetAllProductInCart(int accountID)
        {
            return _cartRepository.GetAllProductInCart(accountID);
        }

        public IEnumerable<AccountAddress> GetAllAddress(int accountID)
        {
            return _accountAddressRepository.GetAllAddress(accountID);
        }

        public AccountAddress GetAddress(int addressID)
        {
           return _accountAddressRepository.GetAddress(addressID);

        }

        public void CreateAddress(AddressRequest request)
        {
            AccountAddress address = new AccountAddress();

            address.Address = request.Address;
            address.DeliveryPhone = request.Phone;
            address.DeliveryName = request.Name;
            address.AccountId = request.AccountID;

            _accountAddressRepository.AddAddress(address);
        }

        public void UpdateAddress(int addressID, AddressRequest request)
        {
            AccountAddress address = new AccountAddress();

            address.Address = request.Address;
            address.DeliveryPhone = request.Phone;
            address.DeliveryName = request.Name;
            address.AccountId = request.AccountID;

            _accountAddressRepository.UpdateAddress(addressID, address);
        }

        public void DeleteAddress(int addressId)
        {
            _accountAddressRepository.DeleteAddress(addressId);
        }

        public int CreatePayment(CreatePaymentRequest request)
        {
            Payment payment = new Payment();
            payment.Type = request.PaymentType;
            payment.Amount = request.Amount;
            payment.PaymentDateTime = DateTime.Now.ToString();

            return _paymentRepository.CreatePayment(payment);

        }
    }
}
