using ApprovalWorkFlow.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalWorkFlow.DataAccessDTOs;
using System.Data.SqlClient;
using System.Data;

namespace ApprovalWorkFlow.DataAccess
{
    public class MyApprovalRepository : IMyApprovalRepository
    {
        public List<MyApprovalDetailsDO> GetApprovalDA(int userId, int requestId, string type)
        {
            List<MyApprovalDetailsDO> myApprovalListDO = new List<MyApprovalDetailsDO>();
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

                SqlParameter ParamRequestId = new SqlParameter("@RequestId", SqlDbType.Int);
                ParamRequestId.Value = requestId;


                SqlParameter ParamType = new SqlParameter("@Type", SqlDbType.VarChar);
                ParamType.Value = type;

                using (var context = new AWFContext())
                {
                    myApprovalListDO = context.Database.SqlQuery<MyApprovalDetailsDO>("[usp_Get_MyApproval] @UserId, @RequestId, @Type", ParamUserId, ParamRequestId, ParamType).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return myApprovalListDO;
        }

        public string UpdateRequestDA(UpdateRequestDetailsDO updateRequestDetailsDO)
        {
            string status;
            try
            {
                using (var context = new AWFContext())
                {
                    SqlParameter ParamComment = new SqlParameter("@Comment", SqlDbType.VarChar,100);
                    ParamComment.Value = updateRequestDetailsDO.Comment;

                    SqlParameter ParamOption = new SqlParameter("@Option", SqlDbType.VarChar, 30);
                    ParamOption.Value = updateRequestDetailsDO.Option;

                    SqlParameter ParamUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                    ParamUserName.Value = updateRequestDetailsDO.UserName; //"BLEPLAE";//

                    SqlParameter ParamRequestNo = new SqlParameter("@RequestNo", SqlDbType.Int);
                    ParamRequestNo.Value = Convert.ToInt32(updateRequestDetailsDO.RequestNo);

                    SqlParameter ParamTaskId = new SqlParameter("@TaskId", SqlDbType.VarChar, 20);
                    ParamTaskId.Value = updateRequestDetailsDO.TaskId;

                    SqlParameter StatusOUT = new SqlParameter("@Status", SqlDbType.VarChar, 15);
                    StatusOUT.Direction = ParameterDirection.Output;

                    context.Database.ExecuteSqlCommand("[usp_UpdateRequest] @Comment, @Option, @UserName, @RequestNo, @TaskId, @Status OUTPUT",
                                                             ParamComment,
                                                             ParamOption,
                                                             ParamUserName,
                                                             ParamRequestNo,
                                                             ParamTaskId,
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
    }
}
