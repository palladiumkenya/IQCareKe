using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Application.Common;

    namespace Application.Presentation
    {
    public class IQCareWindowMsgBox
    {

        #region "Alert Msgbox"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageId">For retrieving message from xml file corresponding to MessageId </param>
        /// <param name="frmName">The form control on which message will be shown</param>
        public static void ShowWindow(string MessageId, System.Windows.Forms.Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            ShowWindow(theMsg.ToString(), theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageId">For retrieving message from xml file corresponding to MessageId</param>
        /// <param name="MessageBuilder">Send user defined message</param>
        /// <param name="frmName">The form control on which message will be shown</param>
        public static void ShowWindow(string MessageId, MsgBuilder MessageBuilder, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            ShowWindow(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        #endregion

        #region "Confirm Msgbox"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageId">For retrieving message from xml file corresponding to MessageId </param>
        /// <param name="frmName">The form control on which message will be shown</param>
        public static DialogResult ShowWindowConfirm(string MessageId, Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            return ShowWindow(theMsg.ToString(), theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageId">For retrieving message from xml file corresponding to MessageId</param>
        /// <param name="MessageBuilder">Send user defined message</param>
        /// <param name="frmName">The form control on which message will be shown</param>
        public static DialogResult ShowWindowConfirm(string MessageId, MsgBuilder MessageBuilder, System.Windows.Forms.Control frmName)
        {
            RawMessage theMsg = MsgRepository.GetMessage(MessageId);
            MessageBuilder.MsgRepository[MessageId] = theMsg.ToString();
            string theDynamicMsg = MessageBuilder.BuildMessage(MessageId);
            //ShowConfirm(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), cntrlName);
            return ShowWindow(theDynamicMsg, theMsg.Type.ToString(), theMsg.Buttons.ToString(), frmName);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Msg">Message to be displayed</param>
        /// <param name="Style">Style of dialog box like error dialog box, information dialog box etc.</param>
        /// <param name="Buttons">which type of message box need to be shown like okcancel, ok etc</param>
        /// <param name="frmName">The form control on which message will be shown</param>
        /// <returns></returns>
        public static DialogResult ShowWindow(string Msg, string Style, string Buttons, Control frmName)
        {
            DialogResult dlgResult;
            string theAlert = "";
            MessageBoxIcon enmMsgBoxType;
            MessageBoxButtons enmMsgBoxBtn;

            switch (Style)
            {
                case "!":
                    theAlert = "IQCare Management";
                    enmMsgBoxType = MessageBoxIcon.Information;
                    enmMsgBoxBtn = MessageBoxButtons.OK;
                    break;
                case "?":
                    theAlert = "IQCare Management";
                    enmMsgBoxType = MessageBoxIcon.Question;
                    enmMsgBoxBtn = MessageBoxButtons.YesNo;
                    break;
                default:
                    theAlert = "IQCare Management";
                    enmMsgBoxType = MessageBoxIcon.Error;
                    enmMsgBoxBtn = MessageBoxButtons.OK;
                    break;
            }

            ///// At this movement we are not using Buttons Parameter as 

            ///// Converting into ASP
            StringBuilder theSB;
            //Msg = Msg.Replace("'", "");
            //Msg = Msg.Replace("\n", "\\n");
            //Msg = Msg.Replace("\r", "");
            string tmpMsg = Msg;

            theSB = new StringBuilder();
            theSB.Append(Msg);
            dlgResult = MessageBox.Show(Msg, theAlert, enmMsgBoxBtn, enmMsgBoxType);

            return dlgResult;

        }
    }
    }
