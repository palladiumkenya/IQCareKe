using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

  [assembly: TagPrefix("IQCare.IQControl","IQ")]
namespace IQCare.IQControl
{
    [ToolboxData("<{0}:IQLookupTextBox runat=\"server\"></{0}:IQLookupTextBox>")]
    public class IQLookupTextBox : CompositeControl
    {
        private Label nameLabel;
        private TextBox valueTextBox;

        private string _serviceMethod = "GetLookupValue";
        private string _textboxcssClass="form-control";
        private string _labelcssClass = "form-horizontal control-label pull-left col-md-2";
        private string _servicePath ="IQLookupWS.asmx";
        private string _lookupCategory = "-1";
        private string _lookupName = "MST_MODDECODE";
        private HiddenField selectedValue;
        private static readonly object EventSubmitKey =         new object();
        private int _minimumLength = 2;
        private double _minimumWidth = 280;
        private bool _showLabel = false;

        
        [
       Bindable(true),
       Category("Data"),
       DefaultValue(""),
       Description("The lookup  category")
       ]
        public string LookupCategory
        {
            get
            {
                EnsureChildControls();
                return _lookupCategory;
            }
            set
            {
                EnsureChildControls();
                _lookupCategory = value;
            }
        }
        [
        Bindable(true),
        Category("Data"),
        DefaultValue(""),
        Description("The lookup  name")
        ]    
        public string LookupName
        {
            get
            {
                EnsureChildControls();
                return _lookupName;
            }
            set
            {
                EnsureChildControls();
                _lookupName = value;
            }
        }

