<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityComparison.aspx.cs" Inherits="Hwk5.CityComparison" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            font-size: medium;
        }
        .auto-style2 {
            text-align: center;
            font-size: large;
        }
    </style>
</head>
<body style="height: 706px">
    <form id="form1" runat="server">
        <p class="auto-style2">
            <strong>City Comparison</strong></p>
        <p class="auto-style1">
            Get all the data to plan your next move</p>
        <p>
            <asp:Button ID="MostRecentButton" runat="server" OnClick="MostRecentButton_Click" Text="Most Recent Search" />
        </p>
        <p>
            <strong>Enter Location 1:
            <asp:Label ID="ErrorLabel1" runat="server"></asp:Label>
            <asp:Label ID="ErrorHome" runat="server" style="z-index: 1; left: 375px; top: 440px; position: absolute; width: 295px"></asp:Label>
            </strong></p>
        City and State (ex: Tempe, AZ)<p>
            City: <asp:TextBox ID="CityInput1" runat="server"></asp:TextBox>
        </p>
        <p>
            State:
            <asp:TextBox ID="StateInput1" runat="server"></asp:TextBox>
            <asp:Chart ID="Chart1" runat="server" width="350" style="z-index: 1; left: 373px; top: 125px; position: absolute">
                <series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title ="Location"></AxisX>
                        <AxisY Title ="Average Home Value"></AxisY>
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
        </p>
        <p>
            or by Zip Code (ex: 85257)
            <asp:Label ID="ErrorCrime" runat="server" style="z-index: 1; left: 874px; top: 275px; position: absolute; width: 280px"></asp:Label>
            <asp:Chart ID="Chart2" runat="server" width="350" style="z-index: 1; left: 852px; top: 125px; position: absolute">
                <series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title ="Location"></AxisX>
                        <AxisY Title ="Crime Rate"></AxisY>
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
        </p>
        <p>
            Zip Code&nbsp;
            <asp:TextBox ID="ZipCodeInput1" runat="server"></asp:TextBox>
        </p>
        <p>
            <strong>Enter Location 2:
            <asp:Label ID="ErrorLabel2" runat="server"></asp:Label>
            </strong></p>
        <p>
            City and State (ex: Mesa, AZ)</p>
        <p>
            City:
            <asp:TextBox ID="CityInput2" runat="server"></asp:TextBox>
        </p>
        <p>
            State:
            <asp:TextBox ID="StateInput2" runat="server"></asp:TextBox>
            <asp:Label ID="ErrorAir" runat="server" style="z-index: 1; left: 901px; top: 790px; position: absolute; width: 275px"></asp:Label>
        </p>
        <p>
            or by Zip Code (ex: 85204)
            <asp:Label ID="ErrorWalk" runat="server" style="z-index: 1; left: 413px; top: 792px; position: absolute; width: 285px"></asp:Label>
            <asp:Chart ID="Chart3" runat="server" style="z-index: 1; left: 373px; top: 465px; position: absolute">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title ="Location"></AxisX>
                        <AxisY Title ="Walkability"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:Chart ID="Chart4" runat="server" style="z-index: 1; left: 852px; top: 465px; position: absolute">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title ="Location"></AxisX>
                        <AxisY Title ="Air Quality"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </p>
        <p>
            Zip Code
            <asp:TextBox ID="ZipCodeInput2" runat="server"></asp:TextBox>
        </p>
        <p style="margin-left: 80px">
            <asp:Button ID="ResetButton" runat="server" OnClick="ResetButton_Click" Text="Reset" />
&nbsp;&nbsp;
            <asp:Button ID="CompareButton" runat="server" OnClick="CompareButton_Click" Text="Compare" />
&nbsp;&nbsp;&nbsp; </p>
        <p style="margin-left: 80px">
            <asp:Button ID="RecentDataButton" runat="server" OnClick="RecentDataButton_Click" Text="Recent Data" Width="174px" />
        </p>
        <p style="margin-left: 80px">
            &nbsp;</p>
    </form>
</body>
</html>
