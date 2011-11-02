using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Foms.GUI.UserControl;
using Foms.Services;
using Foms.Shared;
using System.IO;

namespace Foms.GUI.Accounting.Sage
{
    public partial class FrmExportSage : Form
    {
        public FrmExportSage()
        {
            InitializeComponent();
            _initializeDates();
            _displayAccountsTiers();
        }

        private void _initializeDates()
        {
            dateTimePickerBeginDateAccountTiers.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, 01);
            dateTimePickerEndDateAccountTiers.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, DateTime.DaysInMonth(TimeProvider.Today.Year, TimeProvider.Today.Month));
            dateTimePickerBeginDateBookings.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, 01);
            dateTimePickerEndDateBookings.Value = new DateTime(TimeProvider.Today.Year, TimeProvider.Today.Month, DateTime.DaysInMonth(TimeProvider.Today.Year, TimeProvider.Today.Month));
        }

        private void _buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _displayAccountsTiers()
        {
            listViewAccountsTiers.Items.Clear();
            List<SageAccountTiers> accountsTiers = ServicesProvider.GetInstance().GetSageServices().SelectDisbursedLoans(dateTimePickerBeginDateAccountTiers.Value, dateTimePickerEndDateAccountTiers.Value);

            foreach (SageAccountTiers accountTiers in accountsTiers)
            {
                ListViewItem item = new ListViewItem(accountTiers.ContractCode);
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.FrmExportSage, accountTiers.ClientType.ToString() + ".Text"));
                item.SubItems.Add(accountTiers.ClientName);
                item.SubItems.Add(accountTiers.LoanOfficerName);
                item.SubItems.Add(accountTiers.DisbursmentDate.ToShortDateString());
                item.SubItems.Add(accountTiers.Name);
                item.SubItems.Add(accountTiers.Title);
                item.SubItems.Add(accountTiers.CollectiveAccount.ToString());
                item.Tag = accountTiers;
                item.Checked = true;

                listViewAccountsTiers.Items.Add(item);
            }

            _setSelectedAccountTiers();
            labelTotalAccountTiers.Text = accountsTiers.Count.ToString();
        }

        private void _displayBookings()
        {
            listViewBookings.Items.Clear();
            listViewBookings.Groups.Clear();

            int previousMvtSetId = 0;
            Color color = Color.Gray;
            List<SageBooking> bookings = ServicesProvider.GetInstance().GetSageServices().SelectBookings(dateTimePickerBeginDateBookings.Value, dateTimePickerEndDateBookings.Value, checkBoxShowExportedBookings.Checked);

            foreach (SageBooking booking in bookings)
            {
                if (booking.MovementSetId != previousMvtSetId)
                {
                    color = color == Color.Gray ? Color.White : Color.Gray;
                    previousMvtSetId = booking.MovementSetId;
                }
                ListViewItem item = new ListViewItem(booking.JournalCode) { BackColor = color };
                item.SubItems.Add(booking.ContractCode);
                item.SubItems.Add(booking.ClientName);
                item.SubItems.Add(booking.Date.ToShortDateString());
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, booking.BookingType + ".Text"));
                item.SubItems.Add(booking.AccountTiers);
                item.SubItems.Add(booking.Account.ToString());
                item.SubItems.Add(booking.Amount.RoundingValue);
                item.SubItems.Add(booking.PartNumber);
                item.SubItems.Add(booking.Reference);
                item.SubItems.Add(MultiLanguageStrings.GetString(Ressource.FrmExportSage, booking.Direction.ToString() + ".Text"));
                item.Tag = booking;
                item.Checked = true;

                listViewBookings.Items.Add(item);
            }

            _setSelectedBookings();
            labelTotalBookings.Text = bookings.Count.ToString();
        }

        private void _setCheckedAllAccountTiers(bool value)
        {
            foreach (ListViewItem item in listViewAccountsTiers.Items)
            {
                item.Checked = value;
            }
        }

        private void _setSelectedAccountTiers()
        {
            labelSelectedAccountTiers.Text = listViewAccountsTiers.CheckedItems.Count.ToString();
        }

        private void _setCheckedAllBookings(bool value)
        {
            listViewBookings.ItemChecked -= listViewBookings_ItemChecked;
            foreach (ListViewItem item in listViewBookings.Items)
            {
                item.Checked = value;
            }
            listViewBookings.ItemChecked += listViewBookings_ItemChecked;
            _setSelectedBookings();
        }

        private void _setSelectedBookings()
        {
            labelSelectedBookings.Text = listViewBookings.CheckedItems.Count.ToString();
        }

        private void _exportAccountTiers()
        {
            List<SageAccountTiers> selectedAccountTiers = new List<SageAccountTiers>();
            foreach (ListViewItem item in listViewAccountsTiers.CheckedItems)
            {
                selectedAccountTiers.Add(item.Tag as SageAccountTiers);
            }

            string export = ServicesProvider.GetInstance().GetSageServices().ExportAccountTiers(selectedAccountTiers);
            
            if (saveFileDialogAccountTiers.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialogAccountTiers.FileName;
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(export);
                sw.Flush();
                fs.Close();

                MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmExportSage, "AccountTiersSuccess.Text"));
            }
        }

        private void _exportBookings()
        {
            try
            {
                List<SageBooking> selectedBookings = new List<SageBooking>();
                foreach (ListViewItem item in listViewBookings.CheckedItems)
                {
                    selectedBookings.Add(item.Tag as SageBooking);
                }

                string export = ServicesProvider.GetInstance().GetSageServices().ExportBookings(selectedBookings);

                if (saveFileDialogBookings.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialogBookings.FileName;
                    FileStream fs = new FileStream(path, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(export);
                    sw.Flush();
                    fs.Close();

                    MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmExportSage, "BookingsSuccess.Text"));
                    if (MessageBox.Show(MultiLanguageStrings.GetString(Ressource.FrmExportSage, "SetBookingsAsExported.Text"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ServicesProvider.GetInstance().GetSageServices().SetBookingsExported(selectedBookings);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            _displayAccountsTiers();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            _setCheckedAllAccountTiers(true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            _setCheckedAllAccountTiers(false);
        }

        private void listViewAccountsTiers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
           _setSelectedAccountTiers();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            _exportAccountTiers();
        }

        private void buttonSelectAllBookings_Click(object sender, EventArgs e)
        {
            _setCheckedAllBookings(true);
        }

        private void buttonDeselectAllBookings_Click(object sender, EventArgs e)
        {
            _setCheckedAllBookings(false);
        }

        private void buttonRefreshBookings_Click(object sender, EventArgs e)
        {
            _displayBookings();
        }

        private void listViewBookings_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            listViewBookings.ItemChecked -= listViewBookings_ItemChecked;
            foreach (ListViewItem item in listViewBookings.Items)
            {
                if (item != e.Item)
                    if (((SageBooking)item.Tag).MovementSetId == ((SageBooking)e.Item.Tag).MovementSetId)
                        item.Checked = e.Item.Checked;
            }

            listViewBookings.ItemChecked += listViewBookings_ItemChecked;
            _setSelectedBookings();        
        }

        private void buttonExportBookings_Click(object sender, EventArgs e)
        {
            _exportBookings();
        }

        private void _labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxShowExportedBookings_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
