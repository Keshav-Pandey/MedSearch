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
            <form id="form1" runat="server" onsubmit="performSearch">

                <div class="ui-field-contain">
                    <asp:TextBox id="searchEntry" name= "searchEntry"  ToolTip="Search for drugs or symptoms" type="search" runat="server" AutoCompleteType="Search" OnTextChanged="performSearch"></asp:TextBox>
                    <asp:Button id="searchButton" runat="server" for="searchEntry" class="ui-btn ui-btn-inline ui-icon-search ui-btn-icon-left" href="#" onclick="performSearch" Text="Search"></asp:Button>
                </div>
                <div class="ui-grid-a ui-responsive">
                    <div id="outputimg" class="ui-block-a"><div class="ui-bar ui-bar-a" style="height:auto;"><center><img src="<%=searchImage%>" style="width:auto;height:auto;" /></center></div></div>
                    <div id ="output" class="ui-block-b"><div class="ui-bar ui-bar-a" style="height:auto;"><p class="ui-shadow ui-corner-all" type="text"><%=searchResponse%></p></div></div>
                </div>
                <div class="ui-grid-a ui-responsive">
                    <div id="synm" class="ui-block-a"><div class="ui-bar ui-bar-a" style="height:auto;"><%=searchSynonyms%></div></div>
                    <div id ="maps" class="ui-block-b"><div class="ui-bar ui-bar-a" style="height:auto;"><center>Maps</center><div id="map_canvas"></div></div></div>
                </div>
            </form>
    	</div>

        <!-- Login panel , will see what to do with it later -->
        <div data-role="panel" data-position="right" data-position-fixed="true" data-display="overlay" data-theme="a" id="add-form">

        <form class="userform">

        	<h2>Login</h2>

            <label for="name">Username:</label>
            <input type="text" name="name" id="name" value="" data-clear-btn="true" data-mini="true">

            <label for="password">Password:</label>
            <input type="password" name="password" id="password" value="" data-clear-btn="true" autocomplete="off" data-mini="true">

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
        var sb = document.getElementById("searchButton");
        sb.addEventListener(onclick, searchMap, false);
        function searchMap()
        {
            alert("asd");
            var mapOptions = {
            center: myLatLng,
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP // There are atleast 4 types of maps
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"),mapOptions);
            xhr = new XMLHttpRequest();
            xhr.onreadystatechange = populate;
            xhr.open("GET","MedWebservice.asmx",true);
            xhr.send();
        }
        function populate()
        {
            if(xhr.readyState == 4 && xhr.status == 200)
            {
                var a = xhr.resposeText;
                var points = a.split(";");
                for(i =0;i<points.length;i++)
                {
                    latlngs = points.split(":")[1].split(",");
                    var myLatLng = new google.maps.LatLng(latlngs[0],latlngs[1]);
                    var marker = new google.maps.Marker({
                        position: myLatLng,
                        map: map,
                        title:points.split(":")[0]
                    });
                }
            }
        }
    </script>
</body>
</html>
