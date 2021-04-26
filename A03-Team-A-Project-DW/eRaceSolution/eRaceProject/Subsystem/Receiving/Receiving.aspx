<%@ Page Title="Receiving" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receiving.aspx.cs" Inherits="eRaceProject.Subsystem.Receiving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-10">
            <h1>Receiving</h1>
        </div>
          
        <div>
            <asp:Label ID="Label1" runat="server" Text="User : "></asp:Label>
            <asp:Label ID="LoggedUser" runat="server"></asp:Label>
            <asp:Label ID="Test" runat="server"></asp:Label>
       </div>
       
    </div>
   
    
    <div class="col-10">
        <div class ="row">
            <asp:DropDownList ID="ReceivingOrderListDropDown" runat="server"></asp:DropDownList>&nbsp; &nbsp 
            
            <!-- 'Open' button -->
            <asp:LinkButton ID="ReceivingSearchOrderButton" runat="server" Text="Open" OnClick="ReceivingSearchOrder_Click"></asp:LinkButton>

        </div>
        <!-- Vendor Display + Receive Shipment button -->
        <div class ="row">
         
            <asp:Label ID="MessageLabel" runat="server" Text=" "></asp:Label>
            
         <asp:GridView ID="DisplayVendor" runat="server" OnSelectedIndexChanged="Display_Order_SelectedIndexChanged" 
                BorderStyle="None">
               


            </asp:GridView> 
          <!-- <asp:TextBox ID="ReceivedUnits" runat="server"></asp:TextBox> -->
            <!-- Order view -->
          <!--  DataFormatString="{0:p} -->
          <!--  https://stackoverflow.com/questions/47776548/how-to-add-text-to-aspboundfield-in-gridview -->
            
            <div class="row" />
            <div class="row" />
         
            <asp:GridView ID="Display_Order" runat="server" OnSelectedIndexChanged="DisplayVendor_SelectedIndexChanged" AutoGenerateColumns="False">
                
                <Columns>
                    
                    <asp:BoundField DataField="Item" HeaderText="ItemName" SortExpression="Item"></asp:BoundField>
                    <asp:BoundField DataField="QuantityOrdered"  HeaderText="Quantity Ordered"></asp:BoundField>
                    <asp:BoundField DataField="OrderedUnits" HeaderText ="Ordered Units"></asp:BoundField>
                    <asp:BoundField DataField="QuantityOutstanding" HeaderText="Quantity Outstanding" SortExpression="QuantityOutstanding"></asp:BoundField>
                    
              <asp:TemplateField HeaderText="Received Units">
                    <ItemTemplate>
                            <asp:TextBox ID="ReceivedUnits" runat="server" PlaceHolder="0" Visible="true"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" Text="x case of   "  ></asp:Label>
                            
                     </ItemTemplate>
             
                  </asp:TemplateField>

               <asp:TemplateField HeaderText="Rejected Units/ Reason">
                    <ItemTemplate>
                            <asp:TextBox ID="RejectedUnits" runat="server" PlaceHolder="0" Visible="true" Width="25"></asp:TextBox>
                            <asp:TextBox ID="Reason" runat="server" PlaceHolder="Please enter a reason" Visible="true" Width="85"></asp:TextBox>
                    </ItemTemplate>
               </asp:TemplateField>

                      <asp:TemplateField HeaderText="Salvaged Items">
                    <ItemTemplate>
                            <asp:TextBox ID="SalvagedItems" runat="server" PlaceHolder="0" Visible="true"></asp:TextBox>
                     </ItemTemplate>
               </asp:TemplateField>

                </Columns>

            </asp:GridView>
            
        <!--Unordered Items ListView -->


            <asp:ListView ID="Unordered_Items" runat="server">
                <ItemTemplate>
                    <tr style="background-color: azure">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Item Name" Placeholder="Please input a name"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Vendor ID" Placeholder="Please input a Vendor ID"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Quantity" Placeholder="Please input a Quantity"></asp:Label>
                        </td>
                      

                    </tr>
                </ItemTemplate>
                <InsertItemTemplate>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                      <td>
                            <asp:Label ID="Label6" runat="server" Text="Test" Placeholder="Please input a Quantity"></asp:Label>
                        </td>


                </InsertItemTemplate>
                
                    
                        

                  
                

            </asp:ListView>

            


          <%--  <asp:ListView ID="Display_Order" runat="server" OnSelectedIndexChanged="DisplayVendor_SelectedIndexChanged">


                <ItemTemplate>

                        
                       <div class="row col-10">
                           <div class="column" style="background-color:antiquewhite">
                           
                                <asp:Label Text='<%# Eval("Item") %>' runat="server" ID="ItemLabel" Width="200px" />

                           </div>
                       </div>
                       
  
                </ItemTemplate>


               



            </asp:ListView>--%>
                 </div>
           
            
           
         </div>
    </div>

  
  

    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>

</asp:Content>
