<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MedSearch.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="try1()">
    <form id="form1" runat="server">
    <div>
    Hello <%=a1%>
    </div>
    </form>
    <script>
        function try1()
        {

            alert(<%=a1%>);
        }
    </script>
</body>
</html>
