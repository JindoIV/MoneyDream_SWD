using Microsoft.Identity.Client;
using MoneyDreamAPI.Dto.OrderDto;
using MoneyDreamAPI.Dto.PaginationDto;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;

namespace MoneyDreamAPI.Services
{
    public interface IOrderService {
        public object CreateOrder(CreateOrderRequest request);
        public bool CancelOrder(int orderID);
        public object ViewOrder(int orderID);
        public IEnumerable<object> ViewAllOrder(int accountId);
        public  object ViewAllOrderForAdmin(PaginationRequest parameters);
        public  object ViewOrderByIDForAdmin(int  orderID);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            _productRepository = new ProductRepository();
        }

        public bool CancelOrder(int orderID)
        {
            return _orderRepository.CancelOrder(orderID);
        }

        public object CreateOrder(CreateOrderRequest request)
        {
            int IdMarker = -1;
            try
            {
                long totalAmount = 0;

                foreach (var item in request.OrderProducts)
                {
                    var pro = _productRepository.GetProductById(item.ProductID);
                    var amount = (long)(pro.OldPrice * item.Quantity);
                    Console.WriteLine(amount);
                    totalAmount += amount;
                }

                Order newOrder = new Order();
                newOrder.AccountId = request.AccountID;
                newOrder.PaymentId = request.PaymentID;
                newOrder.AddressId = request.AddressID;
                newOrder.VoucherId = request.VoucherID;
                newOrder.OrderStatusId = 1;
                newOrder.TotalAmount = totalAmount;
                newOrder.OrderDate = DateTime.Now.ToString();
                newOrder.UpdateStatusAt = DateTime.Now.ToString();

                int orderID = _orderRepository.CreateOrder(newOrder);
                IdMarker = orderID; 

                List<OrderDetail> details = new List<OrderDetail>();

                if (request.OrderProducts.Length != 0)
                {
                    foreach (var item in request.OrderProducts)
                    {
                        OrderDetail detail = new OrderDetail();

                        detail.OrderId = orderID;
                        detail.ProductId = item.ProductID;
                        detail.Quantity = item.Quantity;
                        detail.OrderDate = DateTime.Now.ToString();

                        details.Add(detail);
                    }
                }

                var createStatus = _orderDetailRepository.CreateNewOrderDetails(details);
                if (!createStatus)
                {
                    var check = _orderRepository.DeleteOrder(IdMarker);
                    if (check)
                    {
                        return new { orderID, CreateStatus = createStatus, Message = "Error! Order is cancel" };
                    }
                }

                return new { orderID, CreateStatus = createStatus};

            } catch (Exception ex)
            {
                var check =  _orderRepository.DeleteOrder(IdMarker);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<object> ViewAllOrder(int accountId)
        {
            return _orderRepository.GetAllOrder(accountId);
        }

        public  object  ViewAllOrderForAdmin(PaginationRequest parameters)
        {
            int pageNumber = parameters.PageNumber;
            int pageSize = parameters.PageSize;

            (IEnumerable<object> orders, int totalRecord) = _orderRepository.AdminGetAllOrder(parameters.PageNumber, parameters.PageSize);
            return new
            {
                paginationData = new
                {
                    totalPage = (int)Math.Ceiling((double)totalRecord / pageSize),
                    totalRecord = totalRecord,
                    pageNumber = pageNumber,
                    pageSize = pageSize,
                    pageData = orders
                }
            };
       
        }

        public object ViewOrder(int orderID)
        {
            return _orderRepository.GetOrder(orderID);
        }

        public object ViewOrderByIDForAdmin(int orderID)
        {
            return _orderRepository.AdminGetOrderByOrderID(orderID);
        }
    }
}
