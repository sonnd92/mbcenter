using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Order;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class OrderDAFERepository : BaseFE, IOrderDAFERepository
    {
        public bool AddOrder(tblOrder order, List<tblOrderDetail> orderDetail, tblOrder_Shipping orderShipping)
        {
            try
            {
                web365db.tblOrder.Add(order);

                orderShipping.OrderID = order.ID;

                web365db.tblOrder_Shipping.Add(orderShipping);

                foreach (var item in orderDetail)
                {
                    item.OrderID = order.ID;

                    web365db.tblOrderDetail.Add(item);
                }

                var result = web365db.SaveChanges();

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public bool AddBooking(BookingItem booking)
        {
            int result = 0;
            try
            {
                var tblBooking = new tblBooking()
                {
                    Address = booking.Address,
                    Age = booking.Age,
                    Name = booking.Name,
                    Note = booking.Note,
                    BookDate = booking.BookDate,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    Gender = booking.Gender,
                    IsDeleted = false,
                    IsViewed = false,
                    Phone = booking.Phone,
                    TypeBookingId = booking.TypeBooking
                };
                Web365DB.tblBooking.Add(tblBooking);
                result = Web365DB.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return result > 0;
        }

        public List<OrderItem> GetListOrderByCustomer(int customerId)
        {
            var query = web365db.tblOrder.Where(o => o.CustomerID == customerId).Select(o => new OrderItem()
            {
                ID = o.ID,
                DateCreated = o.DateCreated,
                TotalCost = o.TotalCost,
                OrderStatus = new OrderStatusItem()
                {
                    Name = o.tblOrder_Status.Name
                }
            });

            return query.ToList();
        }
    }
}
