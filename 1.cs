using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LingLi.Logic
{
    public class MessagePushProvider:EkpMessagePushProvider
    {
        public override void Push(int acceptId, string title, string subject, string Uri, int moduleId)
        {
            try
            {
                MobileWeChartLogic wechartLogic = new MobileWeChartLogic();
                if (Landray.Components.Configuration.ConfigurationBroker.WeiXinSettings.EnableWeiXin)
                {
                    //微信企业号消息
                    wechartLogic.PushWechartCorpMessage(acceptId, moduleId, title, subject, Uri);
                }
                if (Landray.Components.Configuration.ConfigurationBroker.DingTalkSettings.EnableDingTalk)
                {
                    //钉钉消息
                    wechartLogic.PushDingTalkMessage(acceptId, moduleId, title, subject, Uri);
                }
                else
                {
                    LEOA.Core.BusinessRules.Organization.User user = new LEOA.Core.BusinessRules.Organization.User(acceptId);
                    string sql = "select fdAccount from mekp_MobileWeChartBinding(nolock) where fdAccount='" + user["account"] + "'";
                    DataTable dt = Landray.DataAccess.DataAccess.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //微云消息
                        wechartLogic.PushWechartMessage(acceptId, moduleId, title, subject, Uri);
                    }
                }
            }
            catch (Exception ex)
            {
                //Landray.Components.LoggingBroker.Error("Landray.Mobile.Logic.MessagePushProvider.Push Error:" + ex.Message);
            }

            /* 消息推送方式停用
            try
            {
                MobilePushMessageLogic logic = new MobilePushMessageLogic();
                logic.PushMessage(acceptId, moduleId, title, subject, Uri);
            }
            catch { }*/
        }

        public override void Push(DataTable touser, DataTable toparty, DataTable totag, string title, string subject, string Uri, int moduleId)
        {
            try
            {
                MobileWeChartLogic wechartLogic = new MobileWeChartLogic();
                if (Landray.Components.Configuration.ConfigurationBroker.WeiXinSettings.EnableWeiXin)
                {
                    //微信企业号消息
                    wechartLogic.PushWechartCorpMessage(touser, moduleId, title, subject, Uri);
                }
                if (Landray.Components.Configuration.ConfigurationBroker.DingTalkSettings.EnableDingTalk)
                {
                    //钉钉消息
                    wechartLogic.PushDingTalkMessage(touser, moduleId, title, subject, Uri);
                }
                else
                {
                    //LEOA.Core.BusinessRules.Organization.User user = new LEOA.Core.BusinessRules.Organization.User(acceptId);
                    //string sql = "select fdAccount from mekp_MobileWeChartBinding(nolock) where fdAccount='" + user["account"] + "'";
                    //DataTable dt = Landray.DataAccess.DataAccess.GetDataTable(sql);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    //微云消息
                    //    wechartLogic.PushWechartMessage(acceptId, moduleId, title, subject, Uri);
                    //}
                }
            }
            catch (Exception ex)
            {
                Landray.Components.LoggingBroker.Error("Landray.Mobile.Logic.MessagePushProvider.Push Error:" + ex.Message);
            }
        }

        /// <summary>
        /// 微信推送消息
        /// </summary>
        /// <param name="acceptId"></param>
        /// <param name="title"></param>
        /// <param name="subject"></param>
        /// <param name="Uri"></param>
        /// <param name="moduleId"></param>
        public override void PushWeChart(int acceptId, string title, string subject, string Uri, int moduleId)
        {
            MobilePushMessageLogic logic = new MobilePushMessageLogic();
            logic.PushMessage(acceptId, moduleId, title, subject, Uri);
        }
    }
}
