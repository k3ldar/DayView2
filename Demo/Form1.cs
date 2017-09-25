using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DayView2Demo
{
    public partial class frmDayViewDemo : Form
    {
        #region Private Members

        List<Calendar.Appointment> _appointments;
        private int[] _overlappedAppointments = new int[] { 600, 600, 660, 720, 720, 720,
            810, 810, 840, 840, 870, 960, 960, 960 };
        private int[] _overlapDurations = new int[] { 120, 120, 120, 105, 60, 60, 45, 45, 135, 135, 120, 120, 120, 120 };

        private int _daysToView = 5;

        #endregion Private Members

        #region Constructors

        public frmDayViewDemo()
        {
            _appointments = new List<Calendar.Appointment>();

            InitializeComponent();

            dayView1.ViewType = Calendar.DayView.DayViewType.TeamView;

            //select all people
            for (int i = 0; i < lstPeople.Items.Count; i++)
                lstPeople.SetItemChecked(i, true);

            lstPeople.SelectedIndex = 0;

            CreateRandomAppointments();

            ForceRefresh();

            UpdateUI();
        }

        #endregion Constructors

        #region Private Methods

        private void ForceRefresh()
        {
            UpdateUI();
            dayView1.StartDate = monthCalendar1.SelectionStart;
            dayView1.Refresh();
        }

        private void UpdateUI()
        {
            toolStripButtonTeamView.Checked = dayView1.ViewType == Calendar.DayView.DayViewType.TeamView;
            toolStripButtonDayView.Checked = !toolStripButtonTeamView.Checked;
            dayView1.Focus();
        }

        private void CreateRandomAppointments()
        {
            //allday appointments
            for (int i = 0; i < 2; ++i)
            {
                Calendar.Appointment appt = new Calendar.Appointment();
                appt.AllDayEvent = true;
                appt.Color = Color.BurlyWood;
                DateTime date = DateTime.Now.Date;
                appt.StartDate = date;
                appt.EndDate = date.AddDays(i);
                appt.Title = String.Format("All Day Appointment {0}", i);
                _appointments.Add(appt);
            }

            Random rnd = new Random();

            foreach (string person in lstPeople.Items)
            {
                for (int i = 2; i < 5; i++) // four each
                {
                    for (int day = 0; day < 7; ++day) //per day
                    {
                        Calendar.Appointment appt = new Calendar.Appointment();
                        appt.Object = person;
                        appt.Color = Color.BlanchedAlmond; // dont ask me why I chose this color?
                        DateTime date = DateTime.Now.Date.AddDays(day).AddHours(rnd.Next(10, 17));
                        appt.StartDate = date;
                        appt.EndDate = date.AddMinutes(i * 15);
                        appt.Title = String.Format("Test Appointment {0}", i);
                        _appointments.Add(appt);
                    }
                }
            }

            // overlapped appointments
            string firstPerson = (string)lstPeople.Items[0];

            for (int i = 0; i < _overlappedAppointments.Length; i++)
            {
                Calendar.Appointment appt = new Calendar.Appointment();
                appt.Object = firstPerson;
                appt.Color = Color.BlanchedAlmond; // dont ask me why I chose this color?
                DateTime date = DateTime.Now.Date.AddMinutes(_overlappedAppointments[i]);
                appt.StartDate = date;
                appt.EndDate = date.AddMinutes(_overlapDurations[i]);
                appt.Title = String.Format("Overlap {0}", i);
                _appointments.Add(appt);
            }
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "DayView2 Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Private Methods

        #region Tool Strip Events

        private void toolStripButtonTeamView_Click(object sender, EventArgs e)
        {
            dayView1.ViewType = Calendar.DayView.DayViewType.TeamView;
            ForceRefresh();
        }

        private void toolStripButtonDayView_Click(object sender, EventArgs e)
        {
            dayView1.ViewType = Calendar.DayView.DayViewType.SingleView;

            //in single view remove all other selections
            for (int i = 0; i < lstPeople.Items.Count; ++i)
            {
                lstPeople.SetItemChecked(i, i == lstPeople.SelectedIndex);
            }

            ForceRefresh();
        }

        private void dayViewToolStripMenuItem5Days_Click(object sender, EventArgs e)
        {
            _daysToView = 5;
            ForceRefresh();
        }

        private void teamViewToolStripMenuItem7Days_Click(object sender, EventArgs e)
        {
            _daysToView = 7;
            ForceRefresh();
        }

        #endregion Tool Strip Events

        #region DayView Events

        private void dayView1_AppointmentSelected(object sender, Calendar.AppointmentSelectedEventArgs e)
        {
            if (e.Selected)
            {
                if (e.Appointment != null)
                    e.Appointment.Color = Color.Aqua;
            }
            else
            {
                if (e.Appointment != null)
                    e.Appointment.Color = Color.BlanchedAlmond;
            }
        }

        private void dayView1_BeforeAppointmentMove(object sender, Calendar.AppointmentMoveEventArgs e)
        {
            e.AllowMove = cbAllowAppointmentMove.Checked;
        }

        private void dayView1_ResolveAppointments(object sender, Calendar.ResolveAppointmentsEventArgs e)
        {
            List<Calendar.Appointment> resolvedAppointments = new List<Calendar.Appointment>();

            foreach (Calendar.Appointment appt in _appointments)
            {
                if ((!appt.AllDayEvent && (appt.StartDate.Date >= e.StartDate.Date && appt.EndDate.Date <= e.EndDate)
                    || (appt.AllDayEvent && dayView1.DateWithin(e.StartDate, e.EndDate, appt.StartDate, appt.EndDate))))
                {
                    if (dayView1.SelectedAppointment != appt)
                    {
                        if (dayView1.ViewType == Calendar.DayView.DayViewType.TeamView)
                        {
                            //if its team view get the column for the person
                            appt.Column = GetPeopleColumn((string)appt.Object);

                            //if person not found, continue...
                            if (!appt.AllDayEvent && appt.Column == -1)
                                continue;
                        }
                        else
                        {
                            //if dayview only show appointments for selected person
                            if (!appt.AllDayEvent && (string)appt.Object != (string)lstPeople.CheckedItems[0])
                                continue;
                        }
                    }

                    resolvedAppointments.Add(appt);
                }
            }

            e.Appointments = resolvedAppointments;

        }

        private void dayView1_AppointmentMoved(object sender, Calendar.AppointmentEventArgs e)
        {
            //has it changed to another user?
            if (!e.Appointment.AllDayEvent)
                e.Appointment.Object = lstPeople.CheckedItems[e.Appointment.Column];

            ShowMessage("Appointment Moved");
        }

        private void dayView1_HeaderClicked(object sender, Calendar.HeaderClickEventArgs e)
        {
            string message = "You double clicked header for ";

            if (dayView1.ViewType == Calendar.DayView.DayViewType.SingleView)
                message += dayView1.StartDate.AddDays(e.Column).ToShortDateString();
            else
                message += (string)lstPeople.CheckedItems[e.Column];

            ShowMessage(message);
        }

        private void dayView1_MultiCount(object sender, Calendar.TeamViewCountEventArgs e)
        {
            //how many columns to show in day view?
            if (dayView1.ViewType == Calendar.DayView.DayViewType.SingleView)
                e.Count = _daysToView;
            else
                e.Count = lstPeople.CheckedItems.Count;
        }

        private void dayView1_MultiHeader(object sender, Calendar.TeamViewGetEventArgs e)
        {
            if (dayView1.ViewType == Calendar.DayView.DayViewType.TeamView)
                e.HeaderText = (string)lstPeople.CheckedItems[e.Index];
            else
                e.HeaderText = dayView1.StartDate.AddDays(e.Index).ToShortDateString();
        }

        private void dayView1_WorkingHours(object sender, Calendar.WorkingHoursEventArgs e)
        {
            //Just an example of working hours, derive proper data from DB or similar
            switch (e.Column)
            {
                case 0:
                    e.WorkingHourStart = 10;
                    e.WorkingMinuteStart = 0;
                    e.WorkingHourFinish = 18;
                    e.WorkingMinuteFinish = 0;
                    break;
                case 1:
                case 3:
                    e.WorkingHourStart = 9;
                    e.WorkingMinuteStart = 30;
                    e.WorkingHourFinish = 17;
                    e.WorkingMinuteFinish = 30;
                    break;
                default:
                    e.WorkingHourStart = 9;
                    e.WorkingMinuteStart = 0;
                    e.WorkingHourFinish = 17;
                    e.WorkingMinuteFinish = 0;
                    break;
            }
        }

        private void dayView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dayView1_NewAppointment(object sender, Calendar.NewAppointmentEventArgs e)
        {
            Calendar.Appointment appt = new Calendar.Appointment();
            appt.StartDate = e.StartDate;
            appt.EndDate = e.EndDate;
            appt.Title = e.Title;
            appt.ID = _appointments.Count;
            appt.Color = Color.BlanchedAlmond;
            appt.Column = e.Column;
            appt.Object = lstPeople.CheckedItems[e.Column];
            _appointments.Add(appt);
            ForceRefresh();
        }

        private void dayView1_ToolTipShow(object sender, Calendar.TooltipEventArgs e)
        {
            e.ShowBalloon = false;

            if (e.Appointment != null)
            {
                e.Title = "Appointment for " + (string)e.Appointment.Object;
                e.Text = "Hovering over appointment";
                e.ShowBalloon = false;
            }
            else
            {
                if (e.CurrentCellDateTime < DateTime.Now)
                    e.Text = "Can not create appointment in the past";
                else
                    e.Text = "Double Click to add an appointment";
            }

            e.AllowShow = true;
        }

        private void dayView1_AppointmentUpdated(object sender, Calendar.AppointmentEventArgs e)
        {
            ShowMessage("Appointment Updated");
        }

        #endregion DayView Events

        #region People

        private int GetPeopleColumn(string Person)
        {
            int Result = -1;

            for (int i = 0; i < lstPeople.CheckedItems.Count; ++i)
            {
                if ((string)lstPeople.CheckedItems[i] == Person)
                {
                    Result = i;
                    break;
                }
            }

            return (Result);
        }

        private void lstPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dayView1.ViewType == Calendar.DayView.DayViewType.SingleView)
            {
                //in single view remove all other selections
                for (int i = 0; i < lstPeople.Items.Count; ++i)
                {
                    lstPeople.SetItemChecked(i, i == lstPeople.SelectedIndex);
                }
            }

            ForceRefresh();
        }

        #endregion People

        #region Context Menu Strips

        private void contextMenuStripDiary_Opening(object sender, CancelEventArgs e)
        {
            editAppointmentToolStripMenuItem.Enabled = dayView1.SelectedAppointment != null;
            createAppointmentToolStripMenuItem.Enabled = !editAppointmentToolStripMenuItem.Enabled;
            deleteAppointmentToolStripMenuItem.Enabled = dayView1.SelectedAppointment != null;
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMessage("Edit Appointment");
        }

        private void createAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calendar.Appointment appt = new Calendar.Appointment();
            appt.ID = _appointments.Count;
            appt.Title = "New Appointment";

            DateTime date;
            int column;
            dayView1.GetColumnFromMousePosition(out column, out date);

            appt.StartDate = dayView1.SelectionStart;
            appt.EndDate = dayView1.SelectionEnd;
            appt.Object = lstPeople.CheckedItems[column];
            appt.Column = column;
            _appointments.Add(appt);
            dayView1.Invalidate();
        }

        private void deleteAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                _appointments.Remove(dayView1.SelectedAppointment);
                ForceRefresh();
            }
        }

        #endregion Context Menu Strips


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ForceRefresh();
        }

        private void cbAllowNew_CheckedChanged(object sender, EventArgs e)
        {
            dayView1.AllowNew = cbAllowNew.Checked;
        }

        private void cbAllowAppointmentResize_CheckedChanged(object sender, EventArgs e)
        {
            dayView1.AllowAppointmentResize = cbAllowAppointmentResize.Checked;
        }

    }
}
