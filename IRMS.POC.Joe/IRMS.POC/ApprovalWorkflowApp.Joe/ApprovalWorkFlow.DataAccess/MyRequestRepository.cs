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
    public class MyRequestRepository: IMyRequestRepository
    {
        public List<MyRequestDetailsDO> GetAllRequestDA(int userId)
        {
            List<MyRequestDetailsDO> myRequestListDO = new List<MyRequestDetailsDO>();
            SqlParameter ParamUserId = new SqlParameter("@UserId", SqlDbType.Int);
            try
            {
                if (userId != 0)
                {
                    ParamUserId.Value = userId;
                }
                else
                {
                    ParamUserId.Value = 0;
                }
                using (var context = new AWFContext())
                {
                    myRequestListDO = context.Database.SqlQuery<MyRequestDetailsDO>("[usp_Get_MyRequest] @UserId", ParamUserId).ToList();
                }
                    
            }
            catch (Exception ex)
            {
                throw;
            }
            return myRequestListDO;
        }

        public string UpdateRequestDA(UpdateUserRequestDetailsDO updateUserRequestDetailsDO)
        {
            string status;
            try
            {
                using (var context = new AWFContext())
                {
                    SqlParameter ParamTaskId = new SqlParameter("@TaskId", SqlDbType.VarChar, 20);
                    ParamTaskId.Value = updateUserRequestDetailsDO.TaskId;

                    SqlParameter ParamUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                    ParamUserName.Value = updateUserRequestDetailsDO.UserName;

                    SqlParameter ParamRequestNo = new SqlParameter("@RequestNo", SqlDbType.Int);
                    ParamRequestNo.Value = Convert.ToInt32(updateUserRequestDetailsDO.RequestNo);

                    SqlParameter StatusOUT = new SqlParameter("@Status", SqlDbType.VarChar, 15);
                    StatusOUT.Direction = ParameterDirection.Output;

                    context.Database.ExecuteSqlCommand("[usp_UpdateUserRequest] @TaskId, @UserName, @RequestNo, @Status OUTPUT",
                                                             ParamTaskId,
                                                             ParamUserName,
                                                             ParamRequestNo,
                                                             StatusOUT
                                                             );

                    status = StatusOUT.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return status;
        }
        public List<RequestStatusDO> GetStatus(string id)
        {
            List<RequestStatusDO> myRequesStatustListDO = new List<RequestStatusDO>();
           
            try
            {
                SqlParameter ParamRequestId = new SqlParameter("@RequestId", SqlDbType.Int);
                ParamRequestId.Value = Convert.ToInt32(id);
                using (var context = new AWFContext())
                {
                    myRequesStatustListDO = context.Database.SqlQuery<RequestStatusDO>("[usp_Get_RequestStatus] @RequestId", ParamRequestId).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return myRequesStatustListDO;
        }
    }
}
