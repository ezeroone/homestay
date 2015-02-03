using System;
using System.Collections.Generic;

namespace eZeroOne.Service.Visitors
{
    public interface IVisitor
    {
        eZeroOne.Domain.Visitor SaveEmployee(eZeroOne.Domain.Visitor employee, out bool isExist);
        bool UpdateEmployee(eZeroOne.Domain.Visitor employee);
               
        void SaveEmailNotifications(Guid id, int empId,int ? groupId, int?companyId,int userId);
        void UpdateEmailNotifications(Guid id, int empId);
        
        bool UpdateEmailNotifications(Guid id, int empId, string fromAdd, string toAdd, string subject, string body,
                                      string userName, string password, int port);
       
       
        bool DeleteNotifications(Guid id);
        List<Guid> GetEmailNotificationsIds();
    }
}
