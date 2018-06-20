Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace DocumentIteratorExample
    Partial Public Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
            ribbonControl1.SelectedPage = pageIterator
            richEditControl1.LoadDocument("TextWithDifferentFonts.docx")
        End Sub

        Private Sub btnIteratorRun_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIteratorRun.ItemClick
'            #Region "#runvisitor"
            Dim visitor As New MyVisitor()
            Dim [iterator] As New DocumentIterator(richEditControl1.Document, True)
            Do While [iterator].MoveNext()
                [iterator].Current.Accept(visitor)
            Loop
            listBoxControl1.DataSource = visitor.DocumentFonts
'            #End Region ' #runvisitor
        End Sub
    End Class
    #Region "#myvisitorclass"
    Public Class MyVisitor
        Inherits ParentVisitor

        Public ReadOnly Property DocumentFonts() As List(Of String)
            Get
                Return Fonts
            End Get
        End Property

        Public Overrides Sub Visit(ByVal text As DocumentText)
            If Not Fonts.Contains(text.TextProperties.FontName) Then
                Fonts.Add(text.TextProperties.FontName)
            End If
        End Sub
    End Class
    #End Region ' #myvisitorclass

    #Region "#parentvisitorclass"
    Public MustInherit Class ParentVisitor
        Inherits DocumentVisitorBase
        Private _Fonts As List(Of String)
        Protected Property Fonts() As List(Of String)
            Get
                Return _Fonts
            End Get
            Set(ByVal value As List(Of String))
                _Fonts = value
            End Set
        End Property
        Protected Sub New()
            _Fonts = New List(Of String)()
        End Sub
    End Class
    #End Region ' #parentvisitorclass
End Namespace
