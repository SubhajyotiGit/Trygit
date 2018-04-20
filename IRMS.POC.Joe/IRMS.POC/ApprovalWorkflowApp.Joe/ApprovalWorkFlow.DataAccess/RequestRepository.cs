using ApprovalWorkFlow.DataAccess.Contracts;
using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess
{
    public class RequestRepository: IRequestRepository
    {
        public int SubmitRequestDA(RequestUserDetailsDO requestUserDetailsDO)
        {
            int RequestId = 0;
            try
            {
                using (var context = new AWFContext())
                {
                    SqlParameter ParamRequesterUserId = new SqlParameter("@RequesterUserId", SqlDbType.Int);
                    ParamRequesterUserId.Value = Convert.ToInt32(requestUserDetailsDO.RequesterDO.User.UserId);

                    SqlParameter ParamRequesterFirstName = new SqlParameter("@RequesterFirstName", SqlDbType.VarChar, 20);
                    ParamRequesterFirstName.Value = requestUserDetailsDO.RequesterDO.User.FirstName;

                    SqlParameter ParamRequesterLastName = new SqlParameter("@RequesterLastName", SqlDbType.VarChar, 20);
                    ParamRequesterLastName.Value = requestUserDetailsDO.RequesterDO.User.LastName;

                    SqlParameter ParamRequesterEmailId = new SqlParameter("@RequesterEmailId", SqlDbType.VarChar, 50);
                    ParamRequesterEmailId.Value = requestUserDetailsDO.RequesterDO.User.EmailId;

                    SqlParameter ParamRequesterNTId = new SqlParameter("@RequesterNTId", SqlDbType.VarChar, 15);
                    ParamRequesterNTId.Value = requestUserDetailsDO.RequesterDO.User.NTId;

                    SqlParameter ParamManagerUserId = new SqlParameter("@ManagerUserId", SqlDbType.Int);
                    ParamManagerUserId.Value = Convert.ToInt32(requestUserDetailsDO.ManagerDO.User.UserId);

                    SqlParameter ParamManagerFirstName = new SqlParameter("@ManagerFirstName", SqlDbType.VarChar, 20);
                    ParamManagerFirstName.Value = requestUserDetailsDO.ManagerDO.User.FirstName;

                    SqlParameter ParamManagerLastName = new SqlParameter("@ManagerLastName", SqlDbType.VarChar, 20);
                    ParamManagerLastName.Value = requestUserDetailsDO.ManagerDO.User.LastName;

                    SqlParameter ParamManagerEmailId = new SqlParameter("@ManagerEmailId", SqlDbType.VarChar, 50);
                    ParamManagerEmailId.Value = requestUserDetailsDO.ManagerDO.User.EmailId;

                    SqlParameter ParamManagerNTId = new SqlParameter("@ManagerNTId", SqlDbType.VarChar, 15);
                    ParamManagerNTId.Value = requestUserDetailsDO.ManagerDO.User.NTId;

                    SqlParameter ParamApplicationId = new SqlParameter("@ApplicationId", SqlDbType.Int);
                    ParamApplicationId.Value = Convert.ToInt32(requestUserDetailsDO.ApplicationId);
                    
                    SqlParameter ParamRequestStateId = new SqlParameter("@RequestStateId", SqlDbType.Int);
                    ParamRequestStateId.Value = Convert.ToInt32(requestUserDetailsDO.RequestStateId);

                  //  SqlParameter ParamRequestCreatedDt = new SqlParameter("@RequestCreatedDt", SqlDbType.DateTime);
                  //  ParamRequestCreatedDt.Value = Convert.ToDateTime(requestUserDetailsDO.RequestCreatedDt);

                    SqlParameter ParamRequestParams = new SqlParameter("@RequestParams", SqlDbType.NVarChar);
                    ParamRequestParams.Value = requestUserDetailsDO.RequestParams;

                    SqlParameter RequestIdOUT = new SqlParameter("@RequestId", SqlDbType.Int);
                    RequestIdOUT.Direction = ParameterDirection.Output;

                  //  DbContext.Database.ExecuteSqlCommand
                    context.Database.ExecuteSqlCommand("[usp_SubmitRequest] @RequesterUserId, @RequesterFirstName, @RequesterLastName, @RequesterEmailId, @RequesterNTId, @ManagerUserId, @ManagerFirstName, @ManagerLastName, @ManagerEmailId, @ManagerNTId, @ApplicationId, @RequestStateId, @RequestParams, @RequestId OUTPUT", 
                                                     ParamRequesterUserId,
                                                     ParamRequesterFirstName,
                                                     ParamRequesterLastName,
                                                     ParamRequesterEmailId,
                                                     ParamRequesterNTId,
                                                     ParamManagerUserId,
                                                     ParamManagerFirstName,
                                                     ParamManagerLastName,
                                                     ParamManagerEmailId,
                                                     ParamManagerNTId,
                                                     ParamApplicationId,
                                                     ParamRequestStateId,
                                                     ParamRequestParams,
                                                     RequestIdOUT
                                                     );

                    RequestId = (int)RequestIdOUT.Value;

                }

              //  censusInfo = DbContext.Database.SqlQuery<CensusInfoDO>(DataAccess.str_SpGET_CENSUSDATA, ParamQuoteID, ParamUserId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return RequestId;
        }
    }
}
