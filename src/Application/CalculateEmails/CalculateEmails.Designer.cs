namespace CalculateEmails
{
    partial class CalculateEmails : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CalculateEmails()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.box2 = this.Factory.CreateRibbonBox();
            this.button2 = this.Factory.CreateRibbonButton();
            this.lblInCounter = this.Factory.CreateRibbonLabel();
            this.box3 = this.Factory.CreateRibbonBox();
            this.button3 = this.Factory.CreateRibbonButton();
            this.lblOutCouter = this.Factory.CreateRibbonLabel();
            this.box4 = this.Factory.CreateRibbonBox();
            this.button4 = this.Factory.CreateRibbonButton();
            this.lblProcessedCounter = this.Factory.CreateRibbonLabel();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.button1 = this.Factory.CreateRibbonButton();
            this.lblTaskAdd = this.Factory.CreateRibbonLabel();
            this.box5 = this.Factory.CreateRibbonBox();
            this.button5 = this.Factory.CreateRibbonButton();
            this.lblTaskRemoved = this.Factory.CreateRibbonLabel();
            this.box6 = this.Factory.CreateRibbonBox();
            this.button6 = this.Factory.CreateRibbonButton();
            this.lblTaskFinished = this.Factory.CreateRibbonLabel();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.chHeartBeat = this.Factory.CreateRibbonCheckBox();
            this.chCalculateEmailsEnabled = this.Factory.CreateRibbonCheckBox();
            this.btnClearInvitation = this.Factory.CreateRibbonButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tab2 = this.Factory.CreateRibbonTab();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.box7 = this.Factory.CreateRibbonBox();
            this.button7 = this.Factory.CreateRibbonButton();
            this.lblInCounter2 = this.Factory.CreateRibbonLabel();
            this.box8 = this.Factory.CreateRibbonBox();
            this.button8 = this.Factory.CreateRibbonButton();
            this.lblOutCouter2 = this.Factory.CreateRibbonLabel();
            this.box9 = this.Factory.CreateRibbonBox();
            this.button9 = this.Factory.CreateRibbonButton();
            this.lblProcessedCounter2 = this.Factory.CreateRibbonLabel();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.box10 = this.Factory.CreateRibbonBox();
            this.button10 = this.Factory.CreateRibbonButton();
            this.lblTaskAdd2 = this.Factory.CreateRibbonLabel();
            this.box11 = this.Factory.CreateRibbonBox();
            this.button11 = this.Factory.CreateRibbonButton();
            this.lblTaskRemoved2 = this.Factory.CreateRibbonLabel();
            this.box12 = this.Factory.CreateRibbonBox();
            this.button12 = this.Factory.CreateRibbonButton();
            this.lblTaskFinished2 = this.Factory.CreateRibbonLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.box2.SuspendLayout();
            this.box3.SuspendLayout();
            this.box4.SuspendLayout();
            this.group2.SuspendLayout();
            this.box1.SuspendLayout();
            this.box5.SuspendLayout();
            this.box6.SuspendLayout();
            this.group5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tab2.SuspendLayout();
            this.group3.SuspendLayout();
            this.box7.SuspendLayout();
            this.box8.SuspendLayout();
            this.box9.SuspendLayout();
            this.group4.SuspendLayout();
            this.box10.SuspendLayout();
            this.box11.SuspendLayout();
            this.box12.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabMail";
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group5);
            this.tab1.Label = "TabMail";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.box2);
            this.group1.Items.Add(this.box3);
            this.group1.Items.Add(this.box4);
            this.group1.Label = "Emails";
            this.group1.Name = "group1";
            // 
            // box2
            // 
            this.box2.Items.Add(this.button2);
            this.box2.Items.Add(this.lblInCounter);
            this.box2.Name = "box2";
            // 
            // button2
            // 
            this.button2.Image = global::CalculateEmails.Properties.Resources.Slice_1;
            this.button2.Label = "In";
            this.button2.Name = "button2";
            this.button2.ShowImage = true;
            this.button2.ShowLabel = false;
            // 
            // lblInCounter
            // 
            this.lblInCounter.Label = "0";
            this.lblInCounter.Name = "lblInCounter";
            // 
            // box3
            // 
            this.box3.Items.Add(this.button3);
            this.box3.Items.Add(this.lblOutCouter);
            this.box3.Name = "box3";
            // 
            // button3
            // 
            this.button3.Image = global::CalculateEmails.Properties.Resources.Out;
            this.button3.Label = "Out";
            this.button3.Name = "button3";
            this.button3.ShowImage = true;
            this.button3.ShowLabel = false;
            // 
            // lblOutCouter
            // 
            this.lblOutCouter.Label = "0";
            this.lblOutCouter.Name = "lblOutCouter";
            // 
            // box4
            // 
            this.box4.Items.Add(this.button4);
            this.box4.Items.Add(this.lblProcessedCounter);
            this.box4.Name = "box4";
            // 
            // button4
            // 
            this.button4.Image = global::CalculateEmails.Properties.Resources.Processed;
            this.button4.Label = "Processed";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            this.button4.ShowLabel = false;
            // 
            // lblProcessedCounter
            // 
            this.lblProcessedCounter.Label = "0";
            this.lblProcessedCounter.Name = "lblProcessedCounter";
            // 
            // group2
            // 
            this.group2.Items.Add(this.box1);
            this.group2.Items.Add(this.box5);
            this.group2.Items.Add(this.box6);
            this.group2.Label = "Tasks";
            this.group2.Name = "group2";
            // 
            // box1
            // 
            this.box1.Items.Add(this.button1);
            this.box1.Items.Add(this.lblTaskAdd);
            this.box1.Name = "box1";
            // 
            // button1
            // 
            this.button1.Image = global::CalculateEmails.Properties.Resources.TaskCrated;
            this.button1.Label = "In";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.ShowLabel = false;
            // 
            // lblTaskAdd
            // 
            this.lblTaskAdd.Label = "0";
            this.lblTaskAdd.Name = "lblTaskAdd";
            // 
            // box5
            // 
            this.box5.Items.Add(this.button5);
            this.box5.Items.Add(this.lblTaskRemoved);
            this.box5.Name = "box5";
            // 
            // button5
            // 
            this.button5.Image = global::CalculateEmails.Properties.Resources.TaskDeleted;
            this.button5.Label = "Out";
            this.button5.Name = "button5";
            this.button5.ShowImage = true;
            this.button5.ShowLabel = false;
            // 
            // lblTaskRemoved
            // 
            this.lblTaskRemoved.Label = "0";
            this.lblTaskRemoved.Name = "lblTaskRemoved";
            // 
            // box6
            // 
            this.box6.Items.Add(this.button6);
            this.box6.Items.Add(this.lblTaskFinished);
            this.box6.Name = "box6";
            // 
            // button6
            // 
            this.button6.Image = global::CalculateEmails.Properties.Resources.TaskProcessed;
            this.button6.Label = "Processed";
            this.button6.Name = "button6";
            this.button6.ShowImage = true;
            this.button6.ShowLabel = false;
            // 
            // lblTaskFinished
            // 
            this.lblTaskFinished.Label = "0";
            this.lblTaskFinished.Name = "lblTaskFinished";
            // 
            // group5
            // 
            this.group5.Items.Add(this.chHeartBeat);
            this.group5.Items.Add(this.chCalculateEmailsEnabled);
            this.group5.Items.Add(this.btnClearInvitation);
            this.group5.Label = "Manage";
            this.group5.Name = "group5";
            // 
            // chHeartBeat
            // 
            this.chHeartBeat.Label = "Online";
            this.chHeartBeat.Name = "chHeartBeat";
            // 
            // chCalculateEmailsEnabled
            // 
            this.chCalculateEmailsEnabled.Label = "Enabled";
            this.chCalculateEmailsEnabled.Name = "chCalculateEmailsEnabled";
            // 
            // btnClearInvitation
            // 
            this.btnClearInvitation.Label = "Clear invitations";
            this.btnClearInvitation.Name = "btnClearInvitation";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tab2
            // 
            this.tab2.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab2.ControlId.OfficeId = "TabTasks";
            this.tab2.Groups.Add(this.group3);
            this.tab2.Groups.Add(this.group4);
            this.tab2.Label = "TabTasks";
            this.tab2.Name = "tab2";
            // 
            // group3
            // 
            this.group3.Items.Add(this.box7);
            this.group3.Items.Add(this.box8);
            this.group3.Items.Add(this.box9);
            this.group3.Label = "Emails";
            this.group3.Name = "group3";
            // 
            // box7
            // 
            this.box7.Items.Add(this.button7);
            this.box7.Items.Add(this.lblInCounter2);
            this.box7.Name = "box7";
            // 
            // button7
            // 
            this.button7.Image = global::CalculateEmails.Properties.Resources.Slice_1;
            this.button7.Label = "In";
            this.button7.Name = "button7";
            this.button7.ShowImage = true;
            this.button7.ShowLabel = false;
            // 
            // lblInCounter2
            // 
            this.lblInCounter2.Label = "0";
            this.lblInCounter2.Name = "lblInCounter2";
            // 
            // box8
            // 
            this.box8.Items.Add(this.button8);
            this.box8.Items.Add(this.lblOutCouter2);
            this.box8.Name = "box8";
            // 
            // button8
            // 
            this.button8.Image = global::CalculateEmails.Properties.Resources.Out;
            this.button8.Label = "Out";
            this.button8.Name = "button8";
            this.button8.ShowImage = true;
            this.button8.ShowLabel = false;
            // 
            // lblOutCouter2
            // 
            this.lblOutCouter2.Label = "0";
            this.lblOutCouter2.Name = "lblOutCouter2";
            // 
            // box9
            // 
            this.box9.Items.Add(this.button9);
            this.box9.Items.Add(this.lblProcessedCounter2);
            this.box9.Name = "box9";
            // 
            // button9
            // 
            this.button9.Image = global::CalculateEmails.Properties.Resources.Processed;
            this.button9.Label = "Processed";
            this.button9.Name = "button9";
            this.button9.ShowImage = true;
            this.button9.ShowLabel = false;
            // 
            // lblProcessedCounter2
            // 
            this.lblProcessedCounter2.Label = "0";
            this.lblProcessedCounter2.Name = "lblProcessedCounter2";
            // 
            // group4
            // 
            this.group4.Items.Add(this.box10);
            this.group4.Items.Add(this.box11);
            this.group4.Items.Add(this.box12);
            this.group4.Label = "Tasks";
            this.group4.Name = "group4";
            // 
            // box10
            // 
            this.box10.Items.Add(this.button10);
            this.box10.Items.Add(this.lblTaskAdd2);
            this.box10.Name = "box10";
            // 
            // button10
            // 
            this.button10.Image = global::CalculateEmails.Properties.Resources.TaskCrated;
            this.button10.Label = "In";
            this.button10.Name = "button10";
            this.button10.ShowImage = true;
            this.button10.ShowLabel = false;
            // 
            // lblTaskAdd2
            // 
            this.lblTaskAdd2.Label = "0";
            this.lblTaskAdd2.Name = "lblTaskAdd2";
            // 
            // box11
            // 
            this.box11.Items.Add(this.button11);
            this.box11.Items.Add(this.lblTaskRemoved2);
            this.box11.Name = "box11";
            // 
            // button11
            // 
            this.button11.Image = global::CalculateEmails.Properties.Resources.TaskDeleted;
            this.button11.Label = "Out";
            this.button11.Name = "button11";
            this.button11.ShowImage = true;
            this.button11.ShowLabel = false;
            // 
            // lblTaskRemoved2
            // 
            this.lblTaskRemoved2.Label = "0";
            this.lblTaskRemoved2.Name = "lblTaskRemoved2";
            // 
            // box12
            // 
            this.box12.Items.Add(this.button12);
            this.box12.Items.Add(this.lblTaskFinished2);
            this.box12.Name = "box12";
            // 
            // button12
            // 
            this.button12.Image = global::CalculateEmails.Properties.Resources.TaskProcessed;
            this.button12.Label = "Processed";
            this.button12.Name = "button12";
            this.button12.ShowImage = true;
            this.button12.ShowLabel = false;
            // 
            // lblTaskFinished2
            // 
            this.lblTaskFinished2.Label = "0";
            this.lblTaskFinished2.Name = "lblTaskFinished2";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CalculateEmails
            // 
            this.Name = "CalculateEmails";
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Compose, Microsoft.Outlook.Tas" +
    "k";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab2);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.CalculateEmails_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box3.ResumeLayout(false);
            this.box3.PerformLayout();
            this.box4.ResumeLayout(false);
            this.box4.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.box5.ResumeLayout(false);
            this.box5.PerformLayout();
            this.box6.ResumeLayout(false);
            this.box6.PerformLayout();
            this.group5.ResumeLayout(false);
            this.group5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.box7.ResumeLayout(false);
            this.box7.PerformLayout();
            this.box8.ResumeLayout(false);
            this.box8.PerformLayout();
            this.box9.ResumeLayout(false);
            this.box9.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.box10.ResumeLayout(false);
            this.box10.PerformLayout();
            this.box11.ResumeLayout(false);
            this.box11.PerformLayout();
            this.box12.ResumeLayout(false);
            this.box12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblInCounter;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblOutCouter;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblProcessedCounter;
        private System.Windows.Forms.ImageList imageList1;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskAdd;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskRemoved;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box6;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button6;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskFinished;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box7;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button7;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblInCounter2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box8;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button8;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblOutCouter2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box9;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button9;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblProcessedCounter2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box10;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button10;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskAdd2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box11;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button11;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskRemoved2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box12;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button12;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel lblTaskFinished2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chHeartBeat;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnClearInvitation;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chCalculateEmailsEnabled;
    }

    partial class ThisRibbonCollection
    {
        internal CalculateEmails CalculateEmails
        {
            get { return this.GetRibbon<CalculateEmails>(); }
        }
    }
}
