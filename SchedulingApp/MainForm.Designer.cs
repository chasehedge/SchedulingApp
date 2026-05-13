namespace SchedulingApp
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.customerTab = new System.Windows.Forms.TabPage();
            this.phoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.customerNameTextBox = new System.Windows.Forms.TextBox();
            this.phoneNumberLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.customerNameLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.customersTable = new System.Windows.Forms.DataGridView();
            this.appointmentTab = new System.Windows.Forms.TabPage();
            this.appointmentEndDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.appointmentStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.appointmentCustomerComboBox = new System.Windows.Forms.ComboBox();
            this.appointmentTypeTextBox = new System.Windows.Forms.TextBox();
            this.appointmentTitleTextBox = new System.Windows.Forms.TextBox();
            this.appointmentEndTimeLabel = new System.Windows.Forms.Label();
            this.appointmentStartTimeLabel = new System.Windows.Forms.Label();
            this.appointmentCustomerLabel = new System.Windows.Forms.Label();
            this.appointmentTypeLabel = new System.Windows.Forms.Label();
            this.appointmentTitleLabel = new System.Windows.Forms.Label();
            this.appointmentDeleteButton = new System.Windows.Forms.Button();
            this.appointmentUpdateButton = new System.Windows.Forms.Button();
            this.appointmentAddButton = new System.Windows.Forms.Button();
            this.appointmentsTable = new System.Windows.Forms.DataGridView();
            this.calendarTab = new System.Windows.Forms.TabPage();
            this.reportsTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.customerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customersTable)).BeginInit();
            this.appointmentTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.customerTab);
            this.tabControl1.Controls.Add(this.appointmentTab);
            this.tabControl1.Controls.Add(this.calendarTab);
            this.tabControl1.Controls.Add(this.reportsTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 537);
            this.tabControl1.TabIndex = 0;
            // 
            // customerTab
            // 
            this.customerTab.Controls.Add(this.phoneNumberTextBox);
            this.customerTab.Controls.Add(this.addressTextBox);
            this.customerTab.Controls.Add(this.customerNameTextBox);
            this.customerTab.Controls.Add(this.phoneNumberLabel);
            this.customerTab.Controls.Add(this.addressLabel);
            this.customerTab.Controls.Add(this.customerNameLabel);
            this.customerTab.Controls.Add(this.deleteButton);
            this.customerTab.Controls.Add(this.updateButton);
            this.customerTab.Controls.Add(this.addButton);
            this.customerTab.Controls.Add(this.customersTable);
            this.customerTab.Location = new System.Drawing.Point(4, 22);
            this.customerTab.Name = "customerTab";
            this.customerTab.Padding = new System.Windows.Forms.Padding(3);
            this.customerTab.Size = new System.Drawing.Size(752, 511);
            this.customerTab.TabIndex = 0;
            this.customerTab.Text = "Customers";
            this.customerTab.UseVisualStyleBackColor = true;
            // 
            // phoneNumberTextBox
            // 
            this.phoneNumberTextBox.Location = new System.Drawing.Point(114, 348);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.phoneNumberTextBox.TabIndex = 13;
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(114, 378);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(100, 20);
            this.addressTextBox.TabIndex = 10;
            // 
            // customerNameTextBox
            // 
            this.customerNameTextBox.Location = new System.Drawing.Point(114, 317);
            this.customerNameTextBox.Name = "customerNameTextBox";
            this.customerNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.customerNameTextBox.TabIndex = 9;
            // 
            // phoneNumberLabel
            // 
            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(20, 351);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(81, 13);
            this.phoneNumberLabel.TabIndex = 8;
            this.phoneNumberLabel.Text = "Phone Number:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(20, 381);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(48, 13);
            this.addressLabel.TabIndex = 5;
            this.addressLabel.Text = "Address:";
            // 
            // customerNameLabel
            // 
            this.customerNameLabel.AutoSize = true;
            this.customerNameLabel.Location = new System.Drawing.Point(20, 320);
            this.customerNameLabel.Name = "customerNameLabel";
            this.customerNameLabel.Size = new System.Drawing.Size(85, 13);
            this.customerNameLabel.TabIndex = 4;
            this.customerNameLabel.Text = "Customer Name:";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(251, 263);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(139, 263);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(23, 263);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // customersTable
            // 
            this.customersTable.AllowUserToAddRows = false;
            this.customersTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customersTable.Location = new System.Drawing.Point(23, 17);
            this.customersTable.Name = "customersTable";
            this.customersTable.Size = new System.Drawing.Size(706, 240);
            this.customersTable.TabIndex = 0;
            this.customersTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customersTable_CellClick);
            // 
            // appointmentTab
            // 
            this.appointmentTab.Controls.Add(this.appointmentEndDateTimePicker);
            this.appointmentTab.Controls.Add(this.appointmentStartDateTimePicker);
            this.appointmentTab.Controls.Add(this.appointmentCustomerComboBox);
            this.appointmentTab.Controls.Add(this.appointmentTypeTextBox);
            this.appointmentTab.Controls.Add(this.appointmentTitleTextBox);
            this.appointmentTab.Controls.Add(this.appointmentEndTimeLabel);
            this.appointmentTab.Controls.Add(this.appointmentStartTimeLabel);
            this.appointmentTab.Controls.Add(this.appointmentCustomerLabel);
            this.appointmentTab.Controls.Add(this.appointmentTypeLabel);
            this.appointmentTab.Controls.Add(this.appointmentTitleLabel);
            this.appointmentTab.Controls.Add(this.appointmentDeleteButton);
            this.appointmentTab.Controls.Add(this.appointmentUpdateButton);
            this.appointmentTab.Controls.Add(this.appointmentAddButton);
            this.appointmentTab.Controls.Add(this.appointmentsTable);
            this.appointmentTab.Location = new System.Drawing.Point(4, 22);
            this.appointmentTab.Name = "appointmentTab";
            this.appointmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.appointmentTab.Size = new System.Drawing.Size(752, 511);
            this.appointmentTab.TabIndex = 1;
            this.appointmentTab.Text = "Appointments";
            this.appointmentTab.UseVisualStyleBackColor = true;
            // 
            // appointmentEndDateTimePicker
            // 
            this.appointmentEndDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.appointmentEndDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointmentEndDateTimePicker.Location = new System.Drawing.Point(93, 447);
            this.appointmentEndDateTimePicker.Name = "appointmentEndDateTimePicker";
            this.appointmentEndDateTimePicker.ShowUpDown = true;
            this.appointmentEndDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.appointmentEndDateTimePicker.TabIndex = 13;
            // 
            // appointmentStartDateTimePicker
            // 
            this.appointmentStartDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.appointmentStartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.appointmentStartDateTimePicker.Location = new System.Drawing.Point(93, 416);
            this.appointmentStartDateTimePicker.Name = "appointmentStartDateTimePicker";
            this.appointmentStartDateTimePicker.ShowUpDown = true;
            this.appointmentStartDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.appointmentStartDateTimePicker.TabIndex = 12;
            // 
            // appointmentCustomerComboBox
            // 
            this.appointmentCustomerComboBox.FormattingEnabled = true;
            this.appointmentCustomerComboBox.Location = new System.Drawing.Point(93, 385);
            this.appointmentCustomerComboBox.Name = "appointmentCustomerComboBox";
            this.appointmentCustomerComboBox.Size = new System.Drawing.Size(121, 21);
            this.appointmentCustomerComboBox.TabIndex = 11;
            // 
            // appointmentTypeTextBox
            // 
            this.appointmentTypeTextBox.Location = new System.Drawing.Point(93, 353);
            this.appointmentTypeTextBox.Name = "appointmentTypeTextBox";
            this.appointmentTypeTextBox.Size = new System.Drawing.Size(100, 20);
            this.appointmentTypeTextBox.TabIndex = 10;
            // 
            // appointmentTitleTextBox
            // 
            this.appointmentTitleTextBox.Location = new System.Drawing.Point(93, 320);
            this.appointmentTitleTextBox.Name = "appointmentTitleTextBox";
            this.appointmentTitleTextBox.Size = new System.Drawing.Size(100, 20);
            this.appointmentTitleTextBox.TabIndex = 9;
            // 
            // appointmentEndTimeLabel
            // 
            this.appointmentEndTimeLabel.AutoSize = true;
            this.appointmentEndTimeLabel.Location = new System.Drawing.Point(20, 447);
            this.appointmentEndTimeLabel.Name = "appointmentEndTimeLabel";
            this.appointmentEndTimeLabel.Size = new System.Drawing.Size(55, 13);
            this.appointmentEndTimeLabel.TabIndex = 8;
            this.appointmentEndTimeLabel.Text = "End Time:";
            // 
            // appointmentStartTimeLabel
            // 
            this.appointmentStartTimeLabel.AutoSize = true;
            this.appointmentStartTimeLabel.Location = new System.Drawing.Point(20, 416);
            this.appointmentStartTimeLabel.Name = "appointmentStartTimeLabel";
            this.appointmentStartTimeLabel.Size = new System.Drawing.Size(58, 13);
            this.appointmentStartTimeLabel.TabIndex = 7;
            this.appointmentStartTimeLabel.Text = "Start Time:";
            // 
            // appointmentCustomerLabel
            // 
            this.appointmentCustomerLabel.AutoSize = true;
            this.appointmentCustomerLabel.Location = new System.Drawing.Point(20, 385);
            this.appointmentCustomerLabel.Name = "appointmentCustomerLabel";
            this.appointmentCustomerLabel.Size = new System.Drawing.Size(54, 13);
            this.appointmentCustomerLabel.TabIndex = 6;
            this.appointmentCustomerLabel.Text = "Customer:";
            // 
            // appointmentTypeLabel
            // 
            this.appointmentTypeLabel.AutoSize = true;
            this.appointmentTypeLabel.Location = new System.Drawing.Point(20, 353);
            this.appointmentTypeLabel.Name = "appointmentTypeLabel";
            this.appointmentTypeLabel.Size = new System.Drawing.Size(34, 13);
            this.appointmentTypeLabel.TabIndex = 5;
            this.appointmentTypeLabel.Text = "Type:";
            // 
            // appointmentTitleLabel
            // 
            this.appointmentTitleLabel.AutoSize = true;
            this.appointmentTitleLabel.Location = new System.Drawing.Point(20, 320);
            this.appointmentTitleLabel.Name = "appointmentTitleLabel";
            this.appointmentTitleLabel.Size = new System.Drawing.Size(30, 13);
            this.appointmentTitleLabel.TabIndex = 4;
            this.appointmentTitleLabel.Text = "Title:";
            // 
            // appointmentDeleteButton
            // 
            this.appointmentDeleteButton.Location = new System.Drawing.Point(251, 263);
            this.appointmentDeleteButton.Name = "appointmentDeleteButton";
            this.appointmentDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.appointmentDeleteButton.TabIndex = 3;
            this.appointmentDeleteButton.Text = "Delete";
            this.appointmentDeleteButton.UseVisualStyleBackColor = true;
            this.appointmentDeleteButton.Click += new System.EventHandler(this.appointmentDeleteButton_Click);
            // 
            // appointmentUpdateButton
            // 
            this.appointmentUpdateButton.Location = new System.Drawing.Point(139, 263);
            this.appointmentUpdateButton.Name = "appointmentUpdateButton";
            this.appointmentUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.appointmentUpdateButton.TabIndex = 2;
            this.appointmentUpdateButton.Text = "Update";
            this.appointmentUpdateButton.UseVisualStyleBackColor = true;
            // 
            // appointmentAddButton
            // 
            this.appointmentAddButton.Location = new System.Drawing.Point(23, 263);
            this.appointmentAddButton.Name = "appointmentAddButton";
            this.appointmentAddButton.Size = new System.Drawing.Size(75, 23);
            this.appointmentAddButton.TabIndex = 1;
            this.appointmentAddButton.Text = "Add";
            this.appointmentAddButton.UseVisualStyleBackColor = true;
            this.appointmentAddButton.Click += new System.EventHandler(this.appointmentAddButton_Click);
            // 
            // appointmentsTable
            // 
            this.appointmentsTable.AllowUserToAddRows = false;
            this.appointmentsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.appointmentsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentsTable.Location = new System.Drawing.Point(23, 17);
            this.appointmentsTable.Name = "appointmentsTable";
            this.appointmentsTable.Size = new System.Drawing.Size(706, 240);
            this.appointmentsTable.TabIndex = 0;
            this.appointmentsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.appointmentsTable_CellClick);
            // 
            // calendarTab
            // 
            this.calendarTab.Location = new System.Drawing.Point(4, 22);
            this.calendarTab.Name = "calendarTab";
            this.calendarTab.Size = new System.Drawing.Size(752, 511);
            this.calendarTab.TabIndex = 2;
            this.calendarTab.Text = "Calendar";
            this.calendarTab.UseVisualStyleBackColor = true;
            // 
            // reportsTab
            // 
            this.reportsTab.Location = new System.Drawing.Point(4, 22);
            this.reportsTab.Name = "reportsTab";
            this.reportsTab.Size = new System.Drawing.Size(752, 511);
            this.reportsTab.TabIndex = 3;
            this.reportsTab.Text = "Reports";
            this.reportsTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.customerTab.ResumeLayout(false);
            this.customerTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customersTable)).EndInit();
            this.appointmentTab.ResumeLayout(false);
            this.appointmentTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage customerTab;
        private System.Windows.Forms.TabPage appointmentTab;
        private System.Windows.Forms.TabPage calendarTab;
        private System.Windows.Forms.TabPage reportsTab;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridView customersTable;
        private System.Windows.Forms.Label phoneNumberLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label customerNameLabel;
        private System.Windows.Forms.TextBox phoneNumberTextBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.TextBox customerNameTextBox;
        private System.Windows.Forms.DataGridView appointmentsTable;
        private System.Windows.Forms.Button appointmentUpdateButton;
        private System.Windows.Forms.Button appointmentAddButton;
        private System.Windows.Forms.Button appointmentDeleteButton;
        private System.Windows.Forms.Label appointmentEndTimeLabel;
        private System.Windows.Forms.Label appointmentStartTimeLabel;
        private System.Windows.Forms.Label appointmentCustomerLabel;
        private System.Windows.Forms.Label appointmentTypeLabel;
        private System.Windows.Forms.Label appointmentTitleLabel;
        private System.Windows.Forms.DateTimePicker appointmentEndDateTimePicker;
        private System.Windows.Forms.DateTimePicker appointmentStartDateTimePicker;
        private System.Windows.Forms.ComboBox appointmentCustomerComboBox;
        private System.Windows.Forms.TextBox appointmentTypeTextBox;
        private System.Windows.Forms.TextBox appointmentTitleTextBox;
    }
}