using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.AddIn;
using System.Drawing;
using System.Windows.Forms;
using RightNow.AddIns.AddInViews;
using static System.Net.Mime.MediaTypeNames;

namespace OAuth
{
    [AddIn("OAuthAddin", Version = "1.0.0.0")]
    public class Factory : IWorkspaceComponentFactory2
    {
        IGlobalContext _IGlobalContext;
        public IWorkspaceComponent2 CreateControl(bool inDesignMode, IRecordContext context)
        {
            return new Component(inDesignMode, context, _IGlobalContext);
        }

        public System.Drawing.Image Image16
        {
            get;
            set;
        }


        public string Text
        {
            get { return "OAuthAddin"; }
        }


        public string Tooltip
        {
            get { return "OAuth Addin"; }
        }


        public bool Initialize(IGlobalContext context)
        {
            _IGlobalContext = context;
            return true;
        }
    }


    public class Component : IWorkspaceComponent2
    {
        /// <summary> 
        /// set to true if the control is in a workspace designer 
        /// </summary> 
        private bool inDesignMode;

        /// <summary> 
        /// the workspace control 
        /// </summary> 
        private OAuthUC control;

        /// <summary> 
        /// create the component 
        /// </summary> 
        /// <param name="inDesignMode">store the inDesignMode flag</param> 
        public Component(bool inDesignMode, IRecordContext context, IGlobalContext gc)
        {
            this.inDesignMode = inDesignMode;

            //create the UI control and stoactionre it
            control = new OAuthUC(inDesignMode, context, gc);
        }

        public bool ReadOnly
        {
            get;
            set;
        }

        public void RuleActionInvoked(string actionName)
        {
            throw new NotImplementedException();
        }

        public string RuleConditionInvoked(string conditionName)
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// return the reference to the control 
        /// </summary> 
        /// <returns></returns> 
        public Control GetControl()
        {
            return control;
        }
    }

}
