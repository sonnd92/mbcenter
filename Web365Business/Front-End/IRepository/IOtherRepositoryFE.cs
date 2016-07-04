using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Domain.Service;

namespace Web365Business.Front_End.IRepository
{
    public interface IOtherRepositoryFE
    {
        bool AddContact(tblContact contact);
        void AddEmailPromotion(tblReceiveInfo info);
        List<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemByNameascii(string nameAscii);
        List<StepOfServiceItem> GetStepOfServiceItems(int id);
        List<GroupOfServiceItem> GetDetailServiceItems(int id);
    }
}
