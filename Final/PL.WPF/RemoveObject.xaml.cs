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
        MainWindow Main;
        string Type;
        public RemoveObject(String type, MainWindow main)
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
                    int ID = 0;
                    if (int.TryParse(KeyData.Text, out ID))
                    {
                        try
                        {
                            bl.RemoveLine(ID);
                            this.Close();
                            Main.RefreshList(Main.LinesList);
                        }
                        catch (BO.BadLineIdException ex)
                        {
                            MessageBox.Show(ex.Message + "\nID: " + ex.ID);
                            return;
                        }
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