        [
        Bindable(true),
        Category("Default"),
        DefaultValue("Field Label"),
        Description("The field label.")
        ]           
        public string LabelText
        {
            get
            {
                EnsureChildControls();
                return nameLabel.Text;
            }
            set
            {
                EnsureChildControls();
                nameLabel.Text = value;
            }
        }
        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("The selected Text.")
        ]
        public string ValueText
        {
            get
            {
                EnsureChildControls();
                return valueTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                valueTextBox.Text = value;
            }
        }
        [
       Bindable(true),
       Category("Default"),
       Description("ID For the selected value control.")
       ]
        public string ValueControlId
        {
            get
            {
                this.EnsureChildControls();
                return selectedValue.ID;
            }
            //set
            //{
            //    this.EnsureChildControls();
            //    selectedValue.ID = this.ID + "_HDFId"; ;
            //}
        }
         [
        Bindable(true),
        Category("Default"),           
        Description("ID For the textbox.")
        ]
        public string ValueTextId
        {
            get
            {
                this.EnsureChildControls();
                return valueTextBox.ID;
            }
           //set
           // {
           //     this.EnsureChildControls();
           //     valueTextBox.ID = this.ID + "_TXTId"; ;
           // }
        }
         [
        Bindable(true),
        Category("Default"),        
        Description("Id for the label.")
        ]
        public string LabelTextId
        {
            get
            {
                this.EnsureChildControls();
                return nameLabel.ID;
            }
           //set
           // {
           //     this.EnsureChildControls();
           //     nameLabel.ID = this.ID+"_LBLId";
           // }
        }
        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Selected value in value(int):display text delimited by :")
        ]
        public string SelectedValue
        {
            get
            {
                EnsureChildControls();
                return selectedValue.Value;
            }
            set
            {
                EnsureChildControls();
                selectedValue.Value = value;
            }
        }
        [
       Bindable(true),
       Category("Default"),
       DefaultValue("2"),
       Description("The minimum length of the search string")
       ]
        public int MinLength
        {
            get
            {
                EnsureChildControls();
                return _minimumLength;
            }
          private  set
            {
                EnsureChildControls();
                _minimumLength = value;
            }
        }
        [Bindable(true), Category("Appearance"), DefaultValue(false), Localizable(true)]
        public bool ShowLabel
        {
            get
            {
                return _showLabel;
            }

            set
            {
                _showLabel = value;
            }
        }
        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
              public string TextBoxCssClass
        {
            get
            {
                return _textboxcssClass;
            }

            set
            {
                _textboxcssClass = value;
            }
        }
        [Bindable(true), Category("Data"), DefaultValue(":"), Localizable(true)]
        public string ValueDelimiter
        {
            get
            {
                return ":";
            }

           
        }
        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string LabelCssClass
        {
            get
            {
                return _labelcssClass;
            }

            set
            {
                _labelcssClass =  value;
            }
        }
        public bool NewItemSelected
        {
            get
            {
                return this.selectedValue.Value == "-99";
            }
        }
        public bool NoItemSelected
        {
            get
            {
                return this.selectedValue.Value == "-1";
            }
        }
        [
        Bindable(true),
        Category("Default"),
        DefaultValue("GetLookupValue"),
        Description("Service Method name for lookup")
        ]
        public string ServiceMethod
        {
            get
            {
                EnsureChildControls();
                return this._serviceMethod;
            }
            set
            {
                EnsureChildControls();
                this._serviceMethod = value;
            }
        }
   
        [
       Bindable(true),
       Category("Default"),
       DefaultValue("IQLookupWS.asmx"),
       Description("Service Method name for lookup")
       ]
        public string ServicePath
        {
            get
            {
                EnsureChildControls();
                return ResolveUrl(this._servicePath);
            }
            set
            {
                EnsureChildControls();
                this._servicePath = value;
            }
        }
        public override Unit Width
        {
            get
            {
                return base.Width.Value < _minimumWidth ? new Unit(this._minimumWidth) : base.Width;
            }
            set
            {
                base.Width = value.IsEmpty || value.Value < _minimumWidth ?  new Unit(this._minimumWidth) : value;
            }
        }
        private string DialogId
        {
            get
            {
                return this.ID + "_DIAG";
            }
        }
        //public event EventHandler TextChanged
        //{
        //    add
        //    {
        //        Events.AddHandler(EventSubmitKey, value);
        //    }
        //    remove
        //    {
        //        Events.RemoveHandler(EventSubmitKey, value);
        //    }
        //}
        //protected virtual void OnTextChanged(EventArgs e)
        //{
        //    EventHandler changeHandler =   (EventHandler)Events[EventSubmitKey];
        //    if (changeHandler != null)
        //    {
        //        changeHandler(this, e);
        //    }
        //}
        //private void _textBox_TextChange(object source, EventArgs e)
        //{
        //    OnTextChanged(EventArgs.Empty);
        //}

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();

            nameLabel = new Label();
            nameLabel.ID = this.ID + "_LBLId";
            nameLabel.Font.Bold = true;
            valueTextBox = new TextBox();
           // valueTextBox.Width = this.Width;
            valueTextBox.ID = this.ID + "_TXTId";
         

            selectedValue = new HiddenField();
            selectedValue.ID = this.ID + "_HDFId";
            selectedValue.Value = "-1";

            valueTextBox.Attributes.Add("style", "ul.ui-autocomplete li.ui-menu-item{text-align:left;}");  
            valueTextBox.Attributes.Add("placeholder", "type here to search");
            if (this._showLabel)
            {
                nameLabel.Attributes.Add("style", "padding:5px 10px 5px 10px; font-weight:bold");
                nameLabel.Attributes.Add("for", valueTextBox.ClientID);
                this.Controls.Add(nameLabel);
            }
            this.Controls.Add(valueTextBox);
            this.Controls.Add(selectedValue);
            base.CreateChildControls();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            (new LiteralControl(@"<div class=""form-group"" style=""white-space:nowrap; display:inline"">")).RenderControl(writer);
            if (this._showLabel)
            {
                //string labelstring=@"<label class=""form-horizontal control-label pull-left col-md-2"" align=""center"">"+this.LabelText+@":</label>";
                (new LiteralControl(@"<div>")).RenderControl(writer);
                //new LiteralControl(labelstring).RenderControl(writer);
                nameLabel.RenderControl(writer);
                (new LiteralControl(@"</div>")).RenderControl(writer);
            }

            (new LiteralControl(@"<div style=""width:100%"">")).RenderControl(writer);
            valueTextBox.RenderControl(writer);
            selectedValue.RenderControl(writer);
            (new LiteralControl(@"</div>")).RenderControl(writer);
            RenderContents(writer);
        }
       
        protected override void RenderContents(HtmlTextWriter writer)
        {
          
            StringBuilder calnder = new StringBuilder();
            //adding javascript first
            calnder.AppendFormat(@"<script type='text/javascript'>
                                     $(document).ready(function(){{
                                        $(""#{0}"").autocomplete({{
                                            source: function (request, response) {{
                                                $.ajax({{
                                                    url: '{1}/{2}' ,
                                                    data: ""{{ 'prefix': '"" + {3} + ""', lookupname: '{4}', lookupCategory: '{5}' }}"",
                                                    dataType: ""json"",
                                                    type:   ""POST"",
                                                    contentType: ""application/json; charset=utf-8"",
                                                    success: function (data) {{
                                                       if(data.d.length> 0){{ response($.map(data.d, function (item) {{ return {{ label: item.split('{6}')[0], val:   item.split('{6}')[1]  }} }}))}}
                                                       else {{ $(""#{8}"").val(""-99""); }} 
                                                    }},
                                                    error: function (response) {{ 
                                                                $(""#{7}"").html("""");
                                                                try{{
                                                                  var responseText = jQuery.parseJSON(response.responseText);
                                                                  $(""#{7}"").append(""<div><u>Error Message</u>:<br /><br />"" + responseText.Message + ""</div>"");     
                                                                }}
                                                                catch (e) {{ $(""#{7}"").html(""Unknown error occurred"");
                                                                }}
                                                                $(""#{7}"").dialog({{
                                                                     title: ""Lookup error details"",
                                                                     width: 350,  buttons: {{ Close: function() {{ $(this).dialog('close') }} }} }} ); 
                                                                
                                                    }},
                                                    failure: function (response) {{  }}
                                                }});
                                            }},
                                            select: function (e,i) {{$(""#{8}"").val(i.item.val);}},
                                             minLength : {9},
                                             delay : 5
                                        }});
                                     }}); ", 
                                           valueTextBox.ClientID, 
                                           this.ServicePath, 
                                           this.ServiceMethod, 
                                           "request.term",
                                           this.LookupName,
                                           this.LookupCategory,
                                           this.ValueDelimiter,
                                           this.DialogId,
                                           this.selectedValue.ClientID, 
                                           this.MinLength);
            calnder.Append("</script>");
            writer.Write(calnder.ToString());
            StringBuilder dialog = new StringBuilder();
            dialog.AppendFormat(@"<div id=""{0}"" style=""display:none"" style=""height:300px;overflow: auto; font-size: 10pt !important; font-weight: normal !important; background-color: #FFFFC1; margin: 5px; border: 1px solid #ff6a00;margin-bottom:5px;""></div>", this.DialogId);
            writer.Write(dialog);
        }
       
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.AddStyleSheet("IQCare.IQControl.Resources.jquery-ui-1.8.css");
            this.AddStyleSheet("IQCare.IQControl.Resources.bootstrap.css");
            AddJavaScript("IQCare.IQControl.Resources.jquery-1.4.min.js");
            AddJavaScript("IQCare.IQControl.Resources.jquery-ui-1.8.min.js");
            if (!string.IsNullOrEmpty(this.TextBoxCssClass))
            {
                valueTextBox.Attributes.Add("class", this.TextBoxCssClass);
            }
            if (!string.IsNullOrEmpty(this.LabelCssClass))
            {
                nameLabel.Attributes.Add("class", this.LabelCssClass);
            }
        }
        private void AddStyleSheet(string cssFile)
        {
            string includeTemplate = "<link rel='stylesheet' text='text/css' href='{0}' />";
            string includeLocation =
                  Page.ClientScript.GetWebResourceUrl(this.GetType(), cssFile);
            LiteralControl include = new LiteralControl(String.Format(includeTemplate, includeLocation));
            Page.Header.Controls.Add(include);
        }

        private void AddJavaScript(string javaScriptFile)
        {
            string scriptLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), javaScriptFile);
            Page.ClientScript.RegisterClientScriptInclude(javaScriptFile, scriptLocation);

        }
    }
}
