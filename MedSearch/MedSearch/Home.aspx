<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MedSearch.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>MedSearch</title>
    <link rel="Stylesheet" href="Content/jquery.mobile-1.4.5.min.css" />
    <script src="Scripts/jquery-1.8.0.min.js"></script>
    <script src="Scripts/jquery.mobile-1.4.5.min.js"></script>
</head>
<body>
    <div data-role="page" id="pageone">

        <div data-role="header">
            <%--<a class="ui-btn-left ui-btn ui-btn-inline ui-mini ui-corner-all ui-btn-icon-left ui-icon-delete" href="#">Cancel</a>--%>
            <h1>MedSearch</h1>
            <button class="ui-btn-right ui-btn ui-btn-b ui-btn-inline ui-mini ui-corner-all ui-btn-icon-right ui-icon-check">Login</button>
        </div>

	    <div role="main" class="ui-content">
            <form id="form1" runat="server" onsubmit="performSearch">

                <div class="ui-field-contain">
                    <asp:Button runat="server" for="searchEntry" class="ui-btn ui-btn-inline ui-icon-search ui-btn-icon-left" href="#" onclick="performSearch" Text="Search"></asp:Button>
                    <asp:TextBox id="searchEntry" name= "searchEntry" type="search" runat="server" AutoCompleteType="Search" OnTextChanged="performSearch"></asp:TextBox>
                </div>
                <div class="ui-grid-a ui-responsive">
                    <div id="outputimg" class="ui-block-a"><a class="ui-btn ui-shadow ui-corner-all" href="#"><img src="<%=searchImage%>" style="width:auto;height:auto;"/></a></div>
                    <div id ="output" class="ui-block-b"><p class=" ui-shadow ui-corner-all" href="#" type="text"><%=searchResponse%></p></div>
                </div>
            </form>
    	</div>
    
    	<div data-role="footer">
		    <h4>Special Topic - CSE</h4>
	    </div>

    
        </div>
</body>
</html>
