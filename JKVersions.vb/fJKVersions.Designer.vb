<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fJKVersions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.bCancel = New System.Windows.Forms.Button
        Me.pbProgress = New System.Windows.Forms.ProgressBar
        Me.lStatus = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'bCancel
        '
        Me.bCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bCancel.Location = New System.Drawing.Point(215, 54)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 23)
        Me.bCancel.TabIndex = 2
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'pbProgress
        '
        Me.pbProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbProgress.Location = New System.Drawing.Point(12, 25)
        Me.pbProgress.Maximum = 900000000
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(278, 23)
        Me.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbProgress.TabIndex = 1
        '
        'lStatus
        '
        Me.lStatus.AutoSize = True
        Me.lStatus.Location = New System.Drawing.Point(12, 9)
        Me.lStatus.Name = "lStatus"
        Me.lStatus.Size = New System.Drawing.Size(96, 13)
        Me.lStatus.TabIndex = 0
        Me.lStatus.Text = "Preparing to start..."
        '
        'fJKVersions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.bCancel
        Me.ClientSize = New System.Drawing.Size(302, 89)
        Me.Controls.Add(Me.lStatus)
        Me.Controls.Add(Me.pbProgress)
        Me.Controls.Add(Me.bCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fJKVersions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jedi Knight Versions Downloader"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents pbProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents lStatus As System.Windows.Forms.Label

End Class
