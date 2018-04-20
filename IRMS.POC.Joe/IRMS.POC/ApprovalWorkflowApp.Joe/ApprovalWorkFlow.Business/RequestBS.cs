using ApprovalWorkFlow.Business.Contracts;
using ApprovalWorkFlow.BusinessDTOs;
using ApprovalWorkFlow.DataAccess;
using ApprovalWorkFlow.DataAccessDTOs;
using ApprovalWorkFlow.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Edit by Joe

namespace ApprovalWorkFlow.Business
{
    public class RequestBS: IRequestBS
    {
        public int SubmitRequestBS(RequestUserDetailsDTO requestUserDetailsDTO)
        {
            UserDO requesterDO = new UserDO {
                EmailId = requestUserDetailsDTO.RequesterDTO.User.EmailId,
                FirstName = requestUserDetailsDTO.RequesterDTO.User.FirstName,
                LastName = requestUserDetailsDTO.RequesterDTO.User.LastName,
                NTId = requestUserDetailsDTO.RequesterDTO.User.NTId,
                UserId = requestUserDetailsDTO.RequesterDTO.User.UserId
            };

            RequesterDO requester = new RequesterDO { User= requesterDO };

            UserDO managerDO = new UserDO
            {
                EmailId = requestUserDetailsDTO.ManagerDTO.User.EmailId,
                FirstName = requestUserDetailsDTO.ManagerDTO.User.FirstName,
                LastName = requestUserDetailsDTO.ManagerDTO.User.LastName,
                NTId = requestUserDetailsDTO.ManagerDTO.User.NTId,
                UserId = requestUserDetailsDTO.ManagerDTO.User.UserId
            };

            ManagerDO manager = new ManagerDO { User = managerDO };

            RequestParamsDO requestParamsDO = new RequestParamsDO();
            DMCCEntityDO DMCCEntity = new DMCCEntityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.DMCCEntity.AccessName,
                CheckStatus= requestUserDetailsDTO.RequestParams.DMCCEntity.CheckStatus,
                FilePath= requestUserDetailsDTO.RequestParams.DMCCEntity.FilePath,
                FileName = requestUserDetailsDTO.RequestParams.DMCCEntity.FileName
            };
            requestParamsDO.DMCCEntity = DMCCEntity;

            List<ChildCheckStatusDO> childCheckStatusList = new List<ChildCheckStatusDO>();
            foreach (ChildCheckStatus ChildCheckStatus in requestUserDetailsDTO.RequestParams.HPALMEntity.ChildCheckStatusList)
            {
                ChildCheckStatusDO childCheckStatusDO = new ChildCheckStatusDO();
                childCheckStatusDO.AccessName = ChildCheckStatus.AccessName;
                childCheckStatusDO.CheckStatus = ChildCheckStatus.CheckStatus;
                childCheckStatusList.Add(childCheckStatusDO);
            }

            HPALMEntityDO HPALMEntity = new HPALMEntityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.HPALMEntity.AccessName,
                CheckStatus = requestUserDetailsDTO.RequestParams.HPALMEntity.CheckStatus,
                ChildCheckStatusList  = childCheckStatusList
            };
            requestParamsDO.HPALMEntity = HPALMEntity;

            List<ChildCheckStatusDO> childCheckStatusList1 = new List<ChildCheckStatusDO>();
            foreach (ChildCheckStatus ChildCheckStatus in requestUserDetailsDTO.RequestParams.IDSDropZoneEntity.ChildCheckStatusList)
            {
                ChildCheckStatusDO childCheckStatusDO = new ChildCheckStatusDO();
                childCheckStatusDO.AccessName = ChildCheckStatus.AccessName;
                childCheckStatusDO.CheckStatus = ChildCheckStatus.CheckStatus;
                childCheckStatusList1.Add(childCheckStatusDO);
            }

            IDSDropZoneEntityDO IDSDropZoneEntity = new IDSDropZoneEntityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.IDSDropZoneEntity.AccessName,
                CheckStatus = requestUserDetailsDTO.RequestParams.IDSDropZoneEntity.CheckStatus,
                ChildCheckStatusList = childCheckStatusList1
            };
            requestParamsDO.IDSDropZoneEntity = IDSDropZoneEntity;

            IDSSPEntityDO IDSSPEntity = new IDSSPEntityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.IDSSPEntity.AccessName,
                CheckStatus = requestUserDetailsDTO.RequestParams.IDSSPEntity.CheckStatus
            };
            requestParamsDO.IDSSPEntity = IDSSPEntity;

            List<TrailNameDO> trailNames = new List<TrailNameDO>();

            if(requestUserDetailsDTO.RequestParams.MKSIntegrity.TrailNames != null)
            {
                foreach (TrailName trailName in requestUserDetailsDTO.RequestParams.MKSIntegrity.TrailNames)
                {
                    TrailNameDO trailNameDo = new TrailNameDO();
                    trailNameDo.text = trailName.text;
                    trailNames.Add(trailNameDo);
                }
            }
            
            MKSIntegrityDO MKSIntegrity = new MKSIntegrityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.MKSIntegrity.AccessName,
                CheckStatus = requestUserDetailsDTO.RequestParams.MKSIntegrity.CheckStatus,
                TrailNames = (requestUserDetailsDTO.RequestParams.MKSIntegrity.TrailNames != null) ? trailNames.ToArray() : null
            };
            requestParamsDO.MKSIntegrity = MKSIntegrity;

            SASDDEntityDO SASDDEntity = new SASDDEntityDO
            {
                AccessName = requestUserDetailsDTO.RequestParams.SASDDEntity.AccessName,
                CheckStatus = requestUserDetailsDTO.RequestParams.SASDDEntity.CheckStatus
            };
            requestParamsDO.SASDDEntity = SASDDEntity;

            RequestUserDetailsDO requestUserDetailsDO = new RequestUserDetailsDO() {
                    ApplicationId= requestUserDetailsDTO.ApplicationId,
                    ManagerDO= manager,
                    RequesterDO=requester,
                    RequestCreatedDt= requestUserDetailsDTO.RequestCreatedDt,
                    RequestStateId = requestUserDetailsDTO.RequestStateId,
                    StatusInd = requestUserDetailsDTO.StatusInd
            };

            requestUserDetailsDO.RequestParams= SerializationHelper.SerializeToXml(requestParamsDO);

            RequestRepository requestRepo = new RequestRepository();

            return requestRepo.SubmitRequestDA(requestUserDetailsDO);
        }
    }
}
