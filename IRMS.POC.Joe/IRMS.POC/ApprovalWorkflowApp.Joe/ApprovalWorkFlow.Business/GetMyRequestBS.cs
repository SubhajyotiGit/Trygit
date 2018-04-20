using ApprovalWorkFlow.Business.Contracts;
using ApprovalWorkFlow.BusinessDTOs;
using ApprovalWorkFlow.DataAccess;
using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.Business
{
    public class GetMyRequestBS : IGetMyRequestBS
    {
        public List<MyRequestDetailsDTO> GetAllRequestBS(int userId)
        {
            List<MyRequestDetailsDTO> myRequestDetailsDTOList = new List<MyRequestDetailsDTO>();
            List<MyRequestDetailsDO> myRequestDetailsDOList = new List<MyRequestDetailsDO>();

            MyRequestRepository myRequestRepo = new MyRequestRepository();
            myRequestDetailsDOList = myRequestRepo.GetAllRequestDA(userId);

            foreach (MyRequestDetailsDO myRequestDO in myRequestDetailsDOList)
            {
                MyRequestDetailsDTO myRequestDetailsDTO = new MyRequestDetailsDTO();
                myRequestDetailsDTO.AppName = myRequestDO.AppName;
                myRequestDetailsDTO.Approver = myRequestDO.Approver;
                myRequestDetailsDTO.Comment = myRequestDO.Comment;
                myRequestDetailsDTO.RequestCompleteDt = Convert.ToString(myRequestDO.RequestCompleteDate);
                myRequestDetailsDTO.RequestCreateDT = Convert.ToString(myRequestDO.RequestCreateDate);
                myRequestDetailsDTO.RequestNo = myRequestDO.RequestNo;
                myRequestDetailsDTO.RequestStatus = myRequestDO.RequestStatusName;
                myRequestDetailsDTO.TaskId = myRequestDO.IrisTaskId;
                string strRequestParams = myRequestDO.RequestParameters;

                XmlSerializer serializer = new XmlSerializer(typeof(MyRequestParamsDTO));
                object obj = serializer.Deserialize(new StringReader(strRequestParams));
                MyRequestParamsDTO RequestParamData = (MyRequestParamsDTO)obj;
              //  MyRequestParamsDTO RequestParamData = (MyRequestParamsDTO)serializer.Deserialize(new StringReader(strRequestParams));
                myRequestDetailsDTO.RequestParam = RequestParamData;

                myRequestDetailsDTOList.Add(myRequestDetailsDTO);
            }
            return myRequestDetailsDTOList;
        }

        public string UpdateRequest(UpdateUserRequestDetailsDTO updateUserRequestDetailsDTO)
        {
            MyRequestRepository myRequestRepo = new MyRequestRepository();
            UpdateUserRequestDetailsDO updateUserRequestDetailsDO = new UpdateUserRequestDetailsDO
            {
                TaskId = updateUserRequestDetailsDTO.TaskId,
                RequestNo = updateUserRequestDetailsDTO.RequestNo,
                UserName = updateUserRequestDetailsDTO.UserName
            };
            string status = myRequestRepo.UpdateRequestDA(updateUserRequestDetailsDO);

            return status;
        }
        public List<RequestStatusDTO> GetStatus(string id)
        {
            MyRequestRepository myRequestRepo = new MyRequestRepository();
            List<RequestStatusDO> requestStatusListDO = myRequestRepo.GetStatus(id);
            List<RequestStatusDTO> requestStatusListDTO = new List<RequestStatusDTO>();

            foreach (RequestStatusDO requestStatusDO in requestStatusListDO)
            {
                RequestStatusDTO requestStatusDTO = new RequestStatusDTO();
                requestStatusDTO.RequestId = requestStatusDO.RequestId;
                requestStatusDTO.TransactionDate = Convert.ToString(requestStatusDO.TransactionDate);
                requestStatusDTO.RequestStatusName = requestStatusDO.RequestStatusName;
                requestStatusDTO.RequestStateId = requestStatusDO.RequestStateId;
                requestStatusDTO.Comment = requestStatusDO.Comment;

                requestStatusListDTO.Add(requestStatusDTO);
            }
            return requestStatusListDTO;
        }
    }
}
