<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="registration._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
               
.alert {
     padding: 0px; 
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 4px;
}

    </style>


    <div class="form-group">
        <label for="First Name"><b>First Name</b></label>
        <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ValidationGroup="registration" class="alert alert-danger" Text="First Name Required!"></asp:RequiredFieldValidator>
        <br />
        <label for="Last Name"><b>LastName</b></label>
        <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ValidationGroup="registration" ControlToValidate="txtLastName" class="alert alert-danger" Text="Last Name Required!"></asp:RequiredFieldValidator>
        <br />
        <label for="Address1"><b>Address1</b></label>
        <asp:TextBox ID="txtAddress1" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ValidationGroup="registration" ControlToValidate="txtAddress1" class="alert alert-danger" Text="Address1 Required!"></asp:RequiredFieldValidator>
        <br />

        <label for="Address2"><b>Address2</b></label>
        <asp:TextBox ID="txtAddress2" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddress2" runat="server" ValidationGroup="registration" ControlToValidate="txtAddress2" class="alert alert-danger" Text="Address2 Required!"></asp:RequiredFieldValidator>
        <br />

        <label for="City"><b>City</b></label>
        <asp:TextBox ID="txtCity" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ValidationGroup="registration" ControlToValidate="txtCity" class="alert alert-danger" Text="City Required!"></asp:RequiredFieldValidator>
        <br />

        <label for="State"><b>State</b></label>
        <asp:DropDownList ID="DropDownListState" runat="server" CssClass="form-control">
            <asp:ListItem Value="AL">Alabama</asp:ListItem>
            <asp:ListItem Value="AK">Alaska</asp:ListItem>
            <asp:ListItem Value="AZ">Arizona</asp:ListItem>
            <asp:ListItem Value="AR">Arkansas</asp:ListItem>
            <asp:ListItem Value="CA">California</asp:ListItem>
            <asp:ListItem Value="CO">Colorado</asp:ListItem>
            <asp:ListItem Value="CT">Connecticut</asp:ListItem>
            <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
            <asp:ListItem Value="DE">Delaware</asp:ListItem>
            <asp:ListItem Value="FL">Florida</asp:ListItem>
            <asp:ListItem Value="GA">Georgia</asp:ListItem>
            <asp:ListItem Value="HI">Hawaii</asp:ListItem>
            <asp:ListItem Value="ID">Idaho</asp:ListItem>
            <asp:ListItem Value="IL">Illinois</asp:ListItem>
            <asp:ListItem Value="IN">Indiana</asp:ListItem>
            <asp:ListItem Value="IA">Iowa</asp:ListItem>
            <asp:ListItem Value="KS">Kansas</asp:ListItem>
            <asp:ListItem Value="KY">Kentucky</asp:ListItem>
            <asp:ListItem Value="LA">Louisiana</asp:ListItem>
            <asp:ListItem Value="ME">Maine</asp:ListItem>
            <asp:ListItem Value="MD">Maryland</asp:ListItem>
            <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
            <asp:ListItem Value="MI">Michigan</asp:ListItem>
            <asp:ListItem Value="MN">Minnesota</asp:ListItem>
            <asp:ListItem Value="MS">Mississippi</asp:ListItem>
            <asp:ListItem Value="MO">Missouri</asp:ListItem>
            <asp:ListItem Value="MT">Montana</asp:ListItem>
            <asp:ListItem Value="NE">Nebraska</asp:ListItem>
            <asp:ListItem Value="NV">Nevada</asp:ListItem>
            <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
            <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
            <asp:ListItem Value="NM">New Mexico</asp:ListItem>
            <asp:ListItem Value="NY">New York</asp:ListItem>
            <asp:ListItem Value="NC">North Carolina</asp:ListItem>
            <asp:ListItem Value="ND">North Dakota</asp:ListItem>
            <asp:ListItem Value="OH">Ohio</asp:ListItem>
            <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
            <asp:ListItem Value="OR">Oregon</asp:ListItem>
            <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
            <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
            <asp:ListItem Value="SC">South Carolina</asp:ListItem>
            <asp:ListItem Value="SD">South Dakota</asp:ListItem>
            <asp:ListItem Value="TN">Tennessee</asp:ListItem>
            <asp:ListItem Value="TX">Texas</asp:ListItem>
            <asp:ListItem Value="UT">Utah</asp:ListItem>
            <asp:ListItem Value="VT">Vermont</asp:ListItem>
            <asp:ListItem Value="VA">Virginia</asp:ListItem>
            <asp:ListItem Value="WA">Washington</asp:ListItem>
            <asp:ListItem Value="WV">West Virginia</asp:ListItem>
            <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
            <asp:ListItem Value="WY">Wyoming</asp:ListItem>
        </asp:DropDownList>        
        <br />
        <label for="Zip"><b>Zip</b></label>
        <asp:TextBox ID="txtZip" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvZip" runat="server" ValidationGroup="registration" ControlToValidate="txtZip" class="alert alert-danger" Text="Zip Required!"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="rfvZip2" runat="server" ControlToValidate="txtZip"
            ValidationExpression="^[0-9]{5}(?:-[0-9]{4})?$"
            ErrorMessage="Invalid Zip Code." class="alert alert-danger"></asp:RegularExpressionValidator>
        <br />

        <label for="Country"><b>Country</b></label>
        <asp:TextBox ID="txtCountry" runat="server" class="form-control" Text="United States" ReadOnly="true"></asp:TextBox>
        <%--<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ValidationGroup="registration" ControlToValidate="txtCountry" class="alert alert-danger" Text="Required!" ></asp:RequiredFieldValidator>--%>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="registration" />
    </div>
</asp:Content>
