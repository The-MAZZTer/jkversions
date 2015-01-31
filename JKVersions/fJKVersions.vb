Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Security.Cryptography

Public Class fJKVersions
    Public Sub New()
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf _
            UnhandledException

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf _
            UnhandledException
    End Sub

    Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
        MyBase.OnShown(e)

        _worker = New Thread(AddressOf Work)
        _worker.IsBackground = True
        _worker.Priority = ThreadPriority.BelowNormal
        _worker.Start()
    End Sub
    Private _worker As Thread = Nothing

    Private Sub fJKVersions_FormClosing(ByVal sender As System.Object, ByVal e As  _
        System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Abort()
        End If
    End Sub

    Private Sub Abort()
        If InvokeRequired Then
            Invoke(New AbortDelegate(AddressOf Abort))
            Return
        End If

        bCancel.Enabled = False
        If _worker.IsAlive Then
            _worker.Abort()
            _worker.Join()
        End If
        UpdateProgress(-1, "Aborting...")

        Dim di As New DirectoryInfo(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions"))
        If di.Exists Then
            Try
                di.Delete(True)
            Catch
            End Try
        End If

        Application.Exit()
    End Sub
    Private Delegate Sub AbortDelegate()

    Private Sub bCancel_Click(ByVal sender As System.Object, ByVal e As System. _
        EventArgs) Handles bCancel.Click

        Abort()
    End Sub

    Private Sub UpdateProgress(ByVal pos As Double, ByVal text As String)
        If InvokeRequired Then
            Invoke(New UpdateProgressDelegate(AddressOf UpdateProgress), pos, text)
            Return
        End If

        If pos >= 0 Then
            pbProgress.Value = pos * 100000000
        End If
        If text IsNot Nothing Then
            lStatus.Text = text
        End If
    End Sub
    Private Delegate Sub UpdateProgressDelegate(ByVal pos As Double, ByVal text As _
        String)

    Private Sub Work()
        ExtractTools()
        UpdateProgress(1, "Downloading Jedi Knight 1.01 patch...")

        Dim di As New DirectoryInfo(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions"))
        If di.Exists Then
            Try
                di.Delete(True)
            Catch
            End Try
        End If
        If Not di.Exists Then
            di.Create()
        End If

        di = New DirectoryInfo(Path.Combine(di.FullName, "Temp"))
        If Not di.Exists Then
            di.Create()
        End If

        Dim fwr As HttpWebRequest = HttpWebRequest.Create( _
            "http://junk.mzzt.net/jkupd101.exe")
        fwr.KeepAlive = False
        'fwr.UseBinary = True
        'fwr.UsePassive = True
        'fwr.Method = WebRequestMethods.Ftp.DownloadFile
        fwr.Method = WebRequestMethods.Http.Get
        fwr.Timeout = 10000

        DownloadFile(fwr, Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\jkupd101.exe"), 1)

        fwr = Nothing

        UpdateProgress(2, "Extracting Jedi Knight 1.01 binary...")

        ExtractFiles(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\jkupd101.exe"), New String() {"jk.exe"})

        File.Move(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\jk.exe"), Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\jk.1.01.exe"))

        VerifyHash(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\jk.1.01.exe"), My.Resources.JK_1_0_1)

        UpdateProgress(3, "Downloading Jedi Knight 1.01 -> 1.00 patch...")

        Dim hwr As HttpWebRequest = HttpWebRequest.Create( _
            "http://www.jkhub.net/project/get.php?id=975")
        hwr.KeepAlive = False
        hwr.Method = WebRequestMethods.Http.Get
        hwr.Timeout = 10000

        Dim fvi As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application. _
            ExecutablePath)
        hwr.UserAgent = fvi.ProductName & "/" & fvi.ProductVersion

        DownloadFile(hwr, Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\patch_1.01_to_1.0.zip"), 3)

        hwr = Nothing

        UpdateProgress(4, "Extracting Jedi Knight 1.01 -> 1.00 patch...")

        ExtractFiles(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\patch_1.01_to_1.0.zip"), New String() _
            {"bspatch.exe", "patch_1.01_to_1.0.dat"})

        UpdateProgress(5, "Patching Jedi Knight 1.01 -> 1.00...")

        PatchFile("jk.1.01.exe", "jk.1.00.exe", "patch_1.01_to_1.0.dat")

        VerifyHash(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\jk.1.00.exe"), My.Resources.JK_1_0)

        UpdateProgress(6, "Downloading Jedi Knight Unofficial Patch 2008-01-16...")

        hwr = HttpWebRequest.Create("http://www.jkhub.net/project/get.php?id=1499")
        hwr.KeepAlive = False
        hwr.Method = WebRequestMethods.Http.Get
        hwr.Timeout = 10000
        hwr.UserAgent = fvi.ProductName & "/" & fvi.ProductVersion

        DownloadFile(hwr, Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\JKUnofficialPatch_2008-01-16.zip"), 6)

        hwr = Nothing

        UpdateProgress(7, "Extracting Jedi Knight Unofficial Patch...")

        ExtractFiles(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\JKUnofficialPatch_2008-01-16.zip"), New String() _
            {"bspatch.exe", "JK-Extension.dll", "patch.dat"})

        File.Move(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\JK-Extension.dll"), Environment. _
            ExpandEnvironmentVariables("%TEMP%\JKVersions\JK-Extension.dll"))

        UpdateProgress(8, "Patching Jedi Knight with Unofficial Patch...")

        PatchFile("jk.1.00.exe", "jk.Unofficial.Patch.2008.01.16.exe", "patch.dat")

        VerifyHash(Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\jk.Unofficial.Patch.2008.01.16.exe"), My.Resources. _
            JK_Unofficial_Patch_2008_01_16)

        If di.Exists Then
            Try
                di.Delete(True)
            Catch
            End Try
        End If

        Process.Start(Environment.ExpandEnvironmentVariables("%TEMP%\JKVersions"))

        Application.Exit()
    End Sub

    Private Sub ExtractTools()
        Dim di As New DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application. _
            ExecutablePath), "tools"))
        If Not di.Exists Then
            di.Create()
        End If

        Dim fi As New FileInfo(Path.Combine(di.FullName, "7za.exe"))
        If Not fi.Exists Then
            Dim fs As FileStream = Nothing
            Try
                fs = New FileStream(fi.FullName, FileMode.Create, FileAccess.Write)
                fs.Write(My.Resources._7za, 0, My.Resources._7za.Length)
            Catch ex As ThreadAbortException
                If fs IsNot Nothing Then
                    fs.Close()
                    fs.Dispose()
                End If

                Throw ex
            End Try
            fs.Close()
            fs.Dispose()
        End If

        fi = New FileInfo(Path.Combine(di.FullName, "7za_license.txt"))
        If Not fi.Exists Then
            Dim fs As FileStream = Nothing
            Try
                fs = New FileStream(fi.FullName, FileMode.Create, FileAccess.Write)
                fs.Write(My.Resources._7za_license, 0, My.Resources._7za_license.Length)
            Catch ex As ThreadAbortException
                If fs IsNot Nothing Then
                    fs.Close()
                    fs.Dispose()
                End If

                Throw ex
            End Try
            fs.Close()
            fs.Dispose()
        End If
    End Sub

    Private Sub DownloadFile(ByVal req As WebRequest, ByVal dest As String, ByVal pos _
        As Integer)

        Dim res As WebResponse = Nothing
        Dim s As Stream = Nothing
        Dim fs As FileStream = Nothing
        Try
            res = req.GetResponse
            s = res.GetResponseStream

            fs = New FileStream(dest, FileMode.Create, FileAccess.Write)
            Dim read As Integer = 1
            Dim buffer(32767) As Byte
            While read > 0
                read = s.Read(buffer, 0, buffer.Length)
                If read > 0 Then
                    fs.Write(buffer, 0, read)
                End If

                If res.ContentLength > -1 Then
                    UpdateProgress(pos + (CDbl(fs.Position) / res.ContentLength), _
                        Nothing)
                End If
            End While
        Catch ex As ThreadAbortException
            If res IsNot Nothing Then
                res.Close()
            End If

            If s IsNot Nothing Then
                s.Close()
                s.Dispose()
            End If

            If fs IsNot Nothing Then
                fs.Close()
                fs.Dispose()
            End If

            Throw ex
        End Try

        fs.Close()
        s.Close()
        res.Close()
    End Sub

    Private Sub ExtractFiles(ByVal archive As String, ByVal filenames As String())
        Dim psi As New ProcessStartInfo
        psi.FileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), _
            "tools\7za.exe")
        psi.WorkingDirectory = Path.GetDirectoryName(archive)
        psi.Arguments = "x """ & Path.GetFileName(archive) & """ "

        For Each filename As String In filenames
            psi.Arguments &= " """ & filename & """"
        Next

        psi.Arguments &= " -y"
        psi.CreateNoWindow = True
        psi.UseShellExecute = False
        Dim p As Process = Process.Start(psi)
        Try
            p.PriorityClass = ProcessPriorityClass.BelowNormal
        Catch
        End Try
        p.WaitForExit()

        Try
            File.Delete(archive)
        Catch
        End Try
    End Sub

    Private Sub PatchFile(ByVal input As String, ByVal output As String, ByVal patch _
        As String)

        Environment.SetEnvironmentVariable("__compat_layer", "RunAsInvoker") ' Stop Windows from thinking bspatch needs to be elevated (legacy thing).

        Dim psi As New ProcessStartInfo
        psi.FileName = Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions\Temp\bspatch.exe")
        psi.WorkingDirectory = Environment.ExpandEnvironmentVariables( _
            "%TEMP%\JKVersions")
        psi.Arguments = """" & input & """ """ & output & """ ""Temp\" & patch & """"
        psi.CreateNoWindow = True
        psi.UseShellExecute = False
        Dim p As Process = Process.Start(psi)
        Try
            p.PriorityClass = ProcessPriorityClass.BelowNormal
        Catch
        End Try
        p.WaitForExit()

        Try
            File.Delete(Environment.ExpandEnvironmentVariables( _
                "%TEMP%\JKVersions\Temp\" & patch))
        Catch
        End Try

        Try
            File.Delete(Environment.ExpandEnvironmentVariables( _
                "%TEMP%\JKVersions\Temp\bspatch.exe"))
        Catch
        End Try
    End Sub

    Private Sub VerifyHash(ByVal filename As String, ByVal hash As Byte())
        Dim sha1 As New SHA1Managed
        Dim fs As FileStream = Nothing
        Dim filehash As Byte() = Nothing
        Try
            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            filehash = sha1.ComputeHash(fs)
        Catch ex As ThreadAbortException
            If fs IsNot Nothing Then
                fs.Close()
                fs.Dispose()
            End If

            Throw ex
        End Try

        fs.Close()

        For i As Integer = 0 To filehash.Length - 1
            If filehash(i) <> hash(i) Then
                MsgBox("The file was not downloaded or patched correctly.  Aborting.", _
                    MsgBoxStyle.Critical, "Critical Error")

                Abort()
                Application.ExitThread()
            End If
        Next i
    End Sub

    Private Sub UnhandledException(ByVal sender As Object, ByVal e As  _
        UnhandledExceptionEventArgs)

        If Process.GetCurrentProcess.MainModule.FileName.ToLower.EndsWith( _
            ".vshost.exe") Then

            Return
        End If

        If Not TypeOf e.ExceptionObject Is Exception Then
            Return
        End If

        Dim ex As Exception = e.ExceptionObject

        Dim errorText As String = ex.Source & ": " & ex.GetType.FullName & ": " & ex. _
            Message & Environment.NewLine & ex.StackTrace

        While ex.InnerException IsNot Nothing
            ex = ex.InnerException

            errorText = ex.Source & ": " & ex.GetType.FullName & ": " & ex.Message & _
                Environment.NewLine & ex.StackTrace & Environment.NewLine & _
                Environment.NewLine & errorText
        End While

        Dim fs As New IO.StreamWriter("errordump.txt", False)
        fs.Write(errorText)
        fs.Close()

        MsgBox("An unexpected condition has been reached in this program and it is " & _
            "no longer able to continue running.  Please send the author the " & _
            "details from the file that will open so that he can fix it.", MsgBoxStyle. _
            Critical, "Fatal Error - JK Versions")

        Process.Start("errordump.txt")

        End
    End Sub
End Class
