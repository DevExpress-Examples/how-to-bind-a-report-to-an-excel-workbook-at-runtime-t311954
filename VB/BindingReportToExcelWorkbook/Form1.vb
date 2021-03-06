﻿Imports System
Imports System.Windows.Forms
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Configuration
' ...

Namespace BindingReportToExcelWorkbook
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            ' Create an empty report.
            Dim report As New XtraReport()

            ' Create a new Excel data source.
            Dim excelDataSource As New ExcelDataSource()
            excelDataSource.FileName = "..//..//Northwind.xlsx"

            ' Select a required worksheet.
            Dim excelWorksheetSettings As New ExcelWorksheetSettings()
            excelWorksheetSettings.WorksheetName = "Sheet_Categories"

            ' Specify import settings.
            Dim excelSourceOptions As New ExcelSourceOptions()
            excelSourceOptions.ImportSettings = excelWorksheetSettings
            excelSourceOptions.SkipHiddenRows = False
            excelSourceOptions.SkipHiddenColumns = False
            excelDataSource.SourceOptions = excelSourceOptions

            ' Create new fields and specify their settings.
            Dim fieldCategoryID As FieldInfo = New FieldInfo With { _
                .Name = "CategoryID", _
                .Type = GetType(Double), _
                .Selected = False _
            }
            Dim fieldCategoryName As FieldInfo = New FieldInfo With { _
                .Name = "CategoryName", _
                .Type = GetType(String) _
            }
            Dim fieldDescription As FieldInfo = New FieldInfo With { _
                .Name = "Description", _
                .Type = GetType(String) _
            }
            ' Add the created fields to the data source schema in the order that matches the column order in the source file.  
            excelDataSource.Schema.AddRange(New FieldInfo() { fieldCategoryID, fieldCategoryName, fieldDescription })

            ' Assign the data source to the report.
            report.DataSource = excelDataSource

            ' Add a detail band to the report.
            Dim detailBand As New DetailBand()
            detailBand.Height = 50
            report.Bands.Add(detailBand)

            ' Create a new label.
            Dim label As New XRLabel()
            ' Specify the label's binding depending on the data binding mode.
            If Settings.Default.UserDesignerOptions.DataBindingMode = DataBindingMode.Bindings Then
                label.DataBindings.Add("Text", Nothing, "CategoryName")
            Else
                label.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", "[CategoryName]"))
            End If
            ' Add the label to the detail band.
            detailBand.Controls.Add(label)


            ' Show the report's print preview.
            report.ShowPreview()
        End Sub
    End Class
End Namespace
