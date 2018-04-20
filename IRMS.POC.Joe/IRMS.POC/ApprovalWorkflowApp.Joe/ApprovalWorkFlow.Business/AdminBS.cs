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
    public class AdminBS : IAdminBS
    {
        public List<AuditDTO> GetAuditBS(string itemsPerPage, string pageno)
        {
            List<AuditDTO> auditDTOList = new List<AuditDTO>();
            List<AuditDO> auditDOList = new List<AuditDO>();
            AdminRepository adminDAobj = new AdminRepository();
            auditDOList = adminDAobj.GetAuditDA(itemsPerPage, pageno);

            foreach (AuditDO auditDO in auditDOList)
            {
                AuditDTO auditDTO = new AuditDTO();
                auditDTO.RowNum = auditDO.AuditId.ToString();
                auditDTO.TotalCount = auditDO.TotalCount.ToString();
                auditDTO.AuditId = auditDO.AuditId;
                auditDTO.CreatedON = auditDO.CreatedON;
                auditDTO.RequestId = auditDO.RequestId;
                auditDTO.UserName = auditDO.UserName;

                auditDTOList.Add(auditDTO);
            }

            return auditDTOList;
        }

        public List<SupportApprovalDetailsDTO> GetApprovalsBS(int userId, int requestId, string type)
        {
            List<SupportApprovalDetailsDTO> myApprovalDetailsDTOList = new List<SupportApprovalDetailsDTO>();
            List<SupportApprovalDetailsDO> myRequestDetailsDOList = new List<SupportApprovalDetailsDO>();

            AdminRepository adminDAobj = new AdminRepository();
            myRequestDetailsDOList = adminDAobj.GetApprovalDA(userId, requestId, type);

            foreach (SupportApprovalDetailsDO myApprovalDO in myRequestDetailsDOList)
            {
                SupportApprovalDetailsDTO myApprovalDetailsDTO = new SupportApprovalDetailsDTO();
                myApprovalDetailsDTO.AppName = myApprovalDO.AppName;
                myApprovalDetailsDTO.Requester = myApprovalDO.Requester;
                myApprovalDetailsDTO.Approver = myApprovalDO.Approver;
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
    }
}
