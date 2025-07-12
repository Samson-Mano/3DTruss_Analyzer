Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions
Imports _3DTruss_Analyzer.MatrixMath

Public Class ThreeD_Truss_Solver
    Dim Is_ModelValid As Boolean = False
    Dim Is_extrenalLoad_Present As Boolean = False

    Private Sub ThreeD_Truss_Solver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Check_Model_Validity()

    End Sub

    Public Sub Check_Model_Validity()
        Dim Str As String = ""
        RichTextBox_AnalysisUpdate.Multiline = True
        '--- Check the Validity of the model
        If MDIMain.FElements.tNodes.Count = 0 Then
            Str = Str & " No Nodes Found " & vbNewLine
        End If
        If MDIMain.FElements.tElements.Count = 0 Then
            Str = Str & " No Elements Found " & vbNewLine
        End If
        '-- Check whether model has constraints
        Dim is_constraint_present As Boolean = False
        Is_extrenalLoad_Present = False
        For Each Nd In MDIMain.FElements.tNodes
            If Nd.is_NodehasConstraint = True Then
                is_constraint_present = True
            End If
            If Nd.is_NodehasForce = True Then
                Is_extrenalLoad_Present = True
            End If
        Next
        If Is_extrenalLoad_Present = False Then '-- Check the include structure self weight check box because no external loads
            CheckBox_StrSelfWt.Checked = True
        End If

        If is_constraint_present = False Then
            Str = Str & " No Constraints Found "
        End If

        If Str <> "" Then '--- Model Input is not valid <Abort the solving>
            Label_S.Text = Str
            Label_S.ForeColor = Color.Red
            Button1.Text = "Ok"
            Is_ModelValid = False
        Else
            Label_S.ForeColor = Color.Black
            Label_S.Text = "Finite Element Solver" & vbNewLine & "developed by Samson Mano <saminnx@gmail.com>"
            Button1.Text = "Solve"
            Is_ModelValid = True
        End If
    End Sub

    Private Sub CheckBox_StrSelfWt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_StrSelfWt.CheckedChanged
        Is_ModelValid = Is_ModelValid And (Is_extrenalLoad_Present Or CheckBox_StrSelfWt.Checked)

        If Is_extrenalLoad_Present = False And CheckBox_StrSelfWt.Checked = False Then
            If Label_S.Text = "Finite Element Solver" & vbNewLine & "developed by Samson Mano <saminnx@gmail.com>" Then
                Label_S.Text = " No Valid Loads "
            Else
                Label_S.Text = Label_S.Text & vbNewLine & " No Valid Loads "
            End If
            Label_S.ForeColor = Color.Red
            Button1.Text = "Ok"
            'Is_ModelValid = False
        Else
            '--- In this case there is other incomplete model data (like no nodes elements or constraints) but has load in the type of structural self weight
            Check_Model_Validity()

        End If

        Label1.Enabled = CheckBox_StrSelfWt.Checked
        Label2.Enabled = CheckBox_StrSelfWt.Checked
        Label3.Enabled = CheckBox_StrSelfWt.Checked

        TextBox_Ax.Enabled = CheckBox_StrSelfWt.Checked
        TextBox_Ay.Enabled = CheckBox_StrSelfWt.Checked
        TextBox_Az.Enabled = CheckBox_StrSelfWt.Checked
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Is_ModelValid = False And Button1.Text = "Ok" Then
            '--- Model has No Constraint or No Element or No Nodes
            Me.Close()
        Else
            '----- Check the Accelereation Values
            Dim can_start_analysis As Boolean = True
            If Test_a_textboxvalue_validity(TextBox_Ax.Text, 0, 0) = False Or
               Test_a_textboxvalue_validity(TextBox_Ay.Text, 0, 0) = False Or
               Test_a_textboxvalue_validity(TextBox_Az.Text, 0, 0) = False Or
               (Test_a_textboxvalue_validity(TextBox_Ax.Text, 0, 1) Or
                Test_a_textboxvalue_validity(TextBox_Ay.Text, 0, 1) Or
                Test_a_textboxvalue_validity(TextBox_Az.Text, 0, 1)) = False Then

                can_start_analysis = False
                MsgBox("Couldn't include selfweight to the analysis !!! Acceleration values are not valid", MsgBoxStyle.OkOnly, "Samson Mano")
            End If


            '------ All Valid so Start Analysis
            If can_start_analysis = True Then
                'MsgBox("Analysis Running")
                Call Truss_solver()



            End If
        End If
    End Sub

    Public Sub Truss_solver()
        RichTextBox_AnalysisUpdate.Clear() 'Analysis User Update
        MDIMain.is_Analysis_Success = False

        '----- Delete all Nodes which are not associated with Elements <Standalone nodes clear>
        Delete_StandAlone_Nodes()


        '______________________________________ START OF ANALYSIS _______________________________________________
        '--- Step: 1 Find the Size of the Matrix 
        Dim Global_MSize As Integer = (Node_List_frm_Elements.Count * 3)  '-- Number of Nodes x 3 (for x,y,z translations)
        RichTextBox_AnalysisUpdate.AppendText("Analysis Started - Programmed by Samson Mano")

        '--- Step: 1A Fix the Size of all the Matrix
        Dim Force_Matrix As New MatrixMath.MatrixMath(Global_MSize, 1)
        Dim DOF_Matrix As New MatrixMath.MatrixMath(Global_MSize, 1)
        Dim Global_Stiff_Matrix As New MatrixMath.MatrixMath(Global_MSize, Global_MSize)

        For Each Elm In MDIMain.FElements.tElements
            '---- Find the appropriate Node IDs
            Dim Elm_SNode As Integer = Elm.SN.N_ID
            Dim Elm_ENode As Integer = Elm.EN.N_ID

            '---- Find the appropriate Nodes
            Dim temp_nodes_S As FE_Objects_Store.Nodes_Store = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = Elm_SNode)
            Dim temp_nodes_E As FE_Objects_Store.Nodes_Store = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = Elm_ENode)

            '--- Step: 2 Initialize all Matrix and Add Self weight if necessary
            Dim Self_wt_Matrix As New MatrixMath.MatrixMath(6, 1)


            '---- Transformation Matrix
            Dim Elm_Transformation_Matrix As New MatrixMath.MatrixMath(6, 2)
            Elm_Transformation_Matrix = TransFormation_Matrix(temp_nodes_S.N_Coord.X, temp_nodes_S.N_Coord.Y, temp_nodes_S.N_Coord.Z,
                                                              temp_nodes_E.N_Coord.X, temp_nodes_E.N_Coord.Y, temp_nodes_E.N_Coord.Z, Elm.ELength)

            If CheckBox_StrSelfWt.Checked = True Then
                '--- Step: 2A Force Matrix from self weight (Acceleration of gravity)
                'Mass Matrix Transformed to local coordinates
                Dim Mass_Matrix As New MatrixMath.MatrixMath(6, 6)

                Dim Mass_Const As New MatrixMath.MatrixMath(2, 2)
                Mass_Const.Item(0, 0) = Elm.Rho_Density * Elm.ECArea * Elm.ELength * 0.5
                Mass_Const.Item(1, 1) = Elm.Rho_Density * Elm.ECArea * Elm.ELength * 0.5

                Mass_Matrix = (Elm_Transformation_Matrix * Mass_Const) * Elm_Transformation_Matrix.Transpose '(6x2)*(2x2)*(2x6) = (6x6)

                'Acceleartion Matrix
                Dim Ax As Double = Val(TextBox_Ax.Text)
                Dim Ay As Double = Val(TextBox_Ay.Text)
                Dim Az As Double = Val(TextBox_Az.Text)

                Dim Accl_Matrix As New MatrixMath.MatrixMath(6, 1)
                Accl_Matrix.SetColumn(0, New Double() {Ax, Ay, Az, Ax, Ay, Az})

                Self_wt_Matrix = Mass_Matrix * Accl_Matrix '--- (6x6)*(6x1) = (6x1)
            End If

            Dim Str1 As String = ""
            Str1 = Self_wt_Matrix.Print_Matrix

            Dim Elm_force As New MatrixMath.MatrixMath(6, 1)
            Dim Elm_Dof As New MatrixMath.MatrixMath(6, 1)
            Dim Elm_Stiffeness_Matrix As New MatrixMath.MatrixMath(6, 6)

            '--- Step: 3 Force and DOF Matrix
            Dim Connection_CountS As Integer = MDIMain.FElements.tElements.FindAll(Function(T) ((T.SN.N_ID = Elm_SNode) Or T.EN.N_ID = Elm_SNode)).Count '-- Number of elements connected to the nodes
            Dim Connection_CountE As Integer = MDIMain.FElements.tElements.FindAll(Function(T) ((T.SN.N_ID = Elm_ENode) Or T.EN.N_ID = Elm_ENode)).Count
            Elm_force.SetColumn(0, New Double() {(temp_nodes_S.Node_Force.X_force / Connection_CountS) + Self_wt_Matrix.Item(0, 0),
                                                 (temp_nodes_S.Node_Force.Y_force / Connection_CountS) + Self_wt_Matrix.Item(1, 0),
                                                 (temp_nodes_S.Node_Force.Z_force / Connection_CountS) + Self_wt_Matrix.Item(2, 0),
                                                 (temp_nodes_E.Node_Force.X_force / Connection_CountE) + Self_wt_Matrix.Item(3, 0),
                                                 (temp_nodes_E.Node_Force.Y_force / Connection_CountE) + Self_wt_Matrix.Item(4, 0),
                                                 (temp_nodes_E.Node_Force.Z_force / Connection_CountE) + Self_wt_Matrix.Item(5, 0)})


            Elm_Dof.SetColumn(0, New Double() {-1 * CInt(temp_nodes_S.Node_Constraint.is_X_fixed),
                                               -1 * CInt(temp_nodes_S.Node_Constraint.is_Y_fixed),
                                               -1 * CInt(temp_nodes_S.Node_Constraint.is_Z_fixed),
                                               -1 * CInt(temp_nodes_E.Node_Constraint.is_X_fixed),
                                               -1 * CInt(temp_nodes_E.Node_Constraint.is_Y_fixed),
                                               -1 * CInt(temp_nodes_E.Node_Constraint.is_Z_fixed)})
            '--- Step: 4 Element Stiffeness Matrix
            Elm_Stiffeness_Matrix = Stiffness_Matrix(Elm.ECArea, Elm.EModulus, Elm.ELength, Elm_Transformation_Matrix)

            'Str1 = Elm_Transformation_Matrix.Print_Matrix
            'Str1 = Elm_Stiffeness_Matrix.Print_Matrix

            '--- Step: 4A Global Stiffeness & Force Matrices
            Dim m, n, p, q As Integer
            Dim NRT, NCT As Integer
            Dim NR, NC As Integer
            Dim i, j As Integer
            For m = 1 To 2 Step +1 '-- From Node 1 to Node 2
                NRT = If(m = 1, Elm_SNode, Elm_ENode) * 3
                For n = 0 To 2 Step +1 '-- From DOF X,Y,Z 
                    NR = n + NRT
                    i = n + ((m - 1) * 3)
                    For p = 1 To 2 Step +1 '-- From Node 1 to Node 2
                        NCT = If(p = 1, Elm_SNode, Elm_ENode) * 3
                        For q = 0 To 2 Step +1 '-- From DOF X,Y,Z 
                            NC = q + NCT
                            j = q + ((p - 1) * 3)

                            Global_Stiff_Matrix.Item(NR, NC) = Global_Stiff_Matrix.Item(NR, NC) + Elm_Stiffeness_Matrix.Item(i, j)
                        Next
                    Next
                    Force_Matrix.Item(NR, 0) = Force_Matrix.Item(NR, 0) + Elm_force.Item(i, 0)
                    DOF_Matrix.Item(NR, 0) = Elm_Dof.Item(i, 0)
                Next
            Next
        Next

        RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine)
        RichTextBox_AnalysisUpdate.AppendText("Global Degree of Freedom Matrix " & vbNewLine & DOF_Matrix.Print_Matrix & vbNewLine & vbNewLine)
        RichTextBox_AnalysisUpdate.AppendText("Global Force Matrix " & vbNewLine & Force_Matrix.Print_Matrix & vbNewLine & vbNewLine)
        RichTextBox_AnalysisUpdate.AppendText("Global Stiffness Matrix" & vbNewLine & Global_Stiff_Matrix.Print_Matrix & vbNewLine & vbNewLine)

        '--- Step: 5 Curtail the Force and Stiffeness Matrix
        Dim Curtl_MSize As Integer = DOF_Matrix.GetColumn(0).ToList.FindAll(Function(T) T = 0).Count '-- Find the number of free degree of freedoms
        Dim Curtl_Force_Matrix As New MatrixMath.MatrixMath(Curtl_MSize, 1)
        Dim Curtl_Global_Stiff_Matrix As New MatrixMath.MatrixMath(Curtl_MSize, Curtl_MSize)
        Dim r, s As Integer
        Dim Dof1, Dof2 As Integer
        r = 0
        For Dof1 = 0 To (Global_MSize - 1) Step +1
            If DOF_Matrix.Item(Dof1, 0) <> 0 Then '-- If the nodes are not free continue the loop
                Continue For
            Else
                s = 0
                For Dof2 = 0 To (Global_MSize - 1) Step +1
                    If DOF_Matrix.Item(Dof2, 0) <> 0 Then '-- If the nodes are not free continue the loop
                        Continue For
                    Else
                        Curtl_Force_Matrix.Item(s, 0) = Force_Matrix.Item(Dof2, 0)
                        Curtl_Global_Stiff_Matrix.Item(r, s) = Global_Stiff_Matrix.Item(Dof1, Dof2)
                        s = s + 1
                    End If
                Next
                r = r + 1
            End If
        Next

        RichTextBox_AnalysisUpdate.AppendText("Curtailed Force Matrix " & vbNewLine & Curtl_Force_Matrix.Print_Matrix & vbNewLine & vbNewLine)
        RichTextBox_AnalysisUpdate.AppendText("Curtailed Stiffness Matrix" & vbNewLine & Curtl_Global_Stiff_Matrix.Print_Matrix & vbNewLine & vbNewLine)

        '---- Step: 6 Solve for Displacement 
        Dim Curtl_Displacement_Matrix As New MatrixMath.MatrixMath(Curtl_MSize, 1)
        Gauss_Solver(Curtl_Global_Stiff_Matrix, Curtl_Force_Matrix, Curtl_Displacement_Matrix, Curtl_MSize - 1)

        '############# Check whether the results are valid ############# 
        '---- Step: 6A Check for Rigid body motion
        Dim is_Nan As Boolean = False
        For i = 0 To Curtl_MSize - 1 Step +1
            is_Nan = IsVal_NAN_or_Infinity(Curtl_Displacement_Matrix.Item(i, 0))
            If is_Nan = True Then Exit For
        Next

        If is_Nan = True Then
            '---- Rigid Body Motion Detected
            RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine & "Rigid Body Motion Detected")
            RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine & "Analysis Failed !!!")
            RichTextBox_AnalysisUpdate.ScrollToCaret()

            MDIMain.is_Analysis_Success = False
            MDIMain.MT_Pic.Refresh()
        Else
            RichTextBox_AnalysisUpdate.AppendText("**Solved** Curtailed Displacement Matrix" & vbNewLine & Curtl_Displacement_Matrix.Print_Matrix & vbNewLine & vbNewLine)

            '---- Step: 7 Welding of Result Matrix
            Dim Displacement_Matrix As New MatrixMath.MatrixMath(Global_MSize, 1)
            s = 0
            For Dof1 = 0 To (DOF_Matrix.GetColumn(0).ToList.Count - 1) Step +1
                If DOF_Matrix.Item(Dof1, 0) <> 0 Then
                    Continue For
                Else
                    Displacement_Matrix.Item(Dof1, 0) = Curtl_Displacement_Matrix.Item(s, 0)
                    s = s + 1
                End If
            Next
            RichTextBox_AnalysisUpdate.AppendText("Global Displacement Matrix" & vbNewLine & Displacement_Matrix.Print_Matrix & vbNewLine & vbNewLine)

            '---- Step: 8 Reaction Matrix
            Dim Recation_Matrix As New MatrixMath.MatrixMath(Global_MSize, 1)
            Recation_Matrix = (Global_Stiff_Matrix * Displacement_Matrix) - Force_Matrix

            RichTextBox_AnalysisUpdate.AppendText("Global Reaction Matrix" & vbNewLine & Recation_Matrix.Print_Matrix & vbNewLine & vbNewLine)

            Dim XResultatnt_Force As Double = 0
            Dim YResultatnt_Force As Double = 0
            Dim ZResultatnt_Force As Double = 0
            '--- Calculate the resultant forces
            For s = 0 To ((Global_MSize / 3) - 1) Step +1
                XResultatnt_Force = XResultatnt_Force + Recation_Matrix.Item((s * 3) + 0, 0) + Force_Matrix.Item((s * 3) + 0, 0)
                YResultatnt_Force = YResultatnt_Force + Recation_Matrix.Item((s * 3) + 1, 0) + Force_Matrix.Item((s * 3) + 1, 0)
                ZResultatnt_Force = ZResultatnt_Force + Recation_Matrix.Item((s * 3) + 2, 0) + Force_Matrix.Item((s * 3) + 2, 0)
            Next


            RichTextBox_AnalysisUpdate.AppendText("Sum of X Resultant Force = " & XResultatnt_Force.ToString("N8") & vbNewLine)
            RichTextBox_AnalysisUpdate.AppendText("Sum of Y Resultant Force = " & YResultatnt_Force.ToString("N8") & vbNewLine)
            RichTextBox_AnalysisUpdate.AppendText("Sum of Z Resultant Force = " & ZResultatnt_Force.ToString("N8") & vbNewLine)


            RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine & "Analysis Successfull !!!")
            RichTextBox_AnalysisUpdate.ScrollToCaret()
            '______________________________________ END OF ANALYSIS _________________________________________________

            MDIMain.FElements.Scale_MaxDisplacement = Math.Max(Displacement_Matrix.GetColumn(0).Max, Math.Abs(Displacement_Matrix.GetColumn(0).Min))
            '--- Maximum Resultant Displacement of each Nodes
            Dim Rsultant_Disp As Double = 0
            For s = 0 To (MDIMain.FElements.tNodes.Count - 1) Step +1
                If Rsultant_Disp < Math.Sqrt((Displacement_Matrix.Item((s * 3) + 0, 0) ^ 2) +
                                             (Displacement_Matrix.Item((s * 3) + 1, 0) ^ 2) +
                                             (Displacement_Matrix.Item((s * 3) + 2, 0) ^ 2)) Then
                    Rsultant_Disp = Math.Sqrt((Displacement_Matrix.Item((s * 3) + 0, 0) ^ 2) +
                                             (Displacement_Matrix.Item((s * 3) + 1, 0) ^ 2) +
                                             (Displacement_Matrix.Item((s * 3) + 2, 0) ^ 2))
                End If
            Next
            If Rsultant_Disp = 0 Then '--- Another check for Error
                '---- Rigid Body Motion Detected
                RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine & "Unknown error all Displacement are Zero -> Please save the model file and send to <saminnx@gmail.com>")
                RichTextBox_AnalysisUpdate.AppendText(vbNewLine & vbNewLine & "Analysis Failed !!!")
                RichTextBox_AnalysisUpdate.ScrollToCaret()

                MDIMain.is_Analysis_Success = False
                MDIMain.MT_Pic.Refresh()
                Exit Sub
            End If

            '----- Set Displacement to Nodes
            For s = 0 To (MDIMain.FElements.tNodes.Count - 1) Step +1
                MDIMain.FElements.tNodes(s).Update_My_Displacements(Displacement_Matrix.Item((s * 3) + 0, 0),
                                                                    Displacement_Matrix.Item((s * 3) + 1, 0),
                                                                    Displacement_Matrix.Item((s * 3) + 2, 0), Rsultant_Disp,
                                                                    Recation_Matrix.Item((s * 3) + 0, 0),
                                                                    Recation_Matrix.Item((s * 3) + 1, 0),
                                                                    Recation_Matrix.Item((s * 3) + 2, 0))

            Next

            '---- Set Member Datas
            Dim All_axial_Force_lst As New List(Of Double)
            Dim All_axial_Strain_lst As New List(Of Double)
            Dim All_axial_Stress_lst As New List(Of Double)

            For Each tElm In MDIMain.FElements.tElements
                '---- Find the appropriate Nodes
                Dim Elm_SNode As Integer = tElm.SN.N_ID
                Dim Elm_ENode As Integer = tElm.EN.N_ID

                Dim temp_nodes_S As FE_Objects_Store.Nodes_Store = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = Elm_SNode)
                Dim temp_nodes_E As FE_Objects_Store.Nodes_Store = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = Elm_ENode)

                Dim Disp_Matrix As New MatrixMath.MatrixMath(1, 6)
                Disp_Matrix.SetRow(0, New Double() {temp_nodes_S.Node_Displacement.X_Disp, temp_nodes_S.Node_Displacement.Y_Disp, temp_nodes_S.Node_Displacement.Z_Disp,
                                                    temp_nodes_E.Node_Displacement.X_Disp, temp_nodes_E.Node_Displacement.Y_Disp, temp_nodes_E.Node_Displacement.Z_Disp})

                '---- Transformation Matrix
                Dim Elm_Transformation_Matrix As New MatrixMath.MatrixMath(6, 2)
                Elm_Transformation_Matrix = TransFormation_Matrix(temp_nodes_S.N_Coord.X, temp_nodes_S.N_Coord.Y, temp_nodes_S.N_Coord.Z,
                                                                  temp_nodes_E.N_Coord.X, temp_nodes_E.N_Coord.Y, temp_nodes_E.N_Coord.Z, tElm.ELength)

                Dim Local_Disp As New MatrixMath.MatrixMath(1, 2)
                Local_Disp = Disp_Matrix * Elm_Transformation_Matrix

                Dim LMember_Force, LMember_Stress, LMember_Strain As Double

                LMember_Strain = (Local_Disp.Item(0, 1) - Local_Disp.Item(0, 0)) / tElm.ELength '--- Strain = Change in Length / Original Length
                LMember_Stress = tElm.EModulus * LMember_Strain '--- Stress = young's modulus * Strain
                LMember_Force = LMember_Stress * tElm.ECArea '--- Member Force = stress * CSArea

                All_axial_Force_lst.Add(LMember_Force)
                All_axial_Stress_lst.Add(LMember_Stress)
                All_axial_Strain_lst.Add(LMember_Strain)
            Next


            For s = 0 To (MDIMain.FElements.tElements.Count - 1) Step +1
                '--- Fix the color ratio here and pass it the elements
                Dim Aforce_ColorRatio As Double
                Aforce_ColorRatio = (All_axial_Force_lst(s) - All_axial_Force_lst.Min) / (All_axial_Force_lst.Max - All_axial_Force_lst.Min)

                Dim Astress_ColorRatio As Double
                Astress_ColorRatio = (All_axial_Stress_lst(s) - All_axial_Stress_lst.Min) / (All_axial_Stress_lst.Max - All_axial_Stress_lst.Min)

                Dim Astrain_ColorRatio As Double
                Astrain_ColorRatio = (All_axial_Strain_lst(s) - All_axial_Strain_lst.Min) / (All_axial_Strain_lst.Max - All_axial_Strain_lst.Min)

                MDIMain.FElements.tElements(s).Update_Member_results(All_axial_Force_lst(s), Aforce_ColorRatio,
                                                                     All_axial_Stress_lst(s), Astress_ColorRatio,
                                                                     All_axial_Strain_lst(s), Astrain_ColorRatio)
            Next


            '########################### IMPORTANT !!!!! Remember the error because scale_Maxload is Zero
            '---- Re Configure the maximum load
            MDIMain.FElements.Scale_MaxLoad = 0 '--- start by keeping the max load as zero
            For Each Nd In MDIMain.FElements.tNodes
                Dim This_Nodal_Forces As New List(Of Double) '--- Keep a list of all the forces present in the node
                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Force.X_force)) '-- X Force
                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Force.Y_force)) '-- Y Force
                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Force.Z_force)) '-- Z Force

                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Reactions.X_force)) '-- X Reaction
                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Reactions.Y_force)) '-- Y Reaction
                This_Nodal_Forces.Add(Math.Abs(Nd.Node_Reactions.Z_force)) '-- Z Reaction

                Dim Max_of_ForceXYZ As Double = This_Nodal_Forces.Max
                If Max_of_ForceXYZ > MDIMain.FElements.Scale_MaxLoad Then
                    MDIMain.FElements.Scale_MaxLoad = Max_of_ForceXYZ
                End If
            Next
            '########################### IMPORTANT !!!!! Remember the error because scale_Maxload is Zero


            MDIMain.is_Analysis_Success = True
            MDIMain.ResultView_Initialize(True)

        End If

    End Sub

    Public Sub Delete_StandAlone_Nodes()
        Dim StandAlone_nd_list As New List(Of Integer)
        Dim is_found As Boolean = False
        For Each Nd In MDIMain.FElements.tNodes
            Dim temp_Nd As FE_Objects_Store.Nodes_Store = Nd
            is_found = False
            is_found = MDIMain.FElements.tElements.Exists(Function(T) (T.SN = temp_Nd) Or (T.EN = temp_Nd))
            If is_found = False Then
                StandAlone_nd_list.Add(MDIMain.FElements.tNodes.IndexOf(Nd))
            End If
        Next

        StandAlone_nd_list.Reverse()
        For Each StandAlone_nd In StandAlone_nd_list
            MDIMain.FElements.tNodes.RemoveAt(StandAlone_nd)
        Next

        '---- Edit all Node IDs to form 0 to N
        For i = 0 To (MDIMain.FElements.tNodes.Count - 1) Step +1
            MDIMain.FElements.tNodes(i).Update_Node_IDs_for_Analysis(i)
        Next


    End Sub

    Public Function IsVal_NAN_or_Infinity(ByVal chkVal As Double) As Boolean
        Return Double.IsNaN(chkVal) Or Double.IsInfinity(chkVal)
    End Function


    '------- 1 Size of Bounds
    Private Function Node_List_frm_Elements() As List(Of Integer)
        Dim Uniq_NdID As New List(Of Integer)
        For Each Elments In MDIMain.FElements.tElements
            If Uniq_NdID.Contains(Elments.SN.N_ID) = False Then
                Uniq_NdID.Add(Elments.SN.N_ID)
            End If
            If Uniq_NdID.Contains(Elments.EN.N_ID) = False Then
                Uniq_NdID.Add(Elments.EN.N_ID)
            End If
        Next
        Return Uniq_NdID
    End Function

    '------- 2 Element Transformation Matrix
    Private Function TransFormation_Matrix(ByVal x1 As Double,
                                           ByVal y1 As Double,
                                           ByVal z1 As Double,
                                           ByVal x2 As Double,
                                           ByVal y2 As Double,
                                           ByVal z2 As Double,
                                           ByVal ELen As Double) As MatrixMath.MatrixMath
        Dim Trans_M As New MatrixMath.MatrixMath(6, 2)
        Dim Cx As Double
        Dim Cy As Double
        Dim Cz As Double

        Cx = (x2 - x1) / ELen
        Cy = (y2 - y1) / ELen
        Cz = (z2 - z1) / ELen

        Dim col1 As New List(Of Double)
        col1.Add(Cx)
        col1.Add(Cy)
        col1.Add(Cz)

        col1.Add(0)
        col1.Add(0)
        col1.Add(0)

        Trans_M.SetColumn(0, col1.ToArray)

        Dim Col2 As New List(Of Double)
        Col2.Add(0)
        Col2.Add(0)
        Col2.Add(0)

        Col2.Add(Cx)
        Col2.Add(Cy)
        Col2.Add(Cz)

        Trans_M.SetColumn(1, Col2.ToArray)

        Return Trans_M
    End Function

    '------- 3 Element Stiffness Matrix
    Private Function Stiffness_Matrix(ByVal t_A As Double, ByVal t_E As Double, ByVal t_ELen As Double,
                                      ByVal Trans_Matrix As MatrixMath.MatrixMath) As MatrixMath.MatrixMath

        Dim Transpose_Trans_Matrix As New MatrixMath.MatrixMath
        Transpose_Trans_Matrix = Trans_Matrix.Transpose '--- 2 x 6 Matrix

        Dim E_Matrix As New MatrixMath.MatrixMath(2, 2)
        E_Matrix.Item(0, 0) = 1 * ((t_A * t_E) / t_ELen)
        E_Matrix.Item(0, 1) = -1 * ((t_A * t_E) / t_ELen)
        E_Matrix.Item(1, 0) = -1 * ((t_A * t_E) / t_ELen)
        E_Matrix.Item(1, 1) = 1 * ((t_A * t_E) / t_ELen)

        Dim E_Stiff As New MatrixMath.MatrixMath(6, 6)
        E_Stiff = (Trans_Matrix * E_Matrix) * Transpose_Trans_Matrix

        Return E_Stiff
    End Function

    '------- 4 Gauss Elimination Solver
