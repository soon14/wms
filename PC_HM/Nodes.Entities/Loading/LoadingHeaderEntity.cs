using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    [Serializable]
    public class LoadingHeaderEntity
    {
        #region 变量
        private int _syncState = 1;
        #endregion

        [ColumnName("ID")]
        public int ID { get; set; }
        [ColumnName("WH_CODE")]
        public string WarehouseCode { get; set; }
        [ColumnName("WH_NAME")]
        public string WarehouseName { get; set; }
        [ColumnName("VH_TRAIN_NO")]
        public string LoadingNO { get; set; }
        [ColumnName("VH_ID")]
        public int VehicleID { get; set; }
        [ColumnName("VH_NO")]
        public string VehicleNO { get; set; }
        [ColumnName("SYNC_STATE")]
        public int SyncState
        {
            get { return this._syncState; }
            set
            {
                if (this._syncState == value)
                    return;
                this._syncState = value;
            }
        }
        [ColumnName("USER_NAME")]
        public string UserName { get; set; }
        [ColumnName("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        [ColumnName("TRAIN_NO")]
        public string TrainNo { get; set; }
        [ColumnName("TRAIN_DATE")]
        public DateTime? TrainDate { get; set; }

        private List<LoadingDetailEntity> _details = null;
        public List<LoadingDetailEntity> Details
        {
            get
            {
                if (this._details == null)
                    this._details = new List<LoadingDetailEntity>();
                return this._details;
            }
            set
            {
                this._details = value;
            }
        }

        public List<LoadingUserEntity> Users { get; set; }

        [ColumnName("FINISH_DATE")]
        public DateTime? FinishDate { get; set; }
    }
}
