using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Globalization;

namespace Calendar.Web
{
    [ToolboxData("<{0}:WebDayView runat=server></{0}:WebDayView>")]
    [Themeable(true)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public partial class WebDayView : System.Web.UI.Control, IPostBackEventHandler
    {
        #region Constants


        #endregion Constants

        #region Enums

        /// <summary>
        /// Calendar type
        /// </summary>
        public enum CalendarType { DayView, WeekView }

        #endregion Enums

        #region Private / Protected Members

        //private ArrayList _columnDetails;
        private System.Web.UI.Control _legend;
        private AppointmentList _appointments = null;

        #endregion Private / Protected Members

        #region Constructors / Destructors

        /// <summary>
        /// Constructor
        /// </summary>
        public WebDayView()
        {
            _appointments = new AppointmentList();
        }


        #endregion Constructors / Destructors

        #region Events

        #region Event Delegates

        public delegate bool ScheduleAllowClashEvent(object sender);

        public delegate void ScheduleGetAppointments(object sender, int column, AppointmentList appointments);

        public delegate void ScheduleAppointmentClick(object sender, Appointment appointment);

        public delegate void ScheduleEmptyCellclick(object sender, DateTime time, int column);

        public delegate bool ScheduleAllowViewAppointment(object sender, Appointment appointment);

        public delegate void ScheduleWeekViewAppointments(object sender, DateTime date, AppointmentList appointments);

        public delegate string ScheduleAppointmentCSSClass(object sender, Appointment appointment);

        //public delegate Therapist ScheduleGetAppointmentOptions(object sender);

        //public delegate Therapists ScheduleLoadStaffMembers(object sender);

        #endregion Event Delegates

        #region Event Declarations

        //public event ScheduleLoadStaffMembers LoadStaffMembers;

        /// <summary>
        /// Retrieves appointment options for the current user (Weekview Only)
        /// </summary>
        //[Description("Retrieves appointment options for the current user (Weekview Only)")]
        //[Category("WeekView")]
        //public event ScheduleGetAppointmentOptions OnGetAppointmentOptions;

        /// <summary>
        /// Specifies wether to allow appointments to clash
        /// </summary>
        [Description("Allow appointments to clash")]
        [Category("Behavior")]
        public event ScheduleAllowClashEvent OnAllowClash;

        /// <summary>
        /// Retrieves appointments
        /// </summary>
        [Description("Retrieve appointments")]
        [Category("Appointments")]
        public event ScheduleGetAppointments OnGetAppointments;

        /// <summary>
        /// Appointment clicked
        /// </summary>
        [Description("Appointmend clicked")]
        [Category("Action")]
        public event ScheduleAppointmentClick OnAppointmentClicked;

        /// <summary>
        /// Empty cell clicked
        /// </summary>
        [Description("Empty cell clicked")]
        [Category("Action")]
        public event ScheduleEmptyCellclick OnEmptyCellClicked;

        /// <summary>
        /// Specifies wether the appointment text can be shown
        /// </summary>
        [Description("Specifies wether the appointment text can be shown")]
        [Category("Display")]
        public event ScheduleAllowViewAppointment OnAllowViewAppointment;

        [Description("Event raised to determine the column count when ViewType is TeamView.")]
        [Category("Team View")]
        public event MultiCountEventHandler MultiCount;

        [Description("Event raised to retrieve the Header Text when ViewType is TeamView.")]
        [Category("Team View")]
        public event MultiGetEventHandler MultiHeader;

        /// <summary>
        /// Called to retrieve appointments for 1 day (WeekView only)
        /// </summary>
        [Description("Called to retrieve appointments for 1 day (WeekView only)")]
        [Category("Team View")]
        public event ScheduleWeekViewAppointments OnGetWeekViewAppointments;

        /// <summary>
        /// Requests the CSS class for the appointment
        /// </summary>
        [Description("Requests the CSS class for the appointment")]
        [Category("Display")]
        public event ScheduleAppointmentCSSClass OnGetAppointmentCSSClass;

        #endregion Event Declarations

        #region Event Wrappers

        /// <summary>
        /// Returns the working hours for 
        /// </summary>
        /// <param name="e"></param>
        internal void RaiseWorkingHours(WorkingHoursEventArgs e)
        {
            //if (WorkingHours != null)
            //    WorkingHours(this, e);
        }

        /// <summary>
        /// Gets the header text for the column in Team View
        /// </summary>
        /// <param name="e">class</param>
        internal void RaiseMultiCountHeader(TeamViewGetEventArgs e)
        {
            if (MultiHeader != null)
                MultiHeader(this, e);
        }

        /// <summary>
        /// Gets the number of columns to show
        /// </summary>
        /// <param name="e"></param>
        internal void RaiseMultiCount(TeamViewCountEventArgs e)
        {
            if (MultiCount != null)
                MultiCount(this, e);
        }

        //private Therapist DoGetAppointmentOptions()
        //{
        //    Therapist Result = null;

        //    if (OnGetAppointmentOptions != null)
        //        Result = OnGetAppointmentOptions(this);

        //    return (Result);
        //}

        private bool DoAllowViewAppointment(Appointment appt)
        {
            bool Result = false;

            if (OnAllowViewAppointment != null)
                Result = OnAllowViewAppointment(this, appt);

            return (Result);
        }

        private void DoOnEmptyCellClicked(DateTime time, int column)
        {
            if (OnEmptyCellClicked != null)
                OnEmptyCellClicked(this, time, column);
        }

        private void DoOnAppointmentClicked(Appointment appt)
        {
            if (OnAppointmentClicked != null)
                OnAppointmentClicked(this, appt);
        }

        private bool DoScheduleAllowClashEvent()
        {
            bool Result = false;

            if (OnAllowClash != null)
                Result = OnAllowClash(this);

            return (Result);
        }

        private void DoGetAppointments(int employeeID, AppointmentList appointments)
        {
            if (OnGetAppointments != null)
                OnGetAppointments(this, employeeID, appointments);
        }

        private void DoGetAppointments(DateTime date, AppointmentList appointments)
        {
            if (OnGetWeekViewAppointments != null)
                OnGetWeekViewAppointments(this, date, appointments);
        }

        private string DoGetAppointmentClass(Appointment appointment)
        {
            string Result = "DiaryAppointmentTaken";

            if (OnGetAppointmentCSSClass != null)
                Result = OnGetAppointmentCSSClass(this, appointment);

            return (Result);
        }

        #endregion Event Wrappers

        #endregion Events

        #region Render

        protected override void Render(HtmlTextWriter output)
        {

            RenderTableStart(output);

            if (_legend != null)
                RenderLegendCell(output);

            RenderTimes(output);

            RenderHeader(output);            
            
            RenderTableEnd(output);
        }

        private void RenderTableStart(HtmlTextWriter output)
        {
            int width = (7 * ColumnWidth) + 50;

            if (_legend != null)
                width += 200;

            //div
            output.AddAttribute("class", "DiaryTableWrapper");
            output.RenderBeginTag("div");

            // <table>
            output.AddAttribute("id", ClientID);
            output.AddAttribute("cellpadding", "0");
            output.AddAttribute("cellspacing", "0");
            output.AddAttribute("border", "1");
            output.AddAttribute("style", String.Format("width: {0}px", width));
            output.AddStyleAttribute("class", "DiaryTable");
            output.AddStyleAttribute("text-align", "left");
            output.RenderBeginTag("table");

            output.RenderBeginTag("tr");
        }

        private void RenderTableEnd(HtmlTextWriter output)
        {
            output.RenderEndTag(); //tr
            output.RenderEndTag(); //table
            output.RenderEndTag(); //div
        }

        private void RenderHeader(HtmlTextWriter output)
        {
            output.AddAttribute("align", "left");
            output.AddAttribute("valign", "top");
            output.RenderBeginTag("td");

            output.AddAttribute("border", "0");
            output.AddAttribute("cellpadding", "0");
            output.AddAttribute("cellspacing", "0");
            output.AddAttribute("width", "100%");
            output.RenderBeginTag("table");

            if (DiaryType == CalendarType.WeekView)
                RenderAppointmentsWeekView(output);
            else
                RenderAppointmentsDayView(output);

            output.RenderBeginTag("tr");

            if (DiaryType == CalendarType.DayView)
            {
                TeamViewCountEventArgs args = new TeamViewCountEventArgs(1);
                RaiseMultiCount(args);

                // add a column for each staff member
                for (int i = 0; i < args.Count; i++)
                {
                    TeamViewGetEventArgs headerArgs = new TeamViewGetEventArgs(i);
                    RaiseMultiCountHeader(headerArgs);

                    output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                    output.AddAttribute("colspan", "1");
                    output.RenderBeginTag("td");

                    output.AddAttribute("class", "DiaryEmployeeNameSmall");
                    output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                    output.AddStyleAttribute("height", String.Format("{0}px", RowHeight));
                    output.RenderBeginTag("div");

                    output.Write(headerArgs.HeaderText);
                    output.RenderEndTag(); //div

                    output.RenderEndTag();//td
                }
            }
            else
            {   //weekview

                DateTime currentDate = CurrentDate;

                for (int i = 1; i <= ColumnCount; i++)
                {
                    output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                    output.AddAttribute("colspan", "1");
                    output.RenderBeginTag("td");

                    output.AddAttribute("class", "DiaryEmployeeNameSmall");
                    output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                    output.AddStyleAttribute("height", String.Format("{0}px", RowHeight));
                    output.RenderBeginTag("div");
                    output.Write(currentDate.ToShortDateString());
                    output.RenderEndTag(); //div

                    output.RenderEndTag();//td

                    currentDate = currentDate.AddDays(1);
                }
            }

            output.RenderEndTag(); //tr

            RenderEmptyCells(output);

            output.RenderEndTag(); // table
            output.RenderEndTag(); // td
        }

        private void RenderAppointmentsDayView(HtmlTextWriter output)
        {
            DateTime firstTimeSlot = GetEarliestStartTime(CurrentDate);
            output.AddAttribute("height", "1px");
            output.RenderBeginTag("tr");

            TeamViewCountEventArgs args = new TeamViewCountEventArgs(1);
            RaiseMultiCount(args);

            // add a column for each person
            for (int i = 0; i < args.Count; i++)
            {
                TeamViewGetEventArgs headerArgs = new TeamViewGetEventArgs(i);
                RaiseMultiCountHeader(headerArgs);

                output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                output.AddAttribute("colspan", "1");
                output.AddAttribute("style", "height: 0px;");
                output.AddAttribute("class", "DiaryAppointmentBlock");
                output.RenderBeginTag("td");

                AppointmentList appointments = new AppointmentList();
                DoGetAppointments(i, appointments);
                
                // add appointments here
                foreach (Appointment appt in appointments)
                {
                    TimeSpan duration = appt.EndDate - appt.StartDate;

                    //div wrapper
                    int divHeight = ((int)duration.TotalMinutes * RowHeight) + 3;

                    TimeSpan spanTop = appt.StartDate - firstTimeSlot;
                    double divTop = (((spanTop.TotalMinutes / 15) * RowHeight) + 1) + RowHeight; // remember header
                    int apptsStacked = _appointments.AppointmentsClash(appt);
                    int divLeft = 0;

                    if (appt.Column > 0)
                    {
                        divLeft = ((ColumnWidth / apptsStacked) * (appt.Column)) + 2;
                        output.AddAttribute("style", String.Format("width:{0}px; height:0px;", (int)(ColumnWidth / apptsStacked) - 4));
                    }
                    else
                        output.AddAttribute("style", String.Format("width:{0}px; height:0px;", (int)(ColumnWidth / apptsStacked)-2));

                    output.AddAttribute("class", "DiaryEntryWrapper");
                    output.RenderBeginTag("div");

                    // mouse div
                    output.AddAttribute("onselectstart", "return false;");
                    output.AddAttribute("onclick", String.Format("javascript:event.cancelBubble=true;{0}", Page.ClientScript.GetPostBackEventReference(this, "APPT:" + appt.ID.ToString())));
                    output.AddAttribute("style", String.Format("cursor:pointer;-moz-user-select:none;-khtml-user-select:none;user-select:none;position:absolute;top:{0}px;height:{1}px;left:{2}px", divTop, divHeight, divLeft));
                    output.RenderBeginTag("div");
                    
                    //mouse over dive
                    output.AddAttribute("onmouseover", "this.style.backgroundColor=&#39;#DCDCDC&#39;;event.cancelBubble=true;");
                    output.AddAttribute("onmouseout", "this.style.backgroundColor=&#39;#FFFFFF&#39;;event.cancelBubble=true;");
                    output.AddAttribute("title", "Appointment: " + appt.ID.ToString());
                    output.AddAttribute("style", String.Format("display:block;height:{0}px;overflow:hidden;", divHeight));
                    output.RenderBeginTag("div");

                    //output.AddAttribute("class", String.Format("{0} DiaryAppointment", apptStyle));
                    output.AddAttribute("style", String.Format("width:{0}px; height:{1}px;", (int)(ColumnWidth / apptsStacked) - 4, divHeight - 4));
                    output.AddAttribute("left", String.Format("{0}px", (ColumnWidth / apptsStacked) * appt.Column));
                    output.AddAttribute("class", "DiaryAppointment " + DoGetAppointmentClass(appt));
                    output.RenderBeginTag("div");
                    output.Write(AppointmentTextFull(appt));
                    output.RenderEndTag(); //div

                    output.RenderEndTag(); // mouse over dive
                    output.RenderEndTag(); // mouse div
                    output.RenderEndTag(); // div wrapper
                }

                output.RenderEndTag();//td
            }

            output.RenderEndTag(); //tr
        }

        private void RenderAppointmentsWeekView(HtmlTextWriter output)
        {
            DateTime firstTimeSlot = GetEarliestStartTime(CurrentDate);
            output.AddAttribute("height", "1px");
            output.RenderBeginTag("tr");

            DateTime currentDate = CurrentDate;
            // add a column for each staff member
            for (int i = 1; i <= ColumnCount; i++)
            {
                output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                output.AddAttribute("colspan", "1");
                output.AddAttribute("style", "height: 0px;");
                output.AddAttribute("class", "DiaryAppointmentBlock");
                output.RenderBeginTag("td");


                //get appointments for the day
                DoGetAppointments(currentDate, _appointments);

                // add appointments here
                foreach (Appointment appt in _appointments)
                {
                    TimeSpan duration = appt.EndDate - appt.StartDate;

                    //div wrapper
                    int divHeight = (int)(duration.TotalMinutes * RowHeight) + 3;
                    
                    TimeSpan spanTop = appt.StartDate - firstTimeSlot;
                    double divTop = (((spanTop.TotalMinutes / 15) * RowHeight) + 1) + RowHeight; // remember header

                    int apptsStacked = _appointments.AppointmentsClash(appt);
                    int divLeft = 0;

                    if (appt.Column > 0)
                    {
                        divLeft = (ColumnWidth / apptsStacked) * (appt.Column);
                    }

                    output.AddAttribute("style", String.Format("width:{0}px; height:0px;", (int)(ColumnWidth / apptsStacked)));
                    output.AddAttribute("class", "DiaryEntryWrapper");
                    output.RenderBeginTag("div");

                    // mouse div
                    output.AddAttribute("onselectstart", "return false;");
                    output.AddAttribute("onclick", String.Format("javascript:event.cancelBubble=true;{0}", Page.ClientScript.GetPostBackEventReference(this, "APPT:" + appt.ID.ToString())));
                    output.AddAttribute("style", String.Format("cursor:pointer;-moz-user-select:none;-khtml-user-select:none;user-select:none;position:absolute;top:{0}px;height:{1}px;left:{2}px", divTop, divHeight, divLeft));
                    output.RenderBeginTag("div");

                    //mouse over dive
                    output.AddAttribute("onmouseover", "this.style.backgroundColor=&#39;#DCDCDC&#39;;event.cancelBubble=true;");
                    output.AddAttribute("onmouseout", "this.style.backgroundColor=&#39;#FFFFFF&#39;;event.cancelBubble=true;");
                    output.AddAttribute("title", "Appointment: " + appt.ID.ToString());
                    output.AddAttribute("style", String.Format("display:block;height:{0}px;overflow:hidden;", divHeight));
                    output.RenderBeginTag("div");

                    //output.AddAttribute("class", String.Format("{0} DiaryAppointment", apptStyle));
                    output.AddAttribute("style", String.Format("width:{0}px; height:{1}px;", (int)(ColumnWidth / apptsStacked) - 4, divHeight - 4));
                    output.AddAttribute("left", String.Format("{0}px", (ColumnWidth / apptsStacked) * appt.Column));
                    output.AddAttribute("class", "DiaryAppointment " + DoGetAppointmentClass(appt));
                    output.RenderBeginTag("div");
                    output.Write(AppointmentTextFull(appt));
                    output.RenderEndTag(); //div

                    output.RenderEndTag(); // mouse over div
                    output.RenderEndTag(); // mouse div
                    output.RenderEndTag(); // div wrapper
                }

                output.RenderEndTag();//td

                currentDate = currentDate.AddDays(1);
            }

            output.RenderEndTag(); //tr
        }

        private void RenderLegendCell(HtmlTextWriter output)
        {
            output.AddAttribute("width", "200px");
            output.AddAttribute("rowspan", "100");
            output.AddAttribute("valign", "top");
            output.AddAttribute("id", "wdLegend");
            output.AddAttribute("runat", "server");
            output.RenderBeginTag("td");

            _legend.RenderControl(output);

            output.RenderEndTag(); //td

        }

        private void RenderTimes(HtmlTextWriter output)
        {
            output.AddAttribute("width", "25px");
            output.AddAttribute("rowspan", "100");
            output.AddAttribute("valign", "top");
            output.RenderBeginTag("td");

            output.RenderBeginTag("table");
            string hour = "";

            //dummy cell to allow for names on header
            output.RenderBeginTag("tr");
            output.AddAttribute("class", "DiaryEmployeeNameSmall");
            output.AddAttribute("colspan", "2");
            output.AddAttribute("valign", "top");
            output.AddStyleAttribute("height", RowHeight.ToString() + "px");
            output.RenderBeginTag("td");
            output.RenderEndTag(); // td
            output.RenderEndTag(); // tr

            for (DateTime time = GetEarliestStartTime(CurrentDate); time < GetLatestFinishTime(CurrentDate); )
            {
                // create a new row
                output.AddAttribute("Height", RowHeight.ToString());
                output.RenderBeginTag("tr");

                // create a cell in the row for the time
                string ApptTime = time.ToString("HH:mm");

                if (ApptTime.Substring(0, 2) != hour)
                {
                    hour = ApptTime.Substring(0, 2);

                    output.AddAttribute("class", "DiaryTimeHour");
                    output.AddAttribute("rowspan", "4");
                    output.AddAttribute("style", String.Format("width:20px;height:{0}px;", RowHeight));
                    output.AddAttribute("valign", "middle");
                    output.RenderBeginTag("td");
                    output.Write(ApptTime.Substring(0, 2));
                    output.RenderEndTag();//td
                }

                output.AddAttribute("class", "DiaryTimeMinute");
                output.AddAttribute("style", "width:12px");
                output.RenderBeginTag("td");
                output.Write(ApptTime.Substring(3, 2));
                output.RenderEndTag(); //td

                output.RenderEndTag(); // tr
                time = time.AddMinutes(15);
            }

            output.RenderEndTag(); //table
            output.RenderEndTag(); // td
        }

        private void RenderEmptyCells(HtmlTextWriter output)
        {
            for (DateTime time = GetEarliestStartTime(CurrentDate); time < GetLatestFinishTime(CurrentDate); )
            {
                // create a new row
                output.AddAttribute("Height", RowHeight.ToString());
                output.RenderBeginTag("tr");

                if (DiaryType == CalendarType.DayView)
                {
                    TeamViewCountEventArgs args = new TeamViewCountEventArgs(1);
                    RaiseMultiCount(args);

                    // add a column for each staff member
                    for (int i = 0; i < args.Count; i++)
                    {
                        TeamViewGetEventArgs headerArgs = new TeamViewGetEventArgs(i);
                        RaiseMultiCountHeader(headerArgs);

                        output.AddAttribute("class", "DiaryEmpteCellTD");
                        output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                        output.AddAttribute("colspan", "1");
                        output.RenderBeginTag("td");

                        if (true) //appts.Options.AllowCreateAppointment(CurrentDate, t))
                        {
                            output.AddAttribute("class", "DiaryAppointmentFree");
                            output.AddAttribute("onclick", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, String.Format("EC:{0} {1}", time.ToString("HH:mm"), i)));
                        }
                        //else
                        //{
                        //    output.AddAttribute("class", "DiaryNotWorking");
                        //}

                        output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth - 2));
                        output.AddStyleAttribute("height", String.Format("{0}px", RowHeight - 2));
                        output.RenderBeginTag("div");
                        output.RenderEndTag(); //div

                        output.RenderEndTag();//td
                    }
                }
                else
                {
                    DateTime currentDate = CurrentDate;

                    for (int i = 1; i <= ColumnCount; i++)
                    {
                        output.AddAttribute("class", "DiaryEmpteCellTD");
                        output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth));
                        output.AddAttribute("colspan", "1");
                        output.RenderBeginTag("td");

                        if (true) //therapist != null && therapist.AllowCreateAppointment(currentDate, t))
                        {
                            output.AddAttribute("class", "DiaryAppointmentFree");
                            output.AddAttribute("onclick", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, String.Format("WVEC:{0} {1}", currentDate.ToShortDateString(), time.ToString("HH:mm"))));
                        }
                        //else
                        //{
                        //    output.AddAttribute("class", "DiaryNotWorking");
                        //}

