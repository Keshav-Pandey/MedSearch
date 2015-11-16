<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MedSearch.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>MedSearch</title>
    <link rel="Stylesheet" href="Content/jquery.mobile-1.4.5.min.css" />
    <script src="Scripts/jquery-1.8.0.min.js"></script>
    <script src="Scripts/jquery.mobile-1.4.5.min.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js"></script>
</head>
<body>
    <div data-role="page" id="pageone">

        <div data-role="header">
            <%--<a class="ui-btn-left ui-btn ui-btn-inline ui-mini ui-corner-all ui-btn-icon-left ui-icon-delete" href="#">Cancel</a>--%>
            <h1>MedSearch</h1>
            <a href="#nav-panel" data-icon="bars" data-iconpos="notext">Menu</a>
            <a href="#add-form" data-icon="gear" data-iconpos="notext" class="ui-icon-left">Add</a>
        </div>

	    <div role="main" class="ui-content">
            <form id="form1" runat="server">

                <div class="ui-field-contain">
                    <asp:TextBox ID="searchEntry" name= "searchEntry"  ToolTip="Search for drugs or symptoms" type="search" runat="server" AutoCompleteType="Search"></asp:TextBox>
                    <asp:Button ID="searchButton" runat="server" for="searchEntry" class="ui-btn ui-btn-inline ui-icon-search ui-btn-icon-left" href="#" onclick="performSearch" Text="Search" ></asp:Button>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="searchButton" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                <div class="ui-grid-a ui-responsive">
                    <div id="outputimg" class="ui-block-a"><div class="ui-bar ui-bar-a" style="height:auto;"><center><asp:Image ID="mimg" runat="server"  ImageURL="Content/images/Medicine.jpg" style="width:auto;height:auto;" /></center></div></div>
                    <div id ="output" class="ui-block-b"><div class="ui-bar ui-bar-a" style="height:auto;"><asp:Label id="srp" runat="server" class="ui-shadow ui-corner-all" type="text">MedSearch provides search which takes advantage of linked datasets.</asp:Label></div></div>
                </div>
                <div class="ui-grid-solo ui-responsive">
                    <div id="synm" class="ui-block-a"><asp:Label id="relsr" runat="server" class="ui-bar ui-bar-a" style="height:auto;"><center>Related Terms</center><br /><center>Here you can find related terms to the query that you enter.</center></asp:Label></div>
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="ui-grid-solo ui-responsive">
                    <div id ="maps" class="ui-block-a"><div class="ui-bar ui-bar-a" style="height:auto;"><center>Maps</center><div id="map_canvas" style="width:100%;height:300px;border:solid 1px blue;"></div></div></div>
                </div>
            </form>
    	</div>
        <!-- Login panel , will see what to do with it later -->
        <div data-role="panel" data-position="right" data-position-fixed="true" data-display="overlay" data-theme="a" id="add-form">

        <form class="userform">

        	<h2>Login</h2>

            <label for="name">Username:</label>
            <input type="text" name="name" id="name" value="" data-clear-btn="true" data-mini="true" />

            <label for="password">Password:</label>
            <input type="password" name="password" id="password" value="" data-clear-btn="true" autocomplete="off" data-mini="true" />

            <div class="ui-grid-a">
                <div class="ui-block-a"><a href="#" data-rel="close" class="ui-btn ui-shadow ui-corner-all ui-btn-b ui-mini">Cancel</a></div>
                <div class="ui-block-b"><a href="#" data-rel="close" class="ui-btn ui-shadow ui-corner-all ui-btn-a ui-mini">Save</a></div>
			</div>
        </form>

	</div><!-- /panel -->
        <!-- Option panel , need to discuss on what all to keep -->
        <div data-role="panel" data-position-fixed="true" data-display="push" data-theme="b" id="nav-panel">

		<ul data-role="listview">
            <li data-icon="delete"><a href="#" data-rel="close">Close menu</a></li>
                <li><a href="#panel-fixed-page2">About</a></li>
                <li><a href="#panel-fixed-page2">How to ?</a></li>
                <li><a href="#panel-fixed-page2">Maps</a></li>
                <li><a href="#panel-fixed-page2">PESIT</a></li>
                <li><a href="#panel-fixed-page2">Help</a></li>
		</ul>

	</div><!-- /panel -->
    	<div data-role="footer">
		    <h4>Special Topic - CSE</h4>
	    </div>

    
        </div>
    <script type="text/javascript">
        window.onload = initmap;
        var sb = document.getElementById("searchButton");
        var map;
        var myLatLng = new google.maps.LatLng(12.9342678, 77.53432629999998);
        var mapOptions;
        var marker;
        function initmap()
        {
            myLatLng = new google.maps.LatLng(12.9342678, 77.53432629999998);
            mapOptions = {
                center: myLatLng,
                zoom: 3,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        }
        function populate(response)
        {
            if(response.length>1)
            {
                var points = response.split(";");
                for (i = 0; i < points.length - 1; i++)
                {
                    var latlngs = points[i].split(":")[1].split(",");
                    var myLatLng = new google.maps.LatLng(parseFloat(latlngs[0]), parseFloat(latlngs[1]));
                    marker = new google.maps.Marker({
                        position: myLatLng,
                        map: map,
                        title: points[i].split(":")[0]
                    });
                }
            }
        }
    </script>
</body>
</html>
