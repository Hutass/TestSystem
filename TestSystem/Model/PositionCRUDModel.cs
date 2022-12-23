using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Model
{
    class PositionCRUDModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public PositionCRUDModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }



        public List<BLL.Models.PositionModel> GetPositions()
        {
            return dbOperations.GetAllPositions().ToList();
        }
        public int CreatePosition(BLL.Models.PositionModel result)
        {
            return dbOperations.CreatePosition(result);
        }
        public void DeletePosition(BLL.Models.PositionModel result)
        {
            dbOperations.DeletePosition(result);
        }
        public void UpdatePosition(BLL.Models.PositionModel result)
        {
            if (result.Name == null)
                result.Name = "";


            dbOperations.UpdatePosition(result);
        }
    }
}