                        output.AddStyleAttribute("width", String.Format("{0}px", ColumnWidth - 2));
                        output.AddStyleAttribute("height", String.Format("{0}px", RowHeight - 2));
                        output.RenderBeginTag("div");
                        output.RenderEndTag(); //div

                        output.RenderEndTag();//td

                        currentDate = currentDate.AddDays(1);
                    }
                }
                
                output.RenderEndTag(); // tr

                time = time.AddMinutes(15);
            }
        }

        #endregion Render

        #region Post Back

        public void RaisePostBackEvent(string eventArgument)
        {
            IFormatProvider culture = new CultureInfo("en-GB", true);

            
            if (eventArgument.StartsWith("APPT:"))
            {
                string appt = eventArgument.Substring(5, eventArgument.Length - 5);
                //DoOnAppointmentClicked(Appointments.Get(HSCUtils.StrToIntDef(appt, -1)));
            }
            else if (eventArgument.StartsWith("EC:"))
            {
                string[] Args = eventArgument.Substring(3, eventArgument.Length - 3).Split(' ');
                DateTime time = Convert.ToDateTime(String.Format("{0} {1}", CurrentDate.ToShortDateString(), Convert.ToString(Args[0])), culture);
                string therapist = Args[1];
                //DoOnEmptyCellClicked(time, Therapist.Get(Convert.ToInt32(Args[1])));
            }
            else if (eventArgument.StartsWith("WVEC"))
            {
                //DateTime time = HSCUtils.StrToDateTime(eventArgument.Substring(5), culture.ToString());
                //DoOnEmptyCellClicked(time, null);
            }
            else
            {
                throw new ArgumentException("Bad argument passed from postback event.");
            }
        }

        #endregion Post Back

        #region Properties

        /// <summary>
        /// Specifies wether past dates are allowed
        /// </summary>
        [Description("Specifies the number of columns to display")]
        [Category("WeekView")]
        [DefaultValue(7)]
        public int ColumnCount
        {
            get
            {
                if (ViewState["ColumnCount"] == null)
                    return (7);

                return ((int)ViewState["ColumnCount"]);
            }

            set
            {
                if (value < 1)
                    ViewState["ColumnCount"] = 1;
                else
                    ViewState["ColumnCount"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the start of the business day (in hours).
        /// </summary>
        [Description("Start of the working day (hour from 0 to 23).")]
        [Category("WeekView")]
        [DefaultValue(9)]
        public int StartTime
        {
            get
            {
                if (ViewState["BusinessBeginsHour"] == null)
                    return 9;
                return (int)ViewState["BusinessBeginsHour"];
            }
            set
            {
                if (value < 0)
                    ViewState["BusinessBeginsHour"] = 0;
                else if (value > 23)
                    ViewState["BusinessBeginsHour"] = 23;
                else
                    ViewState["BusinessBeginsHour"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the end of the business day (hours).
        /// </summary>
        [Description("End of the working day (hour from 1 to 24).")]
        [Category("WeekView")]
        [DefaultValue(18)]
        public int FinishTime
        {
            get
            {
                if (ViewState["BusinessEndsHour"] == null)
                    return 18;

                return (int)ViewState["BusinessEndsHour"];
            }
            set
            {
                if (value < StartTime)
                    ViewState["BusinessEndsHour"] = StartTime + 1;
                else if (value > 24)
                    ViewState["BusinessEndsHour"] = 24;
                else
                    ViewState["BusinessEndsHour"] = value;
            }
        }


        /// <summary>
        /// Specifies wether past dates are allowed
        /// </summary>
        [Description("Specifies the type of calendar to display")]
        [Category("Display")]
        [DefaultValue(CalendarType.WeekView)]
        public CalendarType DiaryType
        {
            get
            {
                if (ViewState["CalendarType"] == null)
                    ViewState["CalendarType"] = CalendarType.WeekView;

                return ((CalendarType)ViewState["CalendarType"]);
            }

            set
            {
                ViewState["CalendarType"] = value;

                //if (value == CalendarType.DayView)
                //    DoOnLoadStaffMembers();
            }
        }

        /// <summary>
        /// Specifies wether past dates are allowed
        /// </summary>
        [Description("Specifies wether to allow dates in the past")]
        [Category("Settings")]
        [DefaultValue(false)]
        public bool AllowPastDates
        {
            get
            {
                if (ViewState["AllowPastDates"] == null)
                    ViewState["AllowPastDates"] = false;

                return ((bool)ViewState["AllowPastDates"]);
            }

            set
            {
                ViewState["AllowPastDates"] = value;
            }
        }

        /// <summary>
        /// Width of each column
        /// </summary>
        [Description("Width of column")]
        [Category("Display")]
        [DefaultValue(180)]
        public int ColumnWidth
        {
            get
            {
                if (ViewState["ColumnWidth"] == null)
                    ViewState["ColumnWidth"] = 180;

                return ((int)ViewState["ColumnWidth"]);
            }

            set
            {
                ViewState["ColumnWidth"] = value;
            }
        }

        /// <summary>
        /// Width of each column
        /// </summary>
        [Description("Height of rows")]
        [Category("Display")]
        [DefaultValue(24)]
        public int RowHeight
        {
            get
            {
                if (ViewState["RowHeight"] == null)
                    ViewState["RowHeight"] = 24;

                return ((int)ViewState["RowHeight"]);
            }

            set
            {
                int newHeight = value;

                if (newHeight < 10)
                    newHeight = 10;

                if (newHeight > 40)
                    newHeight = 40;

                if (newHeight % 2 == 1)
                    newHeight--;

                ViewState["RowHeight"] = newHeight;
            }
        }

        /// <summary>
        /// Specifies wether to show cancelled appointments
        /// </summary>
        [Description("Show cancelled appointments")]
        [Category("Behavior")]
        [DefaultValue(180)]
        public bool ShowCancelledAppointments
        {
            get
            {
                if (ViewState["ShowCancelledAppointments"] == null)
                    ViewState["ShowCancelledAppointments"] = false;

                return ((bool)ViewState["ShowCancelledAppointments"]);
            }

            set
            {
                ViewState["ShowCancelledAppointments"] = value;
            }
        }

        /// <summary>
        /// Specifies wether to show private calendars
        /// </summary>
        [Description("Show Private Calendars")]
        [Category("Behavior")]
        [DefaultValue(180)]
        public bool ShowPrivateCalendars
        {
            get
            {
                if (ViewState["ShowPrivateCalendars"] == null)
                    ViewState["ShowPrivateCalendars"] = false;

                return ((bool)ViewState["ShowPrivateCalendars"]);
            }

            set
            {
                ViewState["ShowPrivateCalendars"] = value;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("Current Date")]
        [Category("Settings")]
        [DefaultValue(180)]
        public DateTime CurrentDate
        {
            get
            {
                if (ViewState["CurrentDate"] == null)
                    ViewState["CurrentDate"] = DateTime.Now;

                return ((DateTime)ViewState["CurrentDate"]);
            }

            set
            {
                ViewState["CurrentDate"] = value;

                // if not employee and date is in the past then show todays date
                if ((DateTime)ViewState["CurrentDate"] < DateTime.Now && !AllowPastDates)
                    ViewState["CurrentDate"] = DateTime.Now;
            }
        }


        #endregion Properties

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Protected Methods

        #region Public Methods

        public void AddLegend(System.Web.UI.Control control)
        {
            _legend = control;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddHeaderRow()
        {
        }

        private DateTime GetEarliestStartTime(DateTime date)
        {
            DateTime Result = date.Date.AddHours(9);

            if (DiaryType == CalendarType.DayView)
            {
                //foreach (Appointments appts in _staffMembers)
                //{
                //    if (appts.Options.StartTime < Result)
                //        Result = appts.Options.StartTime;
                //}
            }
            else
            {
                //Result = Convert.ToDouble(StartTime - 1);
            }

            return (Result);
        }

        private DateTime GetLatestFinishTime(DateTime date)
        {
            DateTime Result = date.Date.AddHours(17);

            if (DiaryType == CalendarType.DayView)
            {
                //foreach (Appointments appts in _staffMembers)
                //{
                //    if (appts.Options.EndTime > Result)
                //        Result = appts.Options.EndTime;
                //}
            }
            else
            {
                //Result = Convert.ToDouble(FinishTime + 1);
            }

            return (Result);
        }

        //private string EmptyAppointmentText(Appointments Appointments, string AppointmentTime)
        //{
        //    string Result = "";
        //    double endTime;
        //    endTime = HSCUtils.TimeToDouble(AppointmentTime);

        //    //if ((GetUsersMemberLevel() >= EMPLOYEE) || ((endTime <= Appointments.Options.EndTime) && (Appointments.Options.AllowCreateAppointment(_currentDate))))
        //    if (((endTime < Appointments.Options.EndTime) && (Appointments.Options.AllowCreateAppointment(CurrentDate))))
        //    {
        //        Result = String.Format("<a href=\"/Diary/Staff/FindUser.aspx?Date={2}&Therapist={0}&StartTime={1}&Staff=1\" alt=\"Create Appointment\"><img src=\"/Images/Diary/AddAppt.gif\" align=\"left\" border=\"0\" /></a>",
        //            Appointments.Options.EmployeeID, AppointmentTime, CurrentDate.ToShortDateString());
        //    }

        //    return (Result);
        //}

        private string AppointmentTextFull(Appointment appt)
        {
            string Result;

            if (DoAllowViewAppointment(appt))
            {
                Result = "need a way of getting appointment text";// appt.AppointmentLink;
            }
            else
            {
                Result = "Unknown Appointment";
            }

            return (Result);
        }

        #endregion Private Methods

        #region Overridden Functions

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        #region Viewstate

        /// <summary>
        /// Loads ViewState.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState == null)
                return;

            object[] vs = (object[])savedState;

            if (vs.Length != 2)
            {
                throw new ArgumentException("Wrong savedState object.");
            }

            if (vs[0] != null)
            {
                base.LoadViewState(vs[0]);
            }

            if (vs[1] != null)
            {
                //_staffMembers = (ArrayList)vs[1];
            }

        }

        /// <summary>
        /// Saves ViewState.
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            object[] vs = new object[2];
            vs[0] = base.SaveViewState();
            //vs[1] = _staffMembers;

            return vs;
        }

        #endregion Viewstate

        #endregion Overridden Functions
    }
}