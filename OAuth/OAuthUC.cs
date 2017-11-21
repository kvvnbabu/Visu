using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OAuth
{
    public partial class OAuthUC : UserControl
    {
        // The AAD Instance is the instance of Azure
        private static string aadInstance = "https://login.microsoftonline.com/{0}";
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        private static string tenant = "";
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        private static string clientId = "";
        // The App Key is a credential used by the application to authenticate to Azure AD.
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];

        private static string ResourceID = ConfigurationManager.AppSettings["ida:ResourceID"];
        // The Authority is the sign-in URL of the tenant.
        static string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        // static string authority = ;

        private static string EndAddress = ConfigurationManager.AppSettings["ida:EndAddress"];

        private static HttpClient httpClient = new HttpClient();

        private static AuthenticationContext authContext = null;
        private static ClientCredential clientCredential = null;
        public OAuthUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authContext = new AuthenticationContext(authority);
            clientCredential = new ClientCredential(clientId, appKey);

            PostTodo().Wait();
        }


        static async Task PostTodo()
        {
            AuthenticationResult result = null;
            try
            {
                result = await authContext.AcquireTokenAsync(ResourceID, clientCredential);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while acquiring a token");
            }
            if (result != null)
            {
                try
                {
                    string _ContentType = "text/xml";
                    ////add access token to the authorization header
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));


                    #region Update Project

                    //StringBuilder strxml = new StringBuilder();
                    //FormXmlUpdateProject(strxml);

                    //string ReqUri = "https://testapi.dnvgl.com/Project/";
                    //httpClient.BaseAddress = new Uri(ReqUri);

                    //////Add subcription key and content type(format) to the headers.
                    //httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ae565812ebe84d2b884a1e63c55a9a23");
                    //"ae565812ebe84d2b884a1e63c55a9a23");

                    #endregion

                    #region Update Order
                    StringBuilder strxml = new StringBuilder();
                    FormXMLForUpdateOrder(strxml);

                    string ReqUri = "https://testapi.dnvgl.com/OrderAPI/";

                    // Add subcription key and content type(format) to the headers.
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ab82b597b8d345d1a93f7b9b3e1feac9");


                    #endregion

                    HttpContent content = new StringContent(strxml.ToString());

                    HttpResponseMessage response = await httpClient.PostAsync(ReqUri, content);

                    MessageBox.Show(response.StatusCode.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine("An error occurred while acquiring a token");
                    MessageBox.Show("An error occurred in response");
                }
            }
        }

        public static string FormXmlUpdateProject(StringBuilder strxml)
        {
            strxml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            strxml.Append("<ns0:projectGBO xmlns:ns0=\"https://SchProjectGBO.SchProjectGBO\">");
            strxml.Append("<OEBS_Header>");

            strxml.Append("<messageDetails>");
            strxml.Append("<msgID>CON-2fcaadec-6e4e-400d-bcd0-d196f5f63fd9</msgID>");
            strxml.Append("<requestedOperation>Update Project</requestedOperation>");
            strxml.Append("<sentTimeStamp>2017-09-20T14:27:26</sentTimeStamp>");
            strxml.Append("<SenderSystem>CON</SenderSystem>");
            strxml.Append("<ReceiverSystem>EBS</ReceiverSystem>");
            strxml.Append("<ServiceCallType>Generic</ServiceCallType>");
            strxml.Append("</messageDetails>");

            strxml.Append("</OEBS_Header>");

            strxml.Append("<project>");
            strxml.Append("<pmProjectReference>Z0562360</pmProjectReference>");
            strxml.Append("<paProjectNumber>10048744</paProjectNumber>");
            strxml.Append("<projectType>Cost Collection</projectType>");
            strxml.Append("<projectName>Z0562360 Fluor-Lux AS - ISO 90</projectName>");
            strxml.Append("<longName>Fluor-Lux AS - ISO 9001:2015</longName>");
            strxml.Append("<projectCurrencyCode>NOK</projectCurrencyCode>");
            strxml.Append("<projectStatusCode>Approved</projectStatusCode>");
            strxml.Append("<startDate>2017-01-28</startDate>");
            strxml.Append("<completionDate>2099-12-31</completionDate>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("<serviceCode>2201</serviceCode>");
            strxml.Append("<costCenter>101472</costCenter>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>IA0</pmTaskReference>");
            strxml.Append("<taskNumber>IA0</taskNumber>");
            strxml.Append("<taskName>IA 2017</taskName>");
            strxml.Append("<taskLongName>Initial Audit</taskLongName>");
            strxml.Append("<description>2017</description>");
            strxml.Append("<startDate>2017-07-22</startDate>");
            strxml.Append("<endDate>2017-09-20</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>P10</pmTaskReference>");
            strxml.Append("<taskNumber>P10</taskNumber>");
            strxml.Append("<taskName>P1 2018</taskName>");
            strxml.Append("<taskLongName>Periodic Audit; P1</taskLongName>");
            strxml.Append("<description>2018</description>");
            strxml.Append("<startDate>2018-04-27</startDate>");
            strxml.Append("<endDate>2018-12-24</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>P20</pmTaskReference>");
            strxml.Append("<taskNumber>P20</taskNumber>");
            strxml.Append("<taskName>P2 2019</taskName>");
            strxml.Append("<taskLongName>Periodic Audit; P2</taskLongName>");
            strxml.Append("<description>2019</description>");
            strxml.Append("<startDate>2019-04-27</startDate>");
            strxml.Append("<endDate>2019-12-24</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>DI0</pmTaskReference>");
            strxml.Append("<taskNumber>DI0</taskNumber>");
            strxml.Append("<taskName>DI 2017</taskName>");
            strxml.Append("<taskLongName>Document Review &amp; Initial Visit</taskLongName>");
            strxml.Append("<description>2017</description>");
            strxml.Append("<startDate>2017-03-11</startDate>");
            strxml.Append("<endDate>2017-11-06</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>RC0</pmTaskReference>");
            strxml.Append("<taskNumber>RC0</taskNumber>");
            strxml.Append("<taskName>RC 2020</taskName>");
            strxml.Append("<taskLongName>Re-certification Audit</taskLongName>");
            strxml.Append("<description>2020</description>");
            strxml.Append("<startDate>2020-04-28</startDate>");
            strxml.Append("<endDate>2020-12-26</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<task>");
            strxml.Append("<pmTaskReference>NI01</pmTaskReference>");
            strxml.Append("<taskNumber>NI01</taskNumber>");
            strxml.Append("<taskName>NIA</taskName>");
            strxml.Append("<taskLongName>Non Invoiceable Activity</taskLongName>");
            strxml.Append("<description>NIA</description>");
            strxml.Append("<startDate>2017-01-28</startDate>");
            strxml.Append("<endDate>2091-06-29</endDate>");
            strxml.Append("<billableFlag>N</billableFlag>");
            strxml.Append("</task>");

            strxml.Append("<customerReference>");
            strxml.Append("<billToAddressID>2331678</billToAddressID>");
            strxml.Append("<shipToAddressID>2331677</shipToAddressID>");
            strxml.Append("<DNVGL_ID>10476447</DNVGL_ID>");
            strxml.Append("<InvoiceCustomerFirstName>Grete</InvoiceCustomerFirstName>");
            strxml.Append("<InvoiceCustomerLastName>Zachariassen</InvoiceCustomerLastName>");
            strxml.Append("</customerReference>");

            strxml.Append("<agreements/>");

            strxml.Append("<projectTeam>");

            strxml.Append("<teamMember>");
            strxml.Append("<projectRoleType>PROJECT CONTROLLER</projectRoleType>");
            strxml.Append("<startDateActive>2017-02-02</startDateActive>");
            strxml.Append("<endDateActive>2099-12-31</endDateActive>");
            strxml.Append("<personID>3235</personID>");
            strxml.Append("</teamMember>");

            strxml.Append("<teamMember>");
            strxml.Append("<projectRoleType>PROJECT MANAGER</projectRoleType>");
            strxml.Append("<startDateActive>2017-02-02</startDateActive>");
            strxml.Append("<endDateActive>2099-12-31</endDateActive>");
            strxml.Append("<personID>24631</personID>");
            strxml.Append("</teamMember>");

            strxml.Append("</projectTeam>");

            strxml.Append("<budgets/>");
            strxml.Append("</project>");
            strxml.Append("</ns0:projectGBO>");

            return strxml.ToString();
        }

        public static string FormXMLForUpdateOrder(StringBuilder strxml)
        {
            strxml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            strxml.Append("<ns0:orderGBO xmls:ns0=\"http://SchOrderGBO.SchOrderGBO\">");

            strxml.Append("<OEBS_Header>");

            strxml.Append("<messageDetails>");
            strxml.Append("<msgID>8f0e40-7b9-4bb6-88d4-e92bf9be553</msgID>");
            strxml.Append("<responseLocation>osl1679.verit.dnv.com\\private$\\pettertest</responseLocation>");
            strxml.Append("<requestedOperation>Update Order</requestedOperation>");
            strxml.Append("<sentTimeStamp>10/13/2017 14:16:15</sentTimeStamp>");
            strxml.Append("<SenderSystem>UFO</SenderSystem>");
            strxml.Append("<ReceiverSystem>ESB</ReceiverSystem>");
            strxml.Append("<ServiceCallType>GenericService</ServiceCallType>");
            strxml.Append("</messageDetails>");

            strxml.Append("</OEBS_Header>");

            strxml.Append("<order>");

            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderNumber>223006201</orderNumber>");
            strxml.Append("<orderNumberRef>C46616|1-7151807|1-3ZTQ</orderNumberRef>");
            strxml.Append("<returnReason/>");
            strxml.Append("<orderType>New Sale</orderType>");
            strxml.Append("<orderCurrency>NOK</orderCurrency>");
            strxml.Append("<invoiceToOrg>879742</invoiceToOrg>");
            strxml.Append("<shipToOrg>943719</shipToOrg>");
            strxml.Append("<paymentTermName/>");
            strxml.Append("<optyOwner>LTMILDA</optyOwner>");
            strxml.Append("<costCenterID>101477</costCenterID>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<PO_Number>Rederiavdelingen-610</PO_Number>");

            strxml.Append("<customerReference>");
            strxml.Append("<source_customerID>192378</source_customerID>");
            strxml.Append("<DNVGL_ID>192378</DNVGL_ID>");
            strxml.Append("<ContactFirstName>Eskil Fjelldahl</ContactFirstName>");
            strxml.Append("</customerReference>");

            strxml.Append("<orderItem>");
            strxml.Append("<DNVGLid>192378</DNVGLid>");
            strxml.Append("<taskNumber>TC4</taskNumber>");
            strxml.Append("<projectNumber>10050053</projectNumber>");
            strxml.Append("<serviceCode>2320</serviceCode>");
            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderLineNumber>1</orderLineNumber>");
            strxml.Append("<orderLineType>BAG Fixed Fee</orderLineType>");
            strxml.Append("<orderLineTotalPrice>8800</orderLineTotalPrice>");
            strxml.Append("<productID>Activity</productID>");
            strxml.Append("<productDescription>Eskil Fjelldahl</productDescription>");
            strxml.Append("<unitQuantity>1</unitQuantity>");
            strxml.Append("<unitQuantityUOM>Each</unitQuantityUOM>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("</orderItem>");

            strxml.Append("<orderItem>");
            strxml.Append("<DNVGLid>192378</DNVGLid>");
            strxml.Append("<taskNumber>TC4</taskNumber>");
            strxml.Append("<projectNumber>10050053</projectNumber>");
            strxml.Append("<serviceCode>2320</serviceCode>");
            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderLineNumber>2</orderLineNumber>");
            strxml.Append("<orderLineType>BAG Fixed Fee</orderLineType>");
            strxml.Append("<orderLineTotalPrice>0</orderLineTotalPrice>");
            strxml.Append("<productID>Travel Expense</productID>");
            strxml.Append("<productDescription>Material Expenses</productDescription>");
            strxml.Append("<unitQuantity>0</unitQuantity>");
            strxml.Append("<unitQuantityUOM>Each</unitQuantityUOM>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("</orderItem>");

            strxml.Append("<orderItem>");
            strxml.Append("<DNVGLid>192378</DNVGLid>");
            strxml.Append("<taskNumber>TC4</taskNumber>");
            strxml.Append("<projectNumber>10050053</projectNumber>");
            strxml.Append("<serviceCode>2320</serviceCode>");
            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderLineNumber>3</orderLineNumber>");
            strxml.Append("<orderLineType>BAG Fixed Fee</orderLineType>");
            strxml.Append("<orderLineTotalPrice>0</orderLineTotalPrice>");
            strxml.Append("<productID>Travel Expense</productID>");
            strxml.Append("<productDescription>Other Expenses</productDescription>");
            strxml.Append("<unitQuantity>0</unitQuantity>");
            strxml.Append("<unitQuantityUOM>Each</unitQuantityUOM>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("</orderItem>");

            strxml.Append("<orderItem>");
            strxml.Append("<DNVGLid>192378</DNVGLid>");
            strxml.Append("<taskNumber>TC4</taskNumber>");
            strxml.Append("<projectNumber>10050053</projectNumber>");
            strxml.Append("<serviceCode>2320</serviceCode>");
            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderLineNumber>4</orderLineNumber>");
            strxml.Append("<orderLineType>BAG Variable QTY</orderLineType>");
            strxml.Append("<orderLineTotalPrice>0</orderLineTotalPrice>");
            strxml.Append("<productID>Mileage Rate</productID>");
            strxml.Append("<productDescription>Mileage Rate</productDescription>");
            strxml.Append("<unitQuantity>0</unitQuantity>");
            strxml.Append("<unitQuantityUOM>Kilometre</unitQuantityUOM>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("</orderItem>");

            strxml.Append("<orderItem>");
            strxml.Append("<DNVGLid>192378</DNVGLid>");
            strxml.Append("<taskNumber>TC4</taskNumber>");
            strxml.Append("<projectNumber>10050053</projectNumber>");
            strxml.Append("<serviceCode>2320</serviceCode>");
            strxml.Append("<orderSource>CON</orderSource>");
            strxml.Append("<orderLineNumber>5</orderLineNumber>");
            strxml.Append("<orderLineType>BAG Variable QTY</orderLineType>");
            strxml.Append("<orderLineTotalPrice>0</orderLineTotalPrice>");
            strxml.Append("<productID>Follow Up</productID>");
            strxml.Append("<productDescription>Follow Up</productDescription>");
            strxml.Append("<unitQuantity>0</unitQuantity>");
            strxml.Append("<unitQuantityUOM>Hours</unitQuantityUOM>");
            strxml.Append("<costCenterName>101477</costCenterName>");
            strxml.Append("<plObject>Z00</plObject>");
            strxml.Append("</orderItem>");

            strxml.Append("</order>");
            strxml.Append("</ns0:orderGBO>");

            return strxml.ToString();
        }
    }
}
