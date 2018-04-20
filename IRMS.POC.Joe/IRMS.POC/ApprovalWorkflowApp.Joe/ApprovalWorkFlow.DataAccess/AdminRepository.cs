using ApprovalWorkFlow.DataAccess.Contracts;
using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess
{
    public class AdminRepository: IAdminRepository
    {
        public List<AuditDO> GetAuditDA(string itemsPerPage, string pageno)
        {
            List<AuditDO> auditDOList = new List<AuditDO>();
            try
            {
                SqlParameter ParamItemsPerPage = new SqlParameter("@ItemsPerPage", SqlDbType.Int);
                ParamItemsPerPage.Value = Convert.ToInt16(itemsPerPage);

                SqlParameter ParamPageno = new SqlParameter("@Pageno", SqlDbType.Int);
                ParamPageno.Value = Convert.ToInt16(pageno);

                using (var context = new AWFContext())
                {
                auditDOList = context.Database.SqlQuery<AuditDO>("[usp_GetAudits] @ItemsPerPage, @Pageno", ParamItemsPerPage, ParamPageno).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return auditDOList;
        }

        public List<SupportApprovalDetailsDO> GetApprovalDA(int userId, int requestId, string type)
        {
            List<SupportApprovalDetailsDO> myApprovalListDO = new List<SupportApprovalDetailsDO>();
            
            try
            {
                //SqlParameter ParamUserId = new SqlParameter("@UserId", SqlDbType.Int);
                //if (userId != 0)
                //{
                //    ParamUserId.Value = userId;
                //}
                //else
                //{
                //    ParamUserId.Value = 0;
                //}

                SqlParameter ParamRequestId = new SqlParameter("@RequestId", SqlDbType.Int);
                ParamRequestId.Value = requestId;


                SqlParameter ParamType = new SqlParameter("@Type", SqlDbType.VarChar);
                ParamType.Value = type;

                using (var context = new AWFContext())
                {
                  //  myApprovalListDO = context.Database.SqlQuery<SupportApprovalDetailsDO>("[usp_Get_SupportApprovalList] @UserId, @RequestId, @Type", ParamUserId, ParamRequestId, ParamType).ToList();
                    myApprovalListDO = context.Database.SqlQuery<SupportApprovalDetailsDO>("[usp_Get_SupportApprovalList] @RequestId, @Type", ParamRequestId, ParamType).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return myApprovalListDO;
        }

    }
}
