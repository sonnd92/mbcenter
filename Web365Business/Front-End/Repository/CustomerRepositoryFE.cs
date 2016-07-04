using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class CustomerRepositoryFE : BaseFE, ICustomerRepositoryFE
    {
        private readonly ICustomerDAFERepository customerDAFERepository;

        public CustomerRepositoryFE(ICustomerDAFERepository _customerDAFERepository)
        {
            customerDAFERepository = _customerDAFERepository;
        }

        public bool AddCustomer(out int customerId, tblCustomer customer)
        {
            return customerDAFERepository.AddCustomer(out customerId, customer);

        }

        public void UpdateCustomer(CustomerItem customer)
        {
            customerDAFERepository.UpdateCustomer(customer);
        }

        public bool TryGetByUserNameAndPassword(out CustomerItem customer, string userName, string password)
        {
            return customerDAFERepository.TryGetByUserNameAndPassword(out customer, userName, password);
        }

        public bool TryGetById(out CustomerItem customer, int id)
        {
            return customerDAFERepository.TryGetById(out customer, id);
        }

        public bool ForgetPassword(out string token, string email)
        {
            return customerDAFERepository.ForgetPassword(out token, email);
        }

        public bool ResetPassword(string email, string key, string password)
        {
            return customerDAFERepository.ResetPassword(email, key, password);
        }
    }
}
