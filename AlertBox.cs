using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcode
{
    public static class AlertBox
    {
        public static System.Windows.Forms.DialogResult ShowMessage(string message, string caption, System.Windows.Forms.MessageBoxButtons button, System.Windows.Forms.MessageBoxIcon icon)
        {
            System.Windows.Forms.DialogResult dlgResult = System.Windows.Forms.DialogResult.None;
            switch (button)
            {
                case System.Windows.Forms.MessageBoxButtons.OK:
                    using (AlertBoxOK msgOK = new AlertBoxOK())
                    {
                        //Change text, caption, icon
                        msgOK.Text = caption;
                        msgOK.Message = message;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Information:
                                msgOK.MessageIcon = Properties.Resources.success;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                msgOK.MessageIcon = Properties.Resources.question;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Error:
                                msgOK.MessageIcon = Properties.Resources.error;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Warning:
                                msgOK.MessageIcon = Properties.Resources.warning;
                                break;
                        }
                        dlgResult = msgOK.ShowDialog();
                    }
                    break;
                case System.Windows.Forms.MessageBoxButtons.YesNo:
                    using (AlertBoxYesNo msgYesNo = new AlertBoxYesNo())
                    {
                        msgYesNo.Text = caption;
                        msgYesNo.Message = message;
                        switch (icon)
                        {
                            case System.Windows.Forms.MessageBoxIcon.Question:
                                msgYesNo.MessageIcon = Properties.Resources.question;
                                break;
                            case System.Windows.Forms.MessageBoxIcon.Warning:
                                msgYesNo.MessageIcon = Properties.Resources.warning;
                                break;
                        }
                        dlgResult = msgYesNo.ShowDialog();
                    }
                    break;
            }
            return dlgResult;
        }
    }
}
