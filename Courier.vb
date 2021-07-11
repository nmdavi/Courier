Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class Courier
    Private Property Courier As New HttpClient()

    Public Sub New()
        Courier.Timeout = TimeSpan.FromSeconds(30)
        Courier.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))
    End Sub

    Private Sub AddToken(ByVal token As String)
        Courier.DefaultRequestHeaders.Add("authorization", "Bearer " & token)
    End Sub

    Public Sub AddHeader(ByVal name As String, ByVal value As String)
        Courier.DefaultRequestHeaders.Add(name, value)
    End Sub

    Public Sub RemoveHeader(ByVal name As String)
        Courier.DefaultRequestHeaders.Remove(name)
    End Sub

    Private Function GetJson(ByVal url As String) As String
        Dim sender As Task(Of HttpResponseMessage) = Courier.GetAsync(url)
        If sender.Result.StatusCode <> HttpStatusCode.Created And sender.Result.StatusCode <> HttpStatusCode.OK Then
            Throw New HttpRequestException(sender.Result.StatusCode)
        End If
        sender.Wait()
        Return sender.Result.Content.ReadAsStringAsync().Result
    End Function

    Private Function PostJson(ByVal url As String, ByVal letter As Object) As String
        Dim sender As Task(Of HttpResponseMessage) = Courier.PostAsync(url, New StringContent(JsonConvert.SerializeObject(letter), Encoding.UTF8, "application/json"))
        If sender.Result.StatusCode <> HttpStatusCode.Created And sender.Result.StatusCode <> HttpStatusCode.OK And HttpStatusCode.OK Then
            Throw New HttpRequestException(sender.Result.StatusCode)
        End If
        sender.Wait()
        Return sender.Result.Content.ReadAsStringAsync().Result
    End Function

    Private Function PutJson(ByVal url As String, ByVal letter As Object) As String
        Dim sender As Task(Of HttpResponseMessage) = Courier.PutAsync(url, New StringContent(JsonConvert.SerializeObject(letter), Encoding.UTF8, "application/json"))
        If sender.Result.StatusCode <> HttpStatusCode.Created And sender.Result.StatusCode <> HttpStatusCode.OK Then
            Throw New HttpRequestException(sender.Result.StatusCode)
        End If
        sender.Wait()
        Return sender.Result.Content.ReadAsStringAsync().Result
    End Function
End Class
