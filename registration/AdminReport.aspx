<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminReport.aspx.cs" Inherits="registration.AdminReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
     <div>    
         <asp:GridView ID="gvAdminReport" runat="server" AutoGenerateColumns="false">    
             <Columns>                     
                 <asp:BoundField DataField="FirstName" HeaderText="First Name" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="LastName" HeaderText="LastName" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Address1" HeaderText="Address1" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Address2" HeaderText="Address2" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Width="150" />                     
                 <asp:BoundField DataField="CreateDate" HeaderText="Date Created" ItemStyle-Width="155" />                     
             </Columns>    
         </asp:GridView>    
     </div>    
</asp:Content>
