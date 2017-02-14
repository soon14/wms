using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class ScreenAchievementEntity : IComparable<ScreenAchievementEntity>, IComparer<ScreenAchievementEntity>
    {
       

        /// <summary>
        /// 人员信息
        /// </summary>
        [ColumnName("人员姓名")]
        public string userName { get; set; }

        /// <summary>
        /// 人员属性
        /// </summary>
        [ColumnName("所属")]
        public string userAttribute { get; set; }

        /// <summary>
        /// 常规拣货量
        /// </summary>
        [ColumnName("拣货量")]
        public decimal normalPick { get; set; }

        /// <summary>
        /// 批市拣货量
        /// </summary>
        [ColumnName("批市拣货量")]
        public decimal mostPick { get; set; }

        /// <summary>
        /// 220_配送量(金杯)
        /// </summary>
        [ColumnName("220_配送量")]
        public decimal dispatching220 { get; set; }

        /// <summary>
        /// 221_配送量(箱货)
        /// </summary>
        [ColumnName("221_配送量")]
        public decimal dispatching221 { get; set; }

        /// <summary>
        /// 配送员角色
        /// </summary>
        [ColumnName("配送角色")]
        public string UserRole { get; set; }

        /// <summary>
        /// 装车量
        /// </summary>
        [ColumnName("装车量")]
        public decimal loading { get; set; }

        /// <summary>
        /// 叉车量
        /// </summary>
        [ColumnName("叉车量")]
        public decimal forklift { get; set; }

        /// <summary>
        /// 常规拣货分数
        /// </summary>
        public decimal normalPickGrade 
        {
            get 
            {
                if (normalPick == 0)
                {
                    return  0;
                }else
                {
                    return Math.Round(normalPick * GlobalSettings.NormalPickRatio,2); 
                }
            
            }
        }

        /// <summary>
        /// 批市拣货分数
        /// </summary>
        public decimal mostPickGrade
        {
            get
            {
                if (mostPick == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(mostPick * GlobalSettings.MostPickRatio,2);
                }

            }
        }

        /// <summary>
        /// 配送量分数(金杯)
        /// </summary>
        public decimal dispatchingGrade_220
        {
            get
            {
                if (dispatching220 == 0)
                {
                    return 0;
                }
                else
                {
                    if (String.IsNullOrEmpty(UserRole))
                        return Math.Round(dispatching220 * GlobalSettings.DispatchingRatio2, 2);
                    else
                        return Math.Round(dispatching220 * GlobalSettings.DispatchingRatio1_220, 2);
                }

            }
        }

        /// <summary>
        /// 配送量分数(金杯)
        /// </summary>
        public decimal dispatchingGrade_221
        {
            get
            {
                if (dispatching221 == 0)
                {
                    return 0;
                }
                else
                {
                    if (String.IsNullOrEmpty(UserRole))
                        return Math.Round(dispatching221 * GlobalSettings.DispatchingRatio2, 2);
                    else
                        return Math.Round(dispatching221 * GlobalSettings.DispatchingRatio1_221, 2);
                }

            }
        }

        /// <summary>
        /// 装车量分数
        /// </summary>
        public decimal loadingGrade
        {
            get
            {
                if (loading == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(loading * GlobalSettings.LoadingRatio,2);
                }

            }
        }


        /// <summary>
        /// 叉车量分数
        /// </summary>
        public decimal forkliftGrade
        {
            get
            {
                if (forklift == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(forklift * GlobalSettings.ForkliftRatio, 2);
                }
            }
        }


       
        /// <summary>
        /// 综合总分数
        /// </summary>
        public decimal userAllGrade
        {
            get
            {
                decimal sumALL = normalPickGrade + mostPickGrade + dispatchingGrade_220 + dispatching221 + loadingGrade + forkliftGrade;
                if(sumALL == 0)
                {
                    return 0;
                 }
                
                else
                 {
                    return sumALL ;
                 }
            }
        }

        #region IComparable 成员

        public int CompareTo(ScreenAchievementEntity obj)
        {
            return userAllGrade.CompareTo(obj.userAllGrade);
        }

        #endregion

        #region IComparer<ScreenAchievementEntity> 成员

        public int Compare(ScreenAchievementEntity x, ScreenAchievementEntity y)
        {
            return x.userAllGrade.CompareTo(y.userAllGrade);
        }

        #endregion
    }
}
