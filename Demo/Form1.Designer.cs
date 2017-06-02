namespace DayView2Demo
{
    partial class frmDayViewDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDayViewDemo));
            Calendar.DrawTool drawTool1 = new Calendar.DrawTool();
            this.contextMenuStripAllDay = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripDiary = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createAppointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAppointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAppointmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editWorkingHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstPeople = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTeamView = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDayView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonViewType = new System.Windows.Forms.ToolStripDropDownButton();
            this.dayViewToolStripMenuItem5Days = new System.Windows.Forms.ToolStripMenuItem();
            this.teamViewToolStripMenuItem7Days = new System.Windows.Forms.ToolStripMenuItem();
            this.cbAllowAppointmentMove = new System.Windows.Forms.CheckBox();
            this.cbAllowNew = new System.Windows.Forms.CheckBox();
            this.cbAllowAppointmentResize = new System.Windows.Forms.CheckBox();
            this.dayView1 = new Calendar.DayView();
            this.contextMenuStripDiary.SuspendLayout();
            this.contextMenuStripHeader.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripAllDay
            // 
            this.contextMenuStripAllDay.Name = "contextMenuStripAllDay";
            this.contextMenuStripAllDay.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStripDiary
            // 
            this.contextMenuStripDiary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAppointmentToolStripMenuItem,
            this.editAppointmentToolStripMenuItem,
            this.deleteAppointmentToolStripMenuItem});
            this.contextMenuStripDiary.Name = "contextMenuStripDiary";
            this.contextMenuStripDiary.Size = new System.Drawing.Size(183, 70);
            this.contextMenuStripDiary.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDiary_Opening);
            // 
            // createAppointmentToolStripMenuItem
            // 
            this.createAppointmentToolStripMenuItem.Name = "createAppointmentToolStripMenuItem";
            this.createAppointmentToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.createAppointmentToolStripMenuItem.Text = "Create Appointment";
            this.createAppointmentToolStripMenuItem.Click += new System.EventHandler(this.createAppointmentToolStripMenuItem_Click);
            // 
            // editAppointmentToolStripMenuItem
            // 
            this.editAppointmentToolStripMenuItem.Name = "editAppointmentToolStripMenuItem";
            this.editAppointmentToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.editAppointmentToolStripMenuItem.Text = "Edit Appointment";
            this.editAppointmentToolStripMenuItem.Click += new System.EventHandler(this.editAppointmentToolStripMenuItem_Click);
            // 
            // deleteAppointmentToolStripMenuItem
            // 
            this.deleteAppointmentToolStripMenuItem.Name = "deleteAppointmentToolStripMenuItem";
            this.deleteAppointmentToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.deleteAppointmentToolStripMenuItem.Text = "Delete Appointment";
            this.deleteAppointmentToolStripMenuItem.Click += new System.EventHandler(this.deleteAppointmentToolStripMenuItem_Click);
            // 
            // contextMenuStripHeader
            // 
            this.contextMenuStripHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPersonToolStripMenuItem,
            this.editWorkingHoursToolStripMenuItem});
            this.contextMenuStripHeader.Name = "contextMenuStripHeader";
            this.contextMenuStripHeader.Size = new System.Drawing.Size(178, 48);
            // 
            // editPersonToolStripMenuItem
            // 
            this.editPersonToolStripMenuItem.Name = "editPersonToolStripMenuItem";
            this.editPersonToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.editPersonToolStripMenuItem.Text = "Edit Person";
            // 
            // editWorkingHoursToolStripMenuItem
            // 
            this.editWorkingHoursToolStripMenuItem.Name = "editWorkingHoursToolStripMenuItem";
            this.editWorkingHoursToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.editWorkingHoursToolStripMenuItem.Text = "Edit Working Hours";
            // 
            // lstPeople
            // 
            this.lstPeople.CheckOnClick = true;
            this.lstPeople.FormattingEnabled = true;
            this.lstPeople.Items.AddRange(new object[] {
            "Billy Bob",
            "Tim",
            "Sally",
            "Peter",
            "Trinny"});
            this.lstPeople.Location = new System.Drawing.Point(11, 224);
            this.lstPeople.Name = "lstPeople";
            this.lstPeople.Size = new System.Drawing.Size(224, 94);
            this.lstPeople.TabIndex = 0;
            this.lstPeople.SelectedIndexChanged += new System.EventHandler(this.lstPeople_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "People";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(8, 34);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 3;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTeamView,
            this.toolStripButtonDayView,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonViewType});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(975, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonTeamView
            // 
            this.toolStripButtonTeamView.Checked = true;
            this.toolStripButtonTeamView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonTeamView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonTeamView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTeamView.Image")));
            this.toolStripButtonTeamView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTeamView.Name = "toolStripButtonTeamView";
            this.toolStripButtonTeamView.Size = new System.Drawing.Size(40, 22);
            this.toolStripButtonTeamView.Text = "Team";
            this.toolStripButtonTeamView.Click += new System.EventHandler(this.toolStripButtonTeamView_Click);
            // 
            // toolStripButtonDayView
            // 
            this.toolStripButtonDayView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDayView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDayView.Image")));
            this.toolStripButtonDayView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDayView.Name = "toolStripButtonDayView";
            this.toolStripButtonDayView.Size = new System.Drawing.Size(31, 22);
            this.toolStripButtonDayView.Text = "Day";
            this.toolStripButtonDayView.Click += new System.EventHandler(this.toolStripButtonDayView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButtonViewType
            // 
            this.toolStripDropDownButtonViewType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonViewType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dayViewToolStripMenuItem5Days,
            this.teamViewToolStripMenuItem7Days});
            this.toolStripDropDownButtonViewType.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonViewType.Image")));
            this.toolStripDropDownButtonViewType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonViewType.Name = "toolStripDropDownButtonViewType";
            this.toolStripDropDownButtonViewType.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButtonViewType.Text = "Days";
            this.toolStripDropDownButtonViewType.ToolTipText = "View Type";
            // 
            // dayViewToolStripMenuItem5Days
            // 
            this.dayViewToolStripMenuItem5Days.Name = "dayViewToolStripMenuItem5Days";
            this.dayViewToolStripMenuItem5Days.Size = new System.Drawing.Size(108, 22);
            this.dayViewToolStripMenuItem5Days.Text = "5 Days";
            this.dayViewToolStripMenuItem5Days.Click += new System.EventHandler(this.dayViewToolStripMenuItem5Days_Click);
            // 
            // teamViewToolStripMenuItem7Days
            // 
            this.teamViewToolStripMenuItem7Days.Name = "teamViewToolStripMenuItem7Days";
            this.teamViewToolStripMenuItem7Days.Size = new System.Drawing.Size(108, 22);
            this.teamViewToolStripMenuItem7Days.Text = "7 Days";
            this.teamViewToolStripMenuItem7Days.Click += new System.EventHandler(this.teamViewToolStripMenuItem7Days_Click);
            // 
            // cbAllowAppointmentMove
            // 
            this.cbAllowAppointmentMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAllowAppointmentMove.AutoSize = true;
            this.cbAllowAppointmentMove.Checked = true;
            this.cbAllowAppointmentMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowAppointmentMove.Location = new System.Drawing.Point(8, 509);
            this.cbAllowAppointmentMove.Name = "cbAllowAppointmentMove";
            this.cbAllowAppointmentMove.Size = new System.Drawing.Size(164, 17);
            this.cbAllowAppointmentMove.TabIndex = 5;
            this.cbAllowAppointmentMove.Text = "Allow Appointments To Move";
            this.cbAllowAppointmentMove.UseVisualStyleBackColor = true;
            // 
            // cbAllowNew
            // 
            this.cbAllowNew.AutoSize = true;
            this.cbAllowNew.Checked = true;
            this.cbAllowNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowNew.Location = new System.Drawing.Point(8, 486);
            this.cbAllowNew.Name = "cbAllowNew";
            this.cbAllowNew.Size = new System.Drawing.Size(76, 17);
            this.cbAllowNew.TabIndex = 6;
            this.cbAllowNew.Text = "Allow New";
            this.cbAllowNew.UseVisualStyleBackColor = true;
            this.cbAllowNew.CheckedChanged += new System.EventHandler(this.cbAllowNew_CheckedChanged);
            // 
            // cbAllowAppointmentResize
            // 
            this.cbAllowAppointmentResize.AutoSize = true;
            this.cbAllowAppointmentResize.Checked = true;
            this.cbAllowAppointmentResize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllowAppointmentResize.Location = new System.Drawing.Point(8, 463);
            this.cbAllowAppointmentResize.Name = "cbAllowAppointmentResize";
            this.cbAllowAppointmentResize.Size = new System.Drawing.Size(148, 17);
            this.cbAllowAppointmentResize.TabIndex = 7;
            this.cbAllowAppointmentResize.Text = "Allow Appointment Resize";
            this.cbAllowAppointmentResize.UseVisualStyleBackColor = true;
            this.cbAllowAppointmentResize.CheckedChanged += new System.EventHandler(this.cbAllowAppointmentResize_CheckedChanged);
            // 
            // dayView1
            // 
            drawTool1.DayView = this.dayView1;
            this.dayView1.ActiveTool = drawTool1;
            this.dayView1.AlwaysShowAppointmentText = true;
            this.dayView1.AmPmDisplay = false;
            this.dayView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dayView1.ContextMenuAllDay = this.contextMenuStripAllDay;
            this.dayView1.ContextMenuDiary = this.contextMenuStripDiary;
            this.dayView1.ContextMenuHeader = this.contextMenuStripHeader;
            this.dayView1.DrawAllAppointmentBorders = false;
            this.dayView1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dayView1.Location = new System.Drawing.Point(247, 34);
            this.dayView1.Name = "dayView1";
            this.dayView1.RightMouseSelect = true;
            this.dayView1.SelectedAppointment = null;
            this.dayView1.SelectionEnd = new System.DateTime(((long)(0)));
            this.dayView1.SelectionStart = new System.DateTime(((long)(0)));
            this.dayView1.ShowMinutes = true;
            this.dayView1.Size = new System.Drawing.Size(716, 492);
            this.dayView1.StartDate = new System.DateTime(((long)(0)));
            this.dayView1.TabIndex = 2;
            this.dayView1.Text = "dayView1";
            this.dayView1.AppointmentSelected += new Calendar.AppointmentSelectedEventHandler(this.dayView1_AppointmentSelected);
            this.dayView1.SelectionChanged += new System.EventHandler(this.dayView1_SelectionChanged);
            this.dayView1.ResolveAppointments += new Calendar.ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            this.dayView1.NewAppointment += new Calendar.NewAppointmentEventHandler(this.dayView1_NewAppointment);
            this.dayView1.AppointmentUpdated += new System.EventHandler<Calendar.AppointmentEventArgs>(this.dayView1_AppointmentUpdated);
            this.dayView1.BeforeAppointmentMove += new Calendar.BeforeMoveAppointmentEventHandler(this.dayView1_BeforeAppointmentMove);
            this.dayView1.AppointmentMoved += new System.EventHandler<Calendar.AppointmentEventArgs>(this.dayView1_AppointmentMoved);
            this.dayView1.MultiCount += new Calendar.MultiCountEventHandler(this.dayView1_MultiCount);
            this.dayView1.MultiHeader += new Calendar.MultiGetEventHandler(this.dayView1_MultiHeader);
            this.dayView1.ToolTipShow += new Calendar.TooltipEventHandler(this.dayView1_ToolTipShow);
            this.dayView1.WorkingHours += new Calendar.WorkingHoursEventHandler(this.dayView1_WorkingHours);
            this.dayView1.HeaderClicked += new Calendar.HeaderClickEventHandler(this.dayView1_HeaderClicked);
            // 
            // frmDayViewDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.cbAllowAppointmentResize);
            this.Controls.Add(this.cbAllowNew);
            this.Controls.Add(this.cbAllowAppointmentMove);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.dayView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPeople);
            this.Name = "frmDayViewDemo";
            this.Text = "DayView2 Demo";
            this.contextMenuStripDiary.ResumeLayout(false);
            this.contextMenuStripHeader.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstPeople;
        private System.Windows.Forms.Label label1;
        private Calendar.DayView dayView1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTeamView;
        private System.Windows.Forms.ToolStripButton toolStripButtonDayView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonViewType;
        private System.Windows.Forms.ToolStripMenuItem dayViewToolStripMenuItem5Days;
        private System.Windows.Forms.ToolStripMenuItem teamViewToolStripMenuItem7Days;
        private System.Windows.Forms.CheckBox cbAllowAppointmentMove;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAllDay;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDiary;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripHeader;
        private System.Windows.Forms.ToolStripMenuItem createAppointmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editAppointmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editWorkingHoursToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbAllowNew;
        private System.Windows.Forms.ToolStripMenuItem deleteAppointmentToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbAllowAppointmentResize;
    }
}