#Region "Gauss Elimination Method"
    '-----Redefined Gauss Elimination Procedure
    Private Sub Gauss_Solver(ByVal A As MatrixMath.MatrixMath, ByVal B As MatrixMath.MatrixMath, ByRef X As MatrixMath.MatrixMath, ByVal Bound As Integer)
        Dim Triangular_A(Bound, Bound + 1) As Double
        Dim soln(Bound) As Double 'Solution matrix
        For m = 0 To Bound
            For n = 0 To Bound
                Triangular_A(m, n) = A.Item(m, n)
            Next
        Next
        '.... substituting the force to triangularmatrics....
        For n = 0 To Bound
            Triangular_A(n, Bound + 1) = B.Item(n, 0)
        Next
        ForwardSubstitution(Triangular_A, Bound)
        ReverseElimination(Triangular_A, soln, Bound)
        '---- Adding to X Matrix
        X = New MatrixMath.MatrixMath((Bound + 1), 1)
        X.SetColumn(0, soln)
    End Sub

    Private Sub ForwardSubstitution(ByRef _triang(,) As Double, ByVal bound As Integer)
        'Forward Elimination
        'Dim _fraction As Single
        For k = 0 To bound - 1
            For i = k + 1 To bound
                If _triang(k, k) = 0 Then
                    Continue For
                End If
                _triang(i, k) = _triang(i, k) / _triang(k, k)
                For j = k + 1 To bound + 1
                    _triang(i, j) = _triang(i, j) - (_triang(i, k) * _triang(k, j))
                Next
            Next
        Next
    End Sub

    Private Sub ReverseElimination(ByRef _triang(,) As Double, ByRef X() As Double, ByVal bound As Integer)
        'Back Substitution
        For i = 0 To bound
            X(i) = _triang(i, bound + 1)
        Next

        For i = bound To 0 Step -1
            For j = i + 1 To bound
                X(i) = X(i) - (_triang(i, j) * X(j))
            Next
            X(i) = X(i) / _triang(i, i)
        Next
    End Sub
