using CRM.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraEditors;


namespace CRM.Module.Win
{
    public class ContactViewController : ViewController
    {
        SimpleAction actOrderDrink;
        public ContactViewController() : base()
        {

            TargetObjectType = typeof(Contact);


            actOrderDrink = new SimpleAction(this, "OrderDrink", "View")
            {
                ImageName = "Shopping_ShoppingCart"
            };
            actOrderDrink.Execute += actOrderDrink_Execute;

        }
        private void actOrderDrink_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var contact = View.CurrentObject as Contact;
            
            var msg = $"You ordered {contact?.DrinkPreference?.DrinkName} for {contact?.FirstName}";
            XtraMessageBox.Show(msg);
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }

}
