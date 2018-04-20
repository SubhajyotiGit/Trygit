using ApprovalWorkFlow.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalWorkFlow.BusinessDTOs;
using ApprovalWorkFlow.DataAccessDTOs;
using System.Xml.Serialization;
using System.IO;
using ApprovalWorkFlow.DataAccess;

namespace ApprovalWorkFlow.Business
{
    public class GetApprovalBS : IGetApprovalBS
    {
        public List<MyApprovalDetailsDTO> GetApprovalsBS(int userId, int requestId, string type)
        {
            List<MyApprovalDetailsDTO> myApprovalDetailsDTOList = new List<MyApprovalDetailsDTO>();
            List<MyApprovalDetailsDO> myRequestDetailsDOList = new List<MyApprovalDetailsDO>();

            MyApprovalRepository myApprovalRepo = new MyApprovalRepository();
            myRequestDetailsDOList = myApprovalRepo.GetApprovalDA(userId, requestId, type);

            foreach (MyApprovalDetailsDO myApprovalDO in myRequestDetailsDOList)
            {
                MyApprovalDetailsDTO myApprovalDetailsDTO = new MyApprovalDetailsDTO();
                myApprovalDetailsDTO.AppName = myApprovalDO.AppName;
                myApprovalDetailsDTO.Requester = myApprovalDO.Requester;
                myApprovalDetailsDTO.Comment = myApprovalDO.Comment;
                myApprovalDetailsDTO.RequestCompleteDt = Convert.ToString(myApprovalDO.RequestCompleteDate);
                myApprovalDetailsDTO.RequestCreateDT = Convert.ToString(myApprovalDO.RequestCreateDate);
                myApprovalDetailsDTO.RequestNo = myApprovalDO.RequestNo;
                myApprovalDetailsDTO.RequestStatus = myApprovalDO.RequestStatusName;
                myApprovalDetailsDTO.TaskId = myApprovalDO.IrisTaskId;
                string strApprovalParams = myApprovalDO.RequestParameters;

                XmlSerializer serializer = new XmlSerializer(typeof(MyRequestParamsDTO));
                MyRequestParamsDTO RequestParamData = (MyRequestParamsDTO)serializer.Deserialize(new StringReader(strApprovalParams));
                myApprovalDetailsDTO.RequestParam = RequestParamData;

                myApprovalDetailsDTOList.Add(myApprovalDetailsDTO);
            }
            return myApprovalDetailsDTOList;
        }

        public string UpdateRequest(UpdateRequestDetailsDTO updateRequestDetailsDTO)
        {
            MyApprovalRepository approvalRepo = new MyApprovalRepository();
            UpdateRequestDetailsDO updateRequestDetailsDO = new UpdateRequestDetailsDO {
                 Comment= updateRequestDetailsDTO.Comment,
                 Option= updateRequestDetailsDTO.Option,
                 RequestNo = updateRequestDetailsDTO.RequestNo,
                 UserName= updateRequestDetailsDTO.UserName,
                 TaskId= updateRequestDetailsDTO.TaskId
            };
            string status = approvalRepo.UpdateRequestDA(updateRequestDetailsDO);

            return status;
        }
    }
}