#End Region

    '------- 4 Determinant of a Matrix
    Public Function Matrix_Determinant(ByVal A(,) As Double, ByVal M_Size As Integer) As Double
        '----- Check Square Matrix
        Dim Det As Double = 0 '--- Return Variable

        If M_Size = 0 Then
            Det = A(0, 0) '1x1 matrix **Just in Case**
        ElseIf M_Size = 1 Then
            Det = (A(0, 0) * A(1, 1)) - (A(1, 0) * A(0, 1)) '2x2 matrix ** Note in each recursion the submatrix is aimed to reduce it to 2x2 level
        Else
            ' for nxn matrix -- Routine to create a sub matrix
            Det = 0
            Dim i, j, j1, j2 As Integer
            For j1 = 0 To M_Size Step +1
                Dim Sub_Matrix(M_Size - 1, M_Size - 1) As Double
                For i = 1 To M_Size Step +1
                    j2 = 0
                    For j = 0 To M_Size Step +1
                        If j = j1 Then Continue For '-- This line make sure that the rows and columns of index j1 is not included in sub-matrix
                        Sub_Matrix(i - 1, j2) = A(i, j)
                        j2 = j2 + 1
                    Next
                Next
                Det = Det + Math.Pow(-1, (1 + j1 + 1)) * A(0, j1) * Matrix_Determinant(Sub_Matrix, M_Size - 1)
            Next
        End If
        Return Det
    End Function

End Class