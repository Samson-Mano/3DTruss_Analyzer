Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions
Public Class Options_Form

    Private Sub Options_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.is_childFormOpen = False '-- Update the main form that this form is closed
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub Options_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(MDIMain.Location.X + 0, MDIMain.Location.Y + 57)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Opacity = 0.9
        Me.BringToFront()
        Me.TopMost = True


        TextBox_ScValue.Text = MDIMain.threeD_Control.Sc_val4
        CheckBox_NodeNumb.Checked = MDIMain.Is_DrawNodeNum
        CheckBox_NodeCoord.Checked = MDIMain.Is_DrawNodeCoord
        CheckBox_ElemLen.Checked = MDIMain.Is_DrawElemLength
        CheckBox_GridLines.Checked = MDIMain.Is_DrawGrid
        CheckBox_OrginPt.Checked = MDIMain.Is_DrawZeroPt
        CheckBox_Load.Checked = MDIMain.Is_DrawLoad
        CheckBox_Loadvalue.Checked = MDIMain.Is_DrawLoadValue
        CheckBox_Constraints.Checked = MDIMain.Is_DrawConst
        CheckBox_ResultValues.Checked = MDIMain.Is_DrawResultValues
        CheckBox_UndeformedView.Checked = MDIMain.Is_DrawUndeformedView

        TextBox_loadScale.Text = MDIMain.FElements.Load_scaleLen
        TextBox_ConstScale.Text = MDIMain.FElements.Const_scaleLen
        TextBox_DeformScale.Text = MDIMain.FElements.Defrom_scaleLen
        TextBox_RoundOff.Text = MDIMain.Round_offVal

    End Sub

#Region "View Control Check Box Check Events"
    Private Sub CheckBox_NodeNumb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_NodeNumb.CheckedChanged
        MDIMain.Is_DrawNodeNum = CheckBox_NodeNumb.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_NodeCoord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_NodeCoord.CheckedChanged
        MDIMain.Is_DrawNodeCoord = CheckBox_NodeCoord.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_ElemLen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_ElemLen.CheckedChanged
        MDIMain.Is_DrawElemLength = CheckBox_ElemLen.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_GridLines_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_GridLines.CheckedChanged
        MDIMain.Is_DrawGrid = CheckBox_GridLines.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_OrginPt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_OrginPt.CheckedChanged
        MDIMain.Is_DrawZeroPt = CheckBox_OrginPt.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_Load_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Load.CheckedChanged
        MDIMain.Is_DrawLoad = CheckBox_Load.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_Loadvalue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Loadvalue.CheckedChanged
        MDIMain.Is_DrawLoadValue = CheckBox_Loadvalue.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_Constraints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_Constraints.CheckedChanged
        MDIMain.Is_DrawConst = CheckBox_Constraints.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_ResultValues_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_ResultValues.CheckedChanged
        MDIMain.Is_DrawResultValues = CheckBox_ResultValues.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub CheckBox_UndeformedView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_UndeformedView.CheckedChanged
        MDIMain.Is_DrawUndeformedView = CheckBox_UndeformedView.Checked
        MDIMain.MT_Pic.Refresh()
    End Sub
#End Region

    Private Sub Button_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Ok.Click
        If Test_a_textboxvalue_validity(TextBox_ScValue.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_loadScale.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_ConstScale.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_DeformScale.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_RoundOff.Text, 1, 0) = True And _
           Val(TextBox_RoundOff.Text) < 16 Then
            MDIMain.threeD_Control._UpdateScale = 800 / Val(TextBox_ScValue.Text)
            MDIMain.FElements.Load_scaleLen = Val(TextBox_loadScale.Text)
            MDIMain.FElements.Const_scaleLen = Val(TextBox_ConstScale.Text)
            MDIMain.FElements.Defrom_scaleLen = Val(TextBox_DeformScale.Text)
            MDIMain.Round_offVal = Val(TextBox_RoundOff.Text)

            Me.Close()
        Else
            MsgBox("Invalid Scalve value !!", MsgBoxStyle.OkOnly, "Samson Mano")
        End If
    End Sub
End Class