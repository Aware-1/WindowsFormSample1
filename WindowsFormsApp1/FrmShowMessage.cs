using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public  partial  class  FrmShowMessage : Form
    {
        
     
        //public enum IconType { Success, Warning, Erorr }
        public FrmShowMessage()
        {
            InitializeComponent();
          /*string text, string title, MssgBoxBttn mssgBoxBttn*/ 
          /*
          btnCncl.Visible = false;
          btnNo.Visible = false;
          btnOk.Visible = false;






           pictureWarn.Visible = false;
          pictureOk.Visible = false;
          pictureDanger.Visible = false;
            switch (icon)
          {
              case IconType.Success:
                  pictureOk.Visible = true;

                  break;
              case IconType.Erorr:
                  pictureDanger.Visible = true;

                  break;
              case IconType.Warning:
                  pictureWarn.Visible = true;

                  break;
          }

          label.Text = title;
          Name = title;
      */
        }

        public void  ShowSuccess(string text, string title, MssgBoxBttn mssgBoxBttn)
        {
            switch (mssgBoxBttn)
            {
                case MssgBoxBttn.OK:
                    btnOk.Visible = true;

                    break;
                case MssgBoxBttn.OKCancel:
                    btnCncl.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNoCancel:
                    btnCncl.Visible = true;
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNo:
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
            }
            
            pictureOk.Visible = true;
            label.Text = text;
            Name = title;
        }
        public void ShowWarn(string text, string title, MssgBoxBttn mssgBoxBttn)
        {
            switch (mssgBoxBttn)
            {
                case MssgBoxBttn.OK:
                    btnOk.Visible = true;

                    break;
                case MssgBoxBttn.OKCancel:
                    btnCncl.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNoCancel:
                    btnCncl.Visible = true;
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNo:
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
            }

            pictureWarn.Visible = true;
            label.Text = text;
            Name = title;
        }
        public void ShowDanger(string text, string title, MssgBoxBttn mssgBoxBttn)
        {
            switch (mssgBoxBttn)
            {
                case MssgBoxBttn.OK:
                    btnOk.Visible = true;

                    break;
                case MssgBoxBttn.OKCancel:
                    btnCncl.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNoCancel:
                    btnCncl.Visible = true;
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
                case MssgBoxBttn.YesNo:
                    btnNo.Visible = true;
                    btnOk.Visible = true;
                    break;
            }

            pictureDanger.Visible = true;
            label.Text = text;
            Name = title;
        }
        private void BtnCncl_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnNo_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
