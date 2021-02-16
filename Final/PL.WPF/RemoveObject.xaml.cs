using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for RemoveObject.xaml
    /// </summary>
    public partial class RemoveObject : Window
    {
        IBL bl = BLFactory.GetBL(1);
        ManagerWindow Main;
        string Type;
        public RemoveObject(String type, ManagerWindow main)
        {
            InitializeComponent();

            Main = main;
            Type = type;
            switch (type)
            {
                case "Bus":
                    KeyReq.Text = "Enter LN:";
                    break;
                case "Line":
                    KeyReq.Text = "Enter ID:";
                    KeyReq2.Text = "Enter Code:";
                    KeyReq2.Visibility = Visibility.Visible;
                    KeyData2.Visibility = Visibility.Visible;
                    break;
                case "Station":
                    KeyReq.Text = "Enter Code:";
                    break;
            }
        }

        private void RemoveObj(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case "Bus":
                    int LN = 0;
                    if(int.TryParse(KeyData.Text, out LN))
                    {
                        try
                        {
                            bl.RemoveBus(LN);
                            this.Close();
                            Main.RefreshList(Main.BusesList);
                        }
                        catch(BO.BadBusIdException ex)
                        {
                            MessageBox.Show(ex.Message + "\nID: " + ex.ID);
                            return;
                        }
                    }
                    break;
                case "Line":
                    int LineID = 0;
                    int LineCode = 0;
                    if (int.TryParse(KeyData.Text, out LineID) && int.TryParse(KeyData2.Text,out LineCode))
                    {
                        try
                        {
                            bl.RemoveLine(LineID, LineCode);
                            this.Close();
                            Main.RefreshList(Main.LinesList);
                        }
                        catch (BO.BadLineIdException ex)
                        {
                            MessageBox.Show(ex.Message + "\nID: " + ex.ID);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("invalid ID or Code");
                        return;
                    }
                    break;
                case "Station":
                    int Code = 0;
                    if (int.TryParse(KeyData.Text, out Code))
                    {
                        try
                        {
                            bl.RemoveStation(Code);
                            this.Close();
                            Main.RefreshList(Main.StationsList);
                        }
                        catch (BO.BadStationIdException ex)
                        {
                            MessageBox.Show(ex.Message + "\nID: " + ex.ID);
                            return;
                        }
                    }
                    break;
            }
            
        }
    }
}
